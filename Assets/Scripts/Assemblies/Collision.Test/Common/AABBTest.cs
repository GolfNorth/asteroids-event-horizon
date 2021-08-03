using NUnit.Framework;

namespace NonUnity.Collision.Test
{
    [TestFixture]
    public sealed class AABBTest
    {
        [Test]
        public void Intersect_NonIntersecting_ReturnFalse()
        {
            AABB firstAABB = new AABB {X1 = 0, X2 = 1, Y1 = 0, Y2 = 1};
            AABB secondAABB = new AABB {X1 = 2, X2 = 3, Y1 = 2, Y2 = 3};

            bool intersect = firstAABB.Intersect(secondAABB);

            Assert.IsFalse(intersect);
        }

        [Test]
        public void Intersect_Intersecting_ReturnTrue()
        {
            AABB firstAABB = new AABB {X1 = 0, X2 = 2, Y1 = 0, Y2 = 2};
            AABB secondAABB = new AABB {X1 = 1, X2 = 3, Y1 = 1, Y2 = 3};

            bool intersect = firstAABB.Intersect(secondAABB);

            Assert.IsTrue(intersect);
        }
    }
}