using Floppy.Utilities;
using Microsoft.Xna.Framework;
using System;

namespace Floppy.Physics {
    public static class BodyPhysics {
        public static void UpdateBody(Body body, float deltaTime, TileMap? tileMap = null) {
            ApplyGravity(body, deltaTime);
            ApplyForce(body, deltaTime);
            ApplyImpulse(body);

            CapVelocity(body);

            ResetContact(body);
            if (tileMap != null) {
                DoTileCollisions(body, deltaTime, tileMap);
            }

            ApplyVelocity(body, deltaTime);

            ApplyFriction(body, deltaTime);
        }

        private static void ApplyGravity(Body body, float deltaTime) {
            body.Velocity += body.Gravity * deltaTime;
        }

        private static void ApplyForce(Body body, float deltaTime) {
            body.Velocity += body.Force * deltaTime / body.Mass;
            body.Force = Vector2.Zero;
        }

        private static void ApplyImpulse(Body body) {
            body.Velocity += body.Impulse / body.Mass;
            body.Impulse = Vector2.Zero;
        }

        private static void CapVelocity(Body body) {
            float speed = body.Velocity.Length();
            if (speed > body.MaxSpeed) {
                body.Velocity = body.Velocity * body.MaxSpeed / speed;
            }
        }

        private static void ResetContact(Body body) {
            body.Contact = Vector2.Zero;
        }

        private static void ApplyVelocity(Body body, float deltaTime) {
            body.Position += body.Velocity * deltaTime;
        }

        private static void ApplyFriction(Body body, float deltaTime) {
            body.Velocity /= 1f + body.Friction * deltaTime;
        }

        private static void DoTileCollisions(Body body, float deltaTime, TileMap tileMap) {
            // A bit of a mess.
            while (true) {
                RectangleF worldBounds = body.Bounds.Offset(body.Position).Shrink(tileMap.EdgeThreshold);
                RectangleF velocityBounds = worldBounds.Extend(body.Velocity * deltaTime);

                int left = (int)Math.Floor(velocityBounds.Left / tileMap.TileSize);
                int top = (int)Math.Floor(velocityBounds.Top / tileMap.TileSize);
                int right = (int)Math.Floor(velocityBounds.Right / tileMap.TileSize);
                int bottom = (int)Math.Floor(velocityBounds.Bottom / tileMap.TileSize);

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
                            bool considerPlatform = direction > 0 && worldBounds.Bottom <= y * tileMap.TileSize;

                            if (tileMap.IsSolid(x, y, considerPlatform) && !tileMap.IsSolid(x, y - direction)) {
                                tileY = y;
                                break;
                            }
                        }
                    }

                    if (tileY >= 0) {
                        if (body.Velocity.Y > 0f) {
                            float edge = tileY * tileMap.TileSize;
                            verticalTarget = edge - body.Bounds.Height;
                            verticalOverlap = worldBounds.Bottom - edge;
                        }
                        else {
                            float edge = (tileY + 1) * tileMap.TileSize;
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
                            if (tileMap.IsSolid(x, y) && !tileMap.IsSolid(x - direction, y)) {
                                tileX = x;
                                break;
                            }
                        }
                    }

                    if (tileX >= 0) {
                        if (body.Velocity.X > 0f) {
                            float edge = tileX * tileMap.TileSize;
                            horizontalTarget = edge - body.Bounds.Width;
                            horizontalOverlap = worldBounds.Right - edge;
                        }
                        else {
                            float edge = (tileX + 1) * tileMap.TileSize;
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
