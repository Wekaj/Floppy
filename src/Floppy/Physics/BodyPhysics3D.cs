using Floppy.Utilities;
using Microsoft.Xna.Framework;
using System;

namespace Floppy.Physics {
    public static class BodyPhysics3D {
        public static void UpdateBody(Body3D body, float deltaTime, ITerrain3D? terrain = null) {
            ResetContact(body);

            ApplyGravity(body, deltaTime);
            ApplyForce(body, deltaTime);
            ApplyImpulse(body);

            CapVelocity(body);

            if (terrain is not null) {
                DoTileCollisions(body, deltaTime, terrain);
            }

            ApplyVelocity(body, deltaTime);

            ApplyFriction(body, deltaTime);
        }

        private static void ResetContact(Body3D body) {
            body.Contact = Vector3.Zero;
            body.IsSubmerged = false;
        }

        private static void ApplyGravity(Body3D body, float deltaTime) {
            if (!body.IsSubmerged || body.FloatForce.HasValue) {
                body.Velocity += body.Gravity * deltaTime;
            }
        }

        private static void ApplyForce(Body3D body, float deltaTime) {
            body.Velocity += body.Force * deltaTime / body.Mass;
            body.Force = Vector3.Zero;
        }

        private static void ApplyImpulse(Body3D body) {
            body.Velocity += body.Impulse / body.Mass;
            body.Impulse = Vector3.Zero;
        }

        private static void CapVelocity(Body3D body) {
            float speed = body.Velocity.Length();
            if (speed > body.MaxSpeed) {
                body.Velocity = body.Velocity * body.MaxSpeed / speed;
            }
        }

        private static void ApplyVelocity(Body3D body, float deltaTime) {
            body.Position += body.Velocity * deltaTime;
        }

        private static void ApplyFriction(Body3D body, float deltaTime) {
            body.Velocity /= Vector3.One + body.Friction * deltaTime;
        }

        private static void DoTileCollisions(Body3D body, float deltaTime, ITerrain3D terrain) {
            bool doVertical = true;
            bool doHorizontal = true;
            bool doDepth = true;

            // A bit of a mess.
            for (int i = 0; i < 3; i++) {
                Cuboid worldBounds = body.Bounds.Offset(body.Position).Shrink(0.001f);

                float verticalTarget = 0f;
                float verticalOverlap = float.MaxValue;
                if (doVertical && body.Velocity.Y != 0f) {
                    GetEdges(worldBounds, new Vector3(0f, body.Velocity.Y * deltaTime, 0f), terrain, out int left, out int top, out int front, out int right, out int bottom, out int back);

                    int startY = top + 1;
                    int endY = bottom + 1;
                    int directionY = 1;
                    if (body.Velocity.Y < 0f) {
                        startY = bottom - 1;
                        endY = top - 1;
                        directionY = -1;
                    }

                    int startX = left;
                    int endX = right + 1;
                    int directionX = 1;
                    if (body.Velocity.X < 0f) {
                        startX = right;
                        endX = left - 1;
                        directionX = -1;
                    }

                    int startZ = front;
                    int endZ = back + 1;
                    int directionZ = 1;
                    if (body.Velocity.Z < 0f) {
                        startZ = back;
                        endZ = front - 1;
                        directionZ = -1;
                    }

                    int tileY = -1;
                    bool found = false;
                    for (int y = startY; y != endY && !found; y += directionY) {
                        for (int x = startX; x != endX && !found; x += directionX) {
                            for (int z = startZ; z != endZ; z += directionZ) {
                                if (IsSolidBlock(x, y, z, terrain) && !IsSolidBlock(x, y - directionY, z, terrain)) {
                                    tileY = y;
                                    found = true;
                                    break;
                                }
                                else {
                                    bool isSolid = terrain.IsSolid(x, y, z);
                                    BlockShape blockShape = terrain.GetBlockShape(x, y, z);

                                    if (isSolid && blockShape != BlockShape.Block) {
                                        float pz1 = 1f - (worldBounds.Front - z);
                                        float pz2 = worldBounds.Back - z;
                                        float px1 = 1f - (worldBounds.Left - x);
                                        float px2 = worldBounds.Right - x;

                                        float p = blockShape switch {
                                            BlockShape.Ramp0 => pz1,
                                            BlockShape.Ramp1 => pz2,
                                            BlockShape.Ramp2 => px1,
                                            BlockShape.Ramp3 => px2,
                                            BlockShape.Ramp4 => Math.Min(pz1, px1),
                                            BlockShape.Ramp5 => Math.Min(pz1, px2),
                                            BlockShape.Ramp6 => Math.Min(pz2, px1),
                                            BlockShape.Ramp7 => Math.Min(pz2, px2),
                                            BlockShape.Ramp8 => Math.Max(pz2, px2),
                                            BlockShape.Ramp9 => Math.Max(pz2, px1),
                                            BlockShape.Ramp10 => Math.Max(pz1, px2),
                                            BlockShape.Ramp11 => Math.Max(pz1, px1),

                                            _ => 0f,
                                        };

                                        p = MathHelper.Clamp(p, 0f, 1f);

                                        float edge = (y + p) * terrain.TileSize.Y;

                                        if (body.Position.Y <= edge - body.Bounds.Y) {
                                            verticalTarget = edge;
                                            verticalOverlap = edge - worldBounds.Bottom;

                                            found = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (tileY >= 0) {
                        if (body.Velocity.Y > 0f) {
                            float edge = tileY * terrain.TileSize.Y;
                            verticalTarget = edge - body.Bounds.Height;
                            verticalOverlap = worldBounds.Top - edge;
                        }
                        else {
                            float edge = (tileY + 1) * terrain.TileSize.Y;
                            verticalTarget = edge;
                            verticalOverlap = edge - worldBounds.Bottom;
                        }
                    }
                }

                float horizontalTarget = 0f;
                float horizontalOverlap = float.MaxValue;
                if (doHorizontal && body.Velocity.X != 0f) {
                    GetEdges(worldBounds, new Vector3(body.Velocity.X * deltaTime, 0f, 0f), terrain, out int left, out int top, out int front, out int right, out int bottom, out int back);

                    int startX = left + 1;
                    int endX = right + 1;
                    int directionX = 1;
                    if (body.Velocity.X < 0f) {
                        startX = right - 1;
                        endX = left - 1;
                        directionX = -1;
                    }

                    int startY = top;
                    int endY = bottom + 1;
                    int directionY = 1;
                    if (body.Velocity.Y < 0f) {
                        startY = bottom;
                        endY = top - 1;
                        directionY = -1;
                    }

                    int startZ = front;
                    int endZ = back + 1;
                    int directionZ = 1;
                    if (body.Velocity.Z < 0f) {
                        startZ = back;
                        endZ = front - 1;
                        directionZ = -1;
                    }

                    int tileX = -1;
                    bool found = false;
                    for (int x = startX; x != endX && !found; x += directionX) {
                        for (int y = startY; y != endY && !found; y += directionY) {
                            for (int z = startZ; z != endZ; z += directionZ) {
                                if (IsSolidBlock(x, y, z, terrain) && !IsSolid(x - directionX, y, z, directionX < 0 ? Direction3D.Right : Direction3D.Left, terrain)) {
                                    tileX = x;
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (tileX >= 0) {
                        if (body.Velocity.X > 0f) {
                            float edge = tileX * terrain.TileSize.X;
                            horizontalTarget = edge - body.Bounds.Width;
                            horizontalOverlap = worldBounds.Right - edge;
                        }
                        else {
                            float edge = (tileX + 1) * terrain.TileSize.X;
                            horizontalTarget = edge;
                            horizontalOverlap = edge - worldBounds.Left;
                        }
                    }
                }

                float depthTarget = 0f;
                float depthOverlap = float.MaxValue;
                if (doDepth && body.Velocity.Z != 0f) {
                    GetEdges(worldBounds, new Vector3(0f, 0f, body.Velocity.Z * deltaTime), terrain, out int left, out int top, out int front, out int right, out int bottom, out int back);

                    int startZ = front + 1;
                    int endZ = back + 1;
                    int directionZ = 1;
                    if (body.Velocity.Z < 0f) {
                        startZ = back - 1;
                        endZ = front - 1;
                        directionZ = -1;
                    }

                    int startX = left;
                    int endX = right + 1;
                    int directionX = 1;
                    if (body.Velocity.X < 0f) {
                        startX = right;
                        endX = left - 1;
                        directionX = -1;
                    }

                    int startY = top;
                    int endY = bottom + 1;
                    int directionY = 1;
                    if (body.Velocity.Y < 0f) {
                        startY = bottom;
                        endY = top - 1;
                        directionY = -1;
                    }

                    int tileZ = -1;
                    bool found = false;
                    for (int z = startZ; z != endZ && !found; z += directionZ) {
                        for (int x = startX; x != endX && !found; x += directionX) {
                            for (int y = startY; y != endY; y += directionY) {
                                if (IsSolidBlock(x, y, z, terrain) && !IsSolid(x, y, z - directionZ, directionZ < 0 ? Direction3D.Forwards : Direction3D.Backwards, terrain)) {
                                    tileZ = z;
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (tileZ >= 0) {
                        if (body.Velocity.Z > 0f) {
                            float edge = tileZ * terrain.TileSize.Z;
                            depthTarget = edge - body.Bounds.Depth;
                            depthOverlap = worldBounds.Back - edge;
                        }
                        else {
                            float edge = (tileZ + 1) * terrain.TileSize.Z;
                            depthTarget = edge;
                            depthOverlap = edge - worldBounds.Front;
                        }
                    }
                }

                if (horizontalOverlap < float.MaxValue || verticalOverlap < float.MaxValue || depthOverlap < float.MaxValue) {
                    if (horizontalOverlap <= verticalOverlap && horizontalOverlap <= depthOverlap) {
                        body.Contact = new Vector3(body.Velocity.X > 0f ? 1f : -1f, body.Contact.Y, body.Contact.Z);

                        body.Position = new Vector3(horizontalTarget - body.Bounds.X, body.Position.Y, body.Position.Z);
                        body.Velocity = new Vector3(body.Velocity.X * -body.BounceFactor, body.Velocity.Y, body.Velocity.Z);

                        // Don't do more than one horizontal correction.
                        doHorizontal = false;
                    }
                    else if (verticalOverlap <= depthOverlap) {
                        body.Contact = new Vector3(body.Contact.X, body.Velocity.Y > 0f ? 1f : -1f, body.Contact.Z);

                        body.Position = new Vector3(body.Position.X, verticalTarget - body.Bounds.Y, body.Position.Z);
                        body.Velocity = new Vector3(body.Velocity.X, body.Velocity.Y * -body.BounceFactor, body.Velocity.Z);

                        // Don't do more than one vertical correction.
                        doVertical = false;
                    }
                    else {
                        body.Contact = new Vector3(body.Contact.X, body.Contact.Y, body.Velocity.Z > 0f ? 1f : -1f);

                        body.Position = new Vector3(body.Position.X, body.Position.Y, depthTarget - body.Bounds.Z);
                        body.Velocity = new Vector3(body.Velocity.X, body.Velocity.Y, body.Velocity.Z * -body.BounceFactor);

                        // Don't do more than one depth correction.
                        doDepth = false;
                    }
                }
                else {
                    break;
                }
            }
        }

        private static void GetEdges(Cuboid worldBounds, Vector3 velocity, ITerrain3D terrain, out int left, out int top, out int front, out int right, out int bottom, out int back) {
            Cuboid velocityBounds = worldBounds.Extend(velocity);

            left = (int)Math.Floor(velocityBounds.Left / terrain.TileSize.X);
            top = (int)Math.Floor(velocityBounds.Bottom / terrain.TileSize.Y);
            front = (int)Math.Floor(velocityBounds.Front / terrain.TileSize.Z);
            right = (int)Math.Floor(velocityBounds.Right / terrain.TileSize.X);
            bottom = (int)Math.Floor(velocityBounds.Top / terrain.TileSize.Y);
            back = (int)Math.Floor(velocityBounds.Back / terrain.TileSize.Z);
        }

        private static bool IsSolidBlock(int x, int y, int z, ITerrain3D terrain) {
            bool isSolid = terrain.IsSolid(x, y, z);
            BlockShape blockShape = terrain.GetBlockShape(x, y, z);

            return isSolid && blockShape == BlockShape.Block;
        }

        private static bool IsSolid(int x, int y, int z, Direction3D direction, ITerrain3D terrain) {
            bool isSolid = terrain.IsSolid(x, y, z);
            BlockShape blockShape = terrain.GetBlockShape(x, y, z);

            return isSolid && blockShape.GetCoveredDirections().HasFlag(direction);
        }
    }
}
