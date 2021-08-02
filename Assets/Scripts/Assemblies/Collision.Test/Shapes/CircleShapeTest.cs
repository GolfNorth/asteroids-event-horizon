using System.Numerics;
using NUnit.Framework;

namespace NonUnity.Collision.Test
{
    [TestFixture]
    public class CircleShapeTest
    {
        [Test]
        public void AABB_CreateShape_ValidAABB()
        {
            CircleShape circleShape = new CircleShape(Vector2.Zero, 3f);
            AABB expectedAABB = new AABB {X1 = -3f, X2 = 3f, Y1 = -3f, Y2 = 3f};

            Assert.AreEqual(expectedAABB.X1, circleShape.AABB.X1, 0.01f);
            Assert.AreEqual(expectedAABB.X2, circleShape.AABB.X2, 0.01f);
            Assert.AreEqual(expectedAABB.Y1, circleShape.AABB.Y1, 0.01f);
            Assert.AreEqual(expectedAABB.Y2, circleShape.AABB.Y2, 0.01f);
        }

        [Test]
        public void Set_NewValues_ValidAABB()
        {
            CircleShape circleShape = new CircleShape(Vector2.Zero, 3f);
            AABB expectedAABB = new AABB {X1 = -2f, X2 = 4f, Y1 = -2f, Y2 = 4f};

            circleShape.Set(Vector2.One, 180f);

            Assert.AreEqual(expectedAABB.X1, circleShape.AABB.X1, 0.01f);
            Assert.AreEqual(expectedAABB.X2, circleShape.AABB.X2, 0.01f);
            Assert.AreEqual(expectedAABB.Y1, circleShape.AABB.Y1, 0.01f);
            Assert.AreEqual(expectedAABB.Y2, circleShape.AABB.Y2, 0.01f);
        }
    }
}