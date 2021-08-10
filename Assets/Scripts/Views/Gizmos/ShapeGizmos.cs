using UnityEngine;
#if UNITY_EDITOR
using Asteroids.Common;
using NonUnity.Collision;
using NonUnity.Ecs;
using NonUnity.Game;

#endif

namespace Asteroids.Views
{
    /// <summary>
    /// Отрисовщик фигур
    /// </summary>
    public sealed class ShapeGizmos : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField, Tooltip("Отрисовывать когда объект не выбран")]
        private bool drawAlways;

        [SerializeField, Tooltip("Цвет AABB")]
        private Color aabbColor = Color.magenta;

        [SerializeField, Tooltip("Цвет формы")]
        private Color shapeColor = Color.green;

        /// <summary>
        /// Выбран ли объект
        /// </summary>
        private bool _selected;

        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private EcsFilter<BodyComponent> _filter;

        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private EcsFilter<BodyComponent> Filter
        {
            get
            {
                if (World == null)
                    return null;

                return _filter ??= new EcsFilter<BodyComponent>(World);
            }
        }

        /// <summary>
        /// Пространство сущностей
        /// </summary>
        private EcsWorld World
        {
            get
            {
                if (!Application.isPlaying || !Context.Initialized)
                    return null;

                return Context.Instance.Game.World;
            }
        }

        private void OnDrawGizmos()
        {
            if (Filter == null || !_selected && !drawAlways)
                return;

            _selected = false;

            foreach (uint entity in Filter.Entities)
            {
                ref BodyComponent body = ref World.GetComponent<BodyComponent>(entity);

                Gizmos.color = aabbColor;
                DrawAABB(body.Shape.AABB);

                Gizmos.color = shapeColor;
                DrawShape(in body.Shape);
            }
        }

        private void OnDrawGizmosSelected()
        {
            _selected = true;
        }

        /// <summary>
        /// Нарисовать AABB
        /// </summary>
        private void DrawAABB(in AABB aabb)
        {
            Vector2[] vertexes = new[]
            {
                new Vector2(aabb.X1, aabb.Y1),
                new Vector2(aabb.X1, aabb.Y2),
                new Vector2(aabb.X2, aabb.Y2),
                new Vector2(aabb.X2, aabb.Y1),
                new Vector2(aabb.X1, aabb.Y1),
            };

            for (int i = 0; i < vertexes.Length - 1; i++)
            {
                Gizmos.DrawLine(vertexes[i], vertexes[i + 1]);
            }
        }

        /// <summary>
        /// Нарисовать форму
        /// </summary>
        private void DrawShape(in IShape shape)
        {
            byte match = shape.Match;

            switch (match)
            {
                case 1:
                    DrawShape((CircleShape) shape);
                    break;
                case 2:
                    DrawShape((EdgeShape) shape);
                    break;
                case 3:
                    DrawShape((PointShape) shape);
                    break;
                case 4:
                    DrawShape((PolygonShape) shape);
                    break;
            }
        }

        /// <summary>
        /// Нарисовать форму круга
        /// </summary>
        private void DrawShape(in CircleShape shape)
        {
            Gizmos.DrawWireSphere(shape.Position.Convert(), shape.Radius);
        }

        /// <summary>
        /// Нарисовать форму границы
        /// </summary>
        private void DrawShape(in EdgeShape shape)
        {
            Gizmos.DrawLine(shape.PointA.Convert(), shape.PointB.Convert());
        }

        /// <summary>
        /// Нарисовать форму точки
        /// </summary>
        private void DrawShape(in PointShape shape)
        {
            Gizmos.DrawWireSphere(shape.Position.Convert(), 0.05f);
        }

        /// <summary>
        /// Нарисовать форму полигона
        /// </summary>
        private void DrawShape(in PolygonShape shape)
        {
            int next;

            for (int current = 0; current < shape.Points.Length; current++)
            {
                next = current + 1;

                if (next == shape.Points.Length)
                    next = 0;

                Gizmos.DrawLine(shape.Points[current].Convert(), shape.Points[next].Convert());
            }
        }
#endif
    }
}