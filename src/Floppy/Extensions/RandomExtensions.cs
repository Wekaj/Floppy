using Floppy.Utilities;
using Microsoft.Xna.Framework;
using System;

namespace Floppy.Extensions {
    public static class RandomExtensions {
        public static Vector2 NextUnitVector(this Random random) {
            float angle = random.NextAngle();

            return FloppyMath.VectorFromAngle(angle);
        }

        #region Internal Methods

        internal static float NextSingle(this Random random) {
            return (float)random.NextDouble();
        }

        internal static float NextSingle(this Random random, float maxValue) {
            return random.NextSingle() * maxValue;
        }

        internal static float NextSingle(this Random random, float minValue, float maxValue) {
            return minValue + random.NextSingle(maxValue - minValue);
        }

        internal static float NextAngle(this Random random) {
            return random.NextSingle(MathHelper.TwoPi);
        }

        internal static float NextAngle(this Random random, float minAngle, float maxAngle) {
            while (minAngle < maxAngle - MathHelper.TwoPi) {
                minAngle += MathHelper.TwoPi;
            }

            while (maxAngle < minAngle) {
                maxAngle += MathHelper.TwoPi;
            }

            return random.NextSingle(minAngle, maxAngle);
        } 

        #endregion
    }
}
