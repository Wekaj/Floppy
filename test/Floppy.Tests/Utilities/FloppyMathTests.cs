using Floppy.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xunit;

namespace Floppy.Tests.Utilities {
    public class FloppyMathTests {
        #region Test Cases

        public static IEnumerable<object[]> VectorFromAngleCases { get; } = new[] {
            new object[] { float.NaN, new Vector2(float.NaN, float.NaN) },
            new object[] { float.PositiveInfinity, new Vector2(float.NaN, float.NaN) },
            new object[] { float.NegativeInfinity, new Vector2(float.NaN, float.NaN) },
            new object[] { 0f, new Vector2(1f, 0f) },
            new object[] { 0.785f, new Vector2(0.707f, 0.707f) },
            new object[] { 1.571f, new Vector2(0f, 1f) },
            new object[] { 2.356f, new Vector2(-0.707f, 0.707f) },
            new object[] { 3.142f, new Vector2(-1f, 0f) },
            new object[] { 3.927f, new Vector2(-0.707f, -0.707f) },
            new object[] { 4.712f, new Vector2(0f, -1f) },
            new object[] { 5.498f, new Vector2(0.707f, -0.707f) },
        };

        #endregion

        [Theory, MemberData(nameof(VectorFromAngleCases))]
        public void VectorFromAngle_ValidAngle_CorrectUnitVector(float angle, Vector2 expectedVector) {
            Vector2 actualVector = FloppyMath.VectorFromAngle(angle);

            Assert.Equal(expectedVector.X, actualVector.X, 3);
            Assert.Equal(expectedVector.Y, actualVector.Y, 3);
        }
    }
}
