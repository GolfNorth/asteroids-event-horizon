using System.Numerics;
using NUnit.Framework;

namespace NonUnity.Collision.Test
{
    [TestFixture]
    public class PointShapeTest
    {
        [Test]
        public void AABB_CreateShape_ValidAABB()
        {
            PointShape pointShape = new PointShape(Vector2.Zero);
            AABB expectedAABB = new AABB {X1 = 0f, X2 = 0f, Y1 = 0f, Y2 = 0f};

            Assert.AreEqual(expectedAABB.X1, pointShape.AABB.X1, 0.01f);
            Assert.AreEqual(expectedAABB.X2, pointShape.AABB.X2, 0.01f);
            Assert.AreEqual(expectedAABB.Y1, pointShape.AABB.Y1, 0.01f);
            Assert.AreEqual(expectedAABB.Y2, pointShape.AABB.Y2, 0.01f);
        }

        [Test]
        public void Set_NewValues_ValidAABB()
        {
            PointShape pointShape = new PointShape(Vector2.Zero);
            AABB expectedAABB = new AABB {X1 = 1f, X2 = 1f, Y1 = 1f, Y2 = 1f};

            pointShape.Set(Vector2.One, 180f);

            Assert.AreEqual(expectedAABB.X1, pointShape.AABB.X1, 0.01f);
            Assert.AreEqual(expectedAABB.X2, pointShape.AABB.X2, 0.01f);
            Assert.AreEqual(expectedAABB.Y1, pointShape.AABB.Y1, 0.01f);
            Assert.AreEqual(expectedAABB.Y2, pointShape.AABB.Y2, 0.01f);
        }
    }
}