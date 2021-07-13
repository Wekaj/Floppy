using Floppy.Utilities;
using Microsoft.Xna.Framework;
using System;

namespace Floppy.Physics {
    public static class BodyPhysics2D {
        public static void UpdateBody(Body2D body, float deltaTime, ITerrain2D? terrain = null) {
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

        public static void ResetContact(Body2D body) {
            body.Contact = Vector2.Zero;
        }

        public static void ApplyGravity(Body2D body, float deltaTime) {
            body.Velocity += body.Gravity * deltaTime;
        }

        public static void ApplyForce(Body2D body, float deltaTime) {
            body.Velocity += body.Force * deltaTime / body.Mass;
            body.Force = Vector2.Zero;
        }

        public static void ApplyImpulse(Body2D body) {
            body.Velocity += body.Impulse / body.Mass;
            body.Impulse = Vector2.Zero;
        }

        public static void CapVelocity(Body2D body) {
            float speed = body.Velocity.Length();
            if (speed > body.MaxSpeed) {
                body.Velocity = body.Velocity * body.MaxSpeed / speed;
            }
        }

        public static void ApplyVelocity(Body2D body, float deltaTime) {
            body.Position += body.Velocity * deltaTime;
        }

        public static void ApplyFriction(Body2D body, float deltaTime) {
            body.Velocity /= 1f + body.Friction * deltaTime;
        }

        public static void DoTileCollisions(Body2D body, float deltaTime, ITerrain2D terrain) {
            // A bit of a mess.
            while (true) {
                RectangleF worldBounds = body.Bounds.Offset(body.Position).Shrink(terrain.EdgeThreshold);
                RectangleF velocityBounds = worldBounds.Extend(body.Velocity * deltaTime);

                int left = (int)Math.Floor(velocityBounds.Left / terrain.TileSize);
                int top = (int)Math.Floor(velocityBounds.Top / terrain.TileSize);
                int right = (int)Math.Floor(velocityBounds.Right / terrain.TileSize);
                int bottom = (int)Math.Floor(velocityBounds.Bottom / terrain.TileSize);

                float verticalTarget = 0f;
                float verticalOverlap = float.MaxValue;
                if (body.Velocity.Y != 0f) {
                    int startY = top + 1;
                    int endY = bottom + 1;
                    int direction = 1;
                    if (body.Velocity.Y < 0f) {
                        startY = bottom - 1;
                        endY = top - 1;
                        direction = -1;
                    }

                    int tileY = -1;
                    for (int y = startY; y != endY && tileY < 0; y += direction) {
                        for (int x = left; x <= right; x++) {
                            if (terrain.IsSolid(x, y) && !terrain.IsSolid(x, y - direction)) {
                                tileY = y;
                                break;
                            }
                        }
                    }

                    if (tileY >= 0) {
                        if (body.Velocity.Y > 0f) {
                            float edge = tileY * terrain.TileSize;
                            verticalTarget = edge - body.Bounds.Height;
                            verticalOverlap = worldBounds.Bottom - edge;
                        }
                        else {
                            float edge = (tileY + 1) * terrain.TileSize;
                            verticalTarget = edge;
                            verticalOverlap = edge - worldBounds.Top;
                        }
                    }
                }

                float horizontalTarget = 0f;
                float horizontalOverlap = float.MaxValue;
                if (body.Velocity.X != 0f) {
                    int startX = left + 1;
                    int endX = right + 1;
                    int direction = 1;
                    if (body.Velocity.X < 0f) {
                        startX = right - 1;
                        endX = left - 1;
                        direction = -1;
                    }

                    int tileX = -1;
                    for (int x = startX; x != endX && tileX < 0; x += direction) {
                        for (int y = top; y <= bottom; y++) {
                            if (terrain.IsSolid(x, y) && !terrain.IsSolid(x - direction, y)) {
                                tileX = x;
                                break;
                            }
                        }
                    }

                    if (tileX >= 0) {
                        if (body.Velocity.X > 0f) {
                            float edge = tileX * terrain.TileSize;
                            horizontalTarget = edge - body.Bounds.Width;
                            horizontalOverlap = worldBounds.Right - edge;
                        }
                        else {
                            float edge = (tileX + 1) * terrain.TileSize;
                            horizontalTarget = edge;
                            horizontalOverlap = edge - worldBounds.Left;
                        }
                    }
                }

                if (horizontalOverlap < float.MaxValue || verticalOverlap < float.MaxValue) {
                    if (horizontalOverlap <= verticalOverlap) {
                        body.Contact = new Vector2(body.Velocity.X > 0f ? 1f : -1f, body.Contact.Y);

                        body.Position = new Vector2(horizontalTarget - body.Bounds.X, body.Position.Y);
                        body.Velocity = new Vector2(body.Velocity.X * -body.BounceFactor, body.Velocity.Y);
                    }
                    else {
                        body.Contact = new Vector2(body.Contact.X, body.Velocity.Y > 0f ? 1f : -1f);

                        body.Position = new Vector2(body.Position.X, verticalTarget - body.Bounds.Y);
                        body.Velocity = new Vector2(body.Velocity.X, body.Velocity.Y * -body.BounceFactor);
                    }
                }
                else {
                    break;
                }
            }
        }
    }
}
