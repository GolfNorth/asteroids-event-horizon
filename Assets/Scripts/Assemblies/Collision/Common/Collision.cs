using System;
using System.Numerics;

namespace NonUnity.Collision
{
    /// <summary>
    /// Обработчик пересечений форм
    /// </summary>
    public static class Collision
    {
        #region Const

        private enum ContactMatch : byte
        {
            PointPoint = (PointShape.match << 2) + PointShape.match,
            PointCircle = (PointShape.match << 2) + CircleShape.match,
            PointEdge = (PointShape.match << 2) + EdgeShape.match,
            PointPolygon = (PointShape.match << 2) + PolygonShape.match,
            CirclePoint = (CircleShape.match << 2) + PointShape.match,
            CircleCircle = (CircleShape.match << 2) + CircleShape.match,
            CircleEdge = (CircleShape.match << 2) + EdgeShape.match,
            CirclePolygon = (CircleShape.match << 2) + PolygonShape.match,
            EdgePoint = (EdgeShape.match << 2) + PointShape.match,
            EdgeCircle = (EdgeShape.match << 2) + CircleShape.match,
            EdgeEdge = (EdgeShape.match << 2) + EdgeShape.match,
            EdgePolygon = (EdgeShape.match << 2) + PolygonShape.match,
            PolygonPoint = (PolygonShape.match << 2) + PointShape.match,
            PolygonCircle = (PolygonShape.match << 2) + CircleShape.match,
            PolygonEdge = (PolygonShape.match << 2) + EdgeShape.match,
            PolygonPolygon = (PolygonShape.match << 2) + PolygonShape.match
        }

        #endregion

        #region Public

        public static bool Intersect(this IShape shapeA, IShape shapeB)
        {
            if (!shapeA.AABB.Intersect(shapeB.AABB))
            {
                return false;
            }

            int match = (shapeA.Match << 2) + shapeB.Match;

            switch ((ContactMatch) match)
            {
                case ContactMatch.PointCircle:
                    return PointAndCircleContact(((PointShape) shapeA).Position, (CircleShape) shapeB);
                case ContactMatch.PointEdge:
                    return EdgeAndPointContact((EdgeShape) shapeB, ((PointShape) shapeA).Position);
                case ContactMatch.PointPolygon:
                    return PolygonAndPointContact((PolygonShape) shapeB, ((PointShape) shapeA).Position);
                case ContactMatch.CirclePoint:
                    return PointAndCircleContact(((PointShape) shapeB).Position, (CircleShape) shapeA);
                case ContactMatch.CircleEdge:
                    return EdgeAndCircleContact((EdgeShape) shapeB, (CircleShape) shapeA);
                case ContactMatch.CirclePolygon:
                    return PolygonAndCircleContact((PolygonShape) shapeB, (CircleShape) shapeA);
                case ContactMatch.EdgePoint:
                    return EdgeAndPointContact((EdgeShape) shapeA, ((PointShape) shapeB).Position);
                case ContactMatch.EdgeCircle:
                    return EdgeAndCircleContact((EdgeShape) shapeA, (CircleShape) shapeB);
                case ContactMatch.EdgeEdge:
                    return EdgeAndEdgeContact((EdgeShape) shapeA, (EdgeShape) shapeB);
                case ContactMatch.PolygonPoint:
                    return PolygonAndPointContact((PolygonShape) shapeA, ((PointShape) shapeB).Position);
                case ContactMatch.PolygonCircle:
                    return PolygonAndCircleContact((PolygonShape) shapeA, (CircleShape) shapeB);
                case ContactMatch.PolygonEdge:
                    return PolygonAndEdgeContact((PolygonShape) shapeA, (EdgeShape) shapeB);
                case ContactMatch.PolygonPolygon:
                    return PolygonAndPolygonContact((PolygonShape) shapeA, (PolygonShape) shapeB);
            }

            throw new NotImplementedException();
        }

        #endregion

        #region Private

        private static bool PointAndCircleContact(Vector2 point, CircleShape circle)
        {
            float distance = Vector2.DistanceSquared(point, circle.Position);

            return distance <= circle.Radius * circle.Radius;
        }

