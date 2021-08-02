using NUnit.Framework;
using Vector2 = System.Numerics.Vector2;

namespace NonUnity.Collision.Test
{
    [TestFixture]
    public class EdgeShapeTest
    {
        [Test]
        public void AABB_CreateShape_ValidAABB()
        {
            EdgeShape edgeShape = new EdgeShape(Vector2.Zero, new Vector2(-3f, -3f), new Vector2(3f, 3f));
            AABB expectedAABB = new AABB {X1 = -3f, X2 = 3f, Y1 = -3f, Y2 = 3f};

            Assert.AreEqual(expectedAABB.X1, edgeShape.AABB.X1, 0.01f);
            Assert.AreEqual(expectedAABB.X2, edgeShape.AABB.X2, 0.01f);
            Assert.AreEqual(expectedAABB.Y1, edgeShape.AABB.Y1, 0.01f);
            Assert.AreEqual(expectedAABB.Y2, edgeShape.AABB.Y2, 0.01f);
        }

        [Test]
        public void Set_NewValues_ValidAABB()
        {
            EdgeShape edgeShape = new EdgeShape(Vector2.Zero, Vector2.Zero, new Vector2(0f, 3f));
            AABB expectedAABB = new AABB {X1 = 1f, X2 = 4f, Y1 = 1f, Y2 = 1f};

            edgeShape.Set(Vector2.One, -90f);

            Assert.AreEqual(expectedAABB.X1, edgeShape.AABB.X1, 0.01f);
            Assert.AreEqual(expectedAABB.X2, edgeShape.AABB.X2, 0.01f);
            Assert.AreEqual(expectedAABB.Y1, edgeShape.AABB.Y1, 0.01f);
            Assert.AreEqual(expectedAABB.Y2, edgeShape.AABB.Y2, 0.01f);
        }
    }
}