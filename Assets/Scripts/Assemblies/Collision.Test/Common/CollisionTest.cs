using System.Numerics;
using NUnit.Framework;

namespace NonUnity.Collision.Test
{
    [TestFixture]
    public class CollisionTest
    {
        [Test]
        public void Intersect_CircleAndPoint_ReturnTrue()
        {
            CircleShape circleShape = new CircleShape(Vector2.Zero, 3f);
            PointShape pointShape = new PointShape(new Vector2(3f, 3f));

            bool intersectA = circleShape.Intersect(pointShape);
            bool intersectB = pointShape.Intersect(circleShape);

            Assert.IsTrue(intersectA);
            Assert.IsTrue(intersectB);
        }

        [Test]
        public void Intersect_CircleAndPoint_ReturnFalse()
        {
            CircleShape circleShape = new CircleShape(Vector2.Zero, 3f);
            PointShape pointShape = new PointShape(new Vector2(4f, 4f));

            bool intersectA = circleShape.Intersect(pointShape);
            bool intersectB = pointShape.Intersect(circleShape);

            Assert.IsFalse(intersectA);
            Assert.IsFalse(intersectB);
        }

        [Test]
        public void Intersect_CircleAndEdge_ReturnTrue()
        {
            CircleShape circleShape = new CircleShape(Vector2.Zero, 3f);
            EdgeShape edgeShape = new EdgeShape(Vector2.Zero, new Vector2(-3f, 3f), new Vector2(3f, 3f));

            bool intersectA = circleShape.Intersect(edgeShape);
            bool intersectB = edgeShape.Intersect(circleShape);

            Assert.IsTrue(intersectA);
            Assert.IsTrue(intersectB);
        }

        [Test]
        public void Intersect_CircleAndEdge_ReturnFalse()
        {
            CircleShape circleShape = new CircleShape(Vector2.Zero, 3f);
            EdgeShape edgeShape = new EdgeShape(Vector2.Zero, new Vector2(-3f, 3.1f), new Vector2(3f, 3.1f));

            bool intersectA = circleShape.Intersect(edgeShape);
            bool intersectB = edgeShape.Intersect(circleShape);

            Assert.IsFalse(intersectA);
            Assert.IsFalse(intersectB);
        }

        [Test]
        public void Intersect_CircleAndPolygon_ReturnTrue()
        {
            CircleShape circleShape = new CircleShape(Vector2.Zero, 3f);
            PolygonShape polygonShape = new PolygonShape(Vector2.Zero, new[]
            {
                new Vector2(-3f, 6f), new Vector2(0f, 9f),
                new Vector2(3f, 6f), new Vector2(0f, 3f)
            });

            bool intersectA = circleShape.Intersect(polygonShape);
            bool intersectB = polygonShape.Intersect(circleShape);

            Assert.IsTrue(intersectA);
            Assert.IsTrue(intersectB);
        }

        [Test]
        public void Intersect_CircleAndPolygon_ReturnFalse()
        {
            CircleShape circleShape = new CircleShape(Vector2.Zero, 3f);
            PolygonShape polygonShape = new PolygonShape(Vector2.Zero, new[]
            {
                new Vector2(-3f, 6f), new Vector2(0f, 9f),
                new Vector2(3f, 6f), new Vector2(0f, 3.1f)
            });

            bool intersectA = circleShape.Intersect(polygonShape);
            bool intersectB = polygonShape.Intersect(circleShape);

            Assert.IsFalse(intersectA);
            Assert.IsFalse(intersectB);
        }

        [Test]
        public void Intersect_EdgeAndPoint_ReturnTrue()
        {
            EdgeShape edgeShape = new EdgeShape(Vector2.Zero, new Vector2(-3f, 0f), new Vector2(3f, 0f));
            PointShape pointShape = new PointShape(Vector2.Zero);

            bool intersectA = edgeShape.Intersect(pointShape);
            bool intersectB = pointShape.Intersect(edgeShape);

            Assert.IsTrue(intersectA);
            Assert.IsTrue(intersectB);
        }