        private static bool EdgeAndEdgeContact(EdgeShape edgeA, EdgeShape edgeB)
        {
            float uA = ((edgeB.PointB.X - edgeB.PointA.X) * (edgeA.PointA.Y - edgeB.PointA.Y)
                        - (edgeB.PointB.Y - edgeB.PointA.Y) * (edgeA.PointA.X - edgeB.PointA.X))
                       / ((edgeB.PointB.Y - edgeB.PointA.Y) * (edgeA.PointB.X - edgeA.PointA.X)
                          - (edgeB.PointB.X - edgeB.PointA.X) * (edgeA.PointB.Y - edgeA.PointA.Y));
            float uB = ((edgeA.PointB.X - edgeA.PointA.X) * (edgeA.PointA.Y - edgeB.PointA.Y)
                        - (edgeA.PointB.Y - edgeA.PointA.Y) * (edgeA.PointA.X - edgeB.PointA.X))
                       / ((edgeB.PointB.Y - edgeB.PointA.Y) * (edgeA.PointB.X - edgeA.PointA.X)
                          - (edgeB.PointB.X - edgeB.PointA.X) * (edgeA.PointB.Y - edgeA.PointA.Y));

            return uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1;
        }

        private static bool EdgeAndPointContact(EdgeShape edge, Vector2 point)
        {
            float d1 = Vector2.DistanceSquared(point, edge.PointA);
            float d2 = Vector2.DistanceSquared(point, edge.PointB);
            float edgeLenght = Vector2.DistanceSquared(edge.PointA, edge.PointB);

            return Math.Abs(d1 + d2 - edgeLenght) < 0.01f;
        }

        private static bool EdgeAndCircleContact(EdgeShape edge, CircleShape circle)
        {
            bool inside1 = PointAndCircleContact(edge.PointA, circle);
            bool inside2 = PointAndCircleContact(edge.PointB, circle);

            if (inside1 || inside2)
            {
                return true;
            }

            float dot = ((circle.Position.X - edge.PointA.X) * (edge.PointB.X - edge.PointA.X)
                         + (circle.Position.Y - edge.PointA.Y) * (edge.PointB.Y - edge.PointA.Y))
                        / Vector2.DistanceSquared(edge.PointA, edge.PointB);

            Vector2 closest = edge.PointA + (edge.PointB - edge.PointB) * dot;
            bool onSegment = EdgeAndPointContact(edge, closest);

            if (!onSegment)
            {
                return false;
            }

            float distance = Vector2.DistanceSquared(closest, circle.Position);

            return distance <= circle.Radius * circle.Radius;
        }

        private static bool PolygonAndPointContact(PolygonShape polygon, Vector2 point)
        {
            bool collision = false;

            for (int current = 0; current < polygon.Points.Length; current++)
            {
                int next = current + 1;

                if (next == polygon.Points.Length)
                {
                    next = 0;
                }

                Vector2 vc = polygon.Points[current];
                Vector2 vn = polygon.Points[next];

                if ((vc.Y >= point.Y && vn.Y < point.Y || vc.Y < point.Y && vn.Y >= point.Y) &&
                    point.X < (vn.X - vc.X) * (point.Y - vc.Y) / (vn.Y - vc.Y) + vc.X)
                {
                    collision = !collision;
                }
            }

            return collision;
        }

        private static bool PolygonAndEdgeContact(PolygonShape polygon, EdgeShape edge)
        {
            for (int current = 0; current < polygon.Points.Length; current++)
            {
                int next = current + 1;

                if (next == polygon.Points.Length)
                {
                    next = 0;
                }

                EdgeShape polygonEdge = new EdgeShape(polygon.Points[current], polygon.Points[next]);
                bool hit = EdgeAndEdgeContact(edge, polygonEdge);

                if (hit)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool PolygonAndPolygonContact(PolygonShape polygonA, PolygonShape polygonB)
        {
            for (int current = 0; current < polygonA.Points.Length; current++)
            {
                int next = current + 1;

                if (next == polygonA.Points.Length)
                {
                    next = 0;
                }

                EdgeShape edgeA = new EdgeShape(polygonA.Points[current], polygonA.Points[next]);
                bool collision = PolygonAndEdgeContact(polygonB, edgeA);

                if (collision)
                {
                    return true;
                }
            }

            bool pointInside = PolygonAndPointContact(polygonA, polygonB.Points[0]);

            return pointInside;
        }

        private static bool PolygonAndCircleContact(PolygonShape polygon, CircleShape circle)
        {
            for (int current = 0; current < polygon.Points.Length; current++)
            {
                int next = current + 1;

                if (next == polygon.Points.Length)
                {
                    next = 0;
                }

                EdgeShape edge = new EdgeShape(polygon.Points[current], polygon.Points[next]);
                bool collision = EdgeAndCircleContact(edge, circle);

                if (collision)
                {
                    return true;
                }
            }

            bool positionInside = PolygonAndPointContact(polygon, circle.Position);

            return positionInside;
        }

        #endregion
    }
}