using Floppy.Extensions;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xunit;

namespace Floppy.Tests.Extensions {
    public class Vector2ExtensionsTests {
        #region Test Cases

        public static IEnumerable<object[]> GetAngleCases { get; } = new[] {
            new object[] { new Vector2(0f, 0f), float.NaN },
            new object[] { new Vector2(float.NaN, 1f), float.NaN },
            new object[] { new Vector2(1f, float.NaN), float.NaN },
            new object[] { new Vector2(float.PositiveInfinity, 1f), float.NaN },
            new object[] { new Vector2(1f, float.PositiveInfinity), float.NaN },
            new object[] { new Vector2(float.NegativeInfinity, 1f), float.NaN },
            new object[] { new Vector2(1f, float.NegativeInfinity), float.NaN },
            new object[] { new Vector2(1f, 0f), 0f },
            new object[] { new Vector2(1f, 1f), 0.785f },
            new object[] { new Vector2(0f, 1f), 1.571f },
            new object[] { new Vector2(-1f, 1f), 2.356f },
            new object[] { new Vector2(-1f, 0f), 3.142f },
            new object[] { new Vector2(-1f, -1f), 3.927f },
            new object[] { new Vector2(0f, -1f), 4.712f },
            new object[] { new Vector2(1f, -1f), 5.498f },
        }; 

        #endregion

        [Theory, MemberData(nameof(GetAngleCases))]
        public void GetAngle_ValidVector_CorrectAngle(Vector2 vector, float expectedAngle) {
            float actualAngle = Vector2Extensions.GetAngle(vector);

            Assert.Equal(expectedAngle, actualAngle, 3);
        }

        [Fact]
        public void SetX_ValidVector_CopyWithDifferentX() {
            Vector2 original = new Vector2(-10f, 20f);

            Vector2 result = Vector2Extensions.SetX(original, 15f);

            Assert.Equal(new Vector2(15f, 20f), result);
        }

        [Fact]
        public void SetY_ValidVector_CopyWithDifferentY() {
            Vector2 original = new Vector2(-10f, 20f);

            Vector2 result = Vector2Extensions.SetY(original, 15f);

            Assert.Equal(new Vector2(-10f, 15f), result);
        }
    }
}
