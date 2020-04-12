using Floppy.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Xunit;

namespace Floppy.Tests.Extensions {
    public class PointExtensionsTests {
        #region Test Cases

        public static IEnumerable<object[]> ClampCases { get; } = new[] {
            new object[] { new Point(0, 0), 0, 0, 0, 0, new Point(0, 0) },
            new object[] { new Point(10, 20), 0, 0, 5, 15, new Point(5, 15) },
            new object[] { new Point(10, 20), 15, 25, 100, 100, new Point(15, 25) },
        };

        public static IEnumerable<object[]> ManhattenDistanceCases { get; } = new[] {
            new object[] { new Point(0, 0), new Point(0, 0), 0 },
            new object[] { new Point(15, 15), new Point(15, 15), 0 },
            new object[] { new Point(5, 0), new Point(15, 0), 10 },
            new object[] { new Point(0, 5), new Point(0, 15), 10 },
            new object[] { new Point(5, 5), new Point(15, 15), 20 },
            new object[] { new Point(-10, 5), new Point(20, -30), 65 },
        }; 

        #endregion

        [Theory, MemberData(nameof(ClampCases))]
        public void Clamp_Ints_ClampedPoint(Point initialPoint, int minX, int minY, int maxX, int maxY, Point expectedPoint) {
            Point clampedPoint = PointExtensions.Clamp(initialPoint, minX, minY, maxX, maxY);

            Assert.Equal(expectedPoint, clampedPoint);
        }

        [Theory, MemberData(nameof(ClampCases))]
        public void Clamp_Points_ClampedPoint(Point initialPoint, int minX, int minY, int maxX, int maxY, Point expectedPoint) {
            Point topLeft = new Point(minX, minY);
            Point bottomRight = new Point(maxX, maxY);

            Point clampedPoint = PointExtensions.Clamp(initialPoint, topLeft, bottomRight);

            Assert.Equal(expectedPoint, clampedPoint);
        }

        [Fact]
        public void Clamp_MinXGreaterThanMaxX_ArgumentException() {
            Assert.Throws<ArgumentException>(() => PointExtensions.Clamp(new Point(0, 0), 20, 0, 10, 0));
        }

        [Fact]
        public void Clamp_MinYGreaterThanMaxY_ArgumentException() {
            Assert.Throws<ArgumentException>(() => PointExtensions.Clamp(new Point(0, 0), 0, 20, 0, 10));
        }

        [Theory, MemberData(nameof(ManhattenDistanceCases))]
        public void ManhattenDistance_TwoPoints_CorrectDistance(Point point1, Point point2, int expectedDistance) {
            int distance = PointExtensions.ManhattenDistance(point1, point2);

            Assert.Equal(expectedDistance, distance);
        }
    }
}