        [Test]
        public void Intersect_EdgeAndPoint_ReturnFalse()
        {
            EdgeShape edgeShape = new EdgeShape(Vector2.Zero, new Vector2(-3f, 0f), new Vector2(3f, 0f));
            PointShape pointShape = new PointShape(new Vector2(0f, 0.1f));

            bool intersectA = edgeShape.Intersect(pointShape);
            bool intersectB = pointShape.Intersect(edgeShape);

            Assert.IsFalse(intersectA);
            Assert.IsFalse(intersectB);
        }

        [Test]
        public void Intersect_EdgeAndEdge_ReturnTrue()
        {
            EdgeShape edgeShapeA = new EdgeShape(Vector2.Zero, new Vector2(-3f, -3f), new Vector2(3f, 3f));
            EdgeShape edgeShapeB = new EdgeShape(Vector2.Zero, new Vector2(-3f, 3f), new Vector2(3f, -3f));

            bool intersectA = edgeShapeA.Intersect(edgeShapeB);
            bool intersectB = edgeShapeB.Intersect(edgeShapeA);

            Assert.IsTrue(intersectA);
            Assert.IsTrue(intersectB);
        }

        [Test]
        public void Intersect_EdgeAndEdge_ReturnFalse()
        {
            EdgeShape edgeShapeA = new EdgeShape(Vector2.Zero, new Vector2(-3f, -3f), new Vector2(3f, -3f));
            EdgeShape edgeShapeB = new EdgeShape(Vector2.Zero, new Vector2(-3f, 3f), new Vector2(3f, 3f));

            bool intersectA = edgeShapeA.Intersect(edgeShapeB);
            bool intersectB = edgeShapeB.Intersect(edgeShapeA);

            Assert.IsFalse(intersectA);
            Assert.IsFalse(intersectB);
        }

        [Test]
        public void Intersect_EdgeAndPolygon_ReturnTrue()
        {
            EdgeShape edgeShape = new EdgeShape(Vector2.Zero, new Vector2(-3f, 3f), new Vector2(3f, 3f));
            PolygonShape polygonShape = new PolygonShape(Vector2.Zero, new[]
            {
                new Vector2(-3f, 6f), new Vector2(0f, 9f),
                new Vector2(3f, 6f), new Vector2(0f, 3f)
            });

            bool intersectA = edgeShape.Intersect(polygonShape);
            bool intersectB = polygonShape.Intersect(edgeShape);

            Assert.IsTrue(intersectA);
            Assert.IsTrue(intersectB);
        }

        [Test]
        public void Intersect_EdgeAndPolygon_ReturnFalse()
        {
            EdgeShape edgeShape = new EdgeShape(Vector2.Zero, new Vector2(-3f, 3f), new Vector2(3f, 3f));
            PolygonShape polygonShape = new PolygonShape(Vector2.Zero, new[]
            {
                new Vector2(-3f, 6f), new Vector2(0f, 9f),
                new Vector2(3f, 6f), new Vector2(0f, 3.1f)
            });

            bool intersectA = edgeShape.Intersect(polygonShape);
            bool intersectB = polygonShape.Intersect(edgeShape);

            Assert.IsFalse(intersectA);
            Assert.IsFalse(intersectB);
        }

        [Test]
        public void Intersect_PolygonAndPoint_ReturnTrue()
        {
            PolygonShape polygonShape = new PolygonShape(Vector2.Zero, new[]
            {
                new Vector2(-3f, 6f), new Vector2(0f, 9f),
                new Vector2(3f, 6f), new Vector2(0f, 3f)
            });
            PointShape pointShape = new PointShape(new Vector2(0f, 6f));

            bool intersectA = polygonShape.Intersect(pointShape);
            bool intersectB = pointShape.Intersect(polygonShape);

            Assert.IsTrue(intersectA);
            Assert.IsTrue(intersectB);
        }

        [Test]
        public void Intersect_PolygonAndPoint_ReturnFalse()
        {
            PolygonShape polygonShape = new PolygonShape(Vector2.Zero, new[]
            {
                new Vector2(-3f, 6f), new Vector2(0f, 9f),
                new Vector2(3f, 6f), new Vector2(0f, 3f)
            });
            PointShape pointShape = new PointShape(new Vector2(0f, 9.1f));

            bool intersectA = polygonShape.Intersect(pointShape);
            bool intersectB = pointShape.Intersect(polygonShape);

            Assert.IsFalse(intersectA);
            Assert.IsFalse(intersectB);
        }
    }
}