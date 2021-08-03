using System.Numerics;
using NUnit.Framework;

namespace NonUnity.Collision.Test
{
    [TestFixture]
    public sealed class PolygonShapeTest
    {
        [Test]
        public void AABB_CreateShape_ValidAABB()
        {
            PolygonShape polygonShape = new PolygonShape(Vector2.Zero, new[]
            {
                new Vector2(-3, 0), new Vector2(0, 3),
                new Vector2(3, 0), new Vector2(0, -3)
            });
            AABB expectedAABB = new AABB {X1 = -3, X2 = 3, Y1 = -3, Y2 = 3};

            Assert.AreEqual(expectedAABB.X1, polygonShape.AABB.X1, 0.01f);
            Assert.AreEqual(expectedAABB.X2, polygonShape.AABB.X2, 0.01f);
            Assert.AreEqual(expectedAABB.Y1, polygonShape.AABB.Y1, 0.01f);
            Assert.AreEqual(expectedAABB.Y2, polygonShape.AABB.Y2, 0.01f);
        }

        [Test]
        public void Set_NewValues_ValidAABB()
        {
            PolygonShape polygonShape = new PolygonShape(Vector2.Zero, new[]
            {
                new Vector2(-3, 0), new Vector2(0, 3),
                new Vector2(3, 0), new Vector2(0, -3)
            });
            AABB expectedAABB = new AABB {X1 = -2, X2 = 4, Y1 = -2, Y2 = 4};

            polygonShape.Set(Vector2.One, -90f);

            Assert.AreEqual(expectedAABB.X1, polygonShape.AABB.X1, 0.01f);
            Assert.AreEqual(expectedAABB.X2, polygonShape.AABB.X2, 0.01f);
            Assert.AreEqual(expectedAABB.Y1, polygonShape.AABB.Y1, 0.01f);
            Assert.AreEqual(expectedAABB.Y2, polygonShape.AABB.Y2, 0.01f);
        }
    }
}