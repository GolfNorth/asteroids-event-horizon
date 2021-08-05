using System;
using System.Numerics;

namespace NonUnity.Collision
{
    /// <summary>
    /// Форма полигона
    /// </summary>
    public struct PolygonShape : IShape
    {
        /// <summary>
        /// Тип формы
        /// </summary>
        internal const byte match = 4;

        /// <summary>
        /// Точки полигона
        /// </summary>
        private Vector2[] _points;

        /// <summary>
        /// Центр полигона
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// Поворот полигона
        /// </summary>
        private float _rotation;

        /// <summary>
        /// AABB полигона
        /// </summary>
        private AABB _aabb;

        /// <summary>
        /// Грязный флаг
        /// </summary>
        private bool _dirty;

        /// <summary>
        /// Тип формы
        /// </summary>
        public byte Match => match;

        /// <summary>
        /// Позиция полигона
        /// </summary>
        public Vector2 Position => _position;

        /// <summary>
        /// Поворот полигона
        /// </summary>
        public float Rotation => 0f;

        /// <summary>
        /// Точки полигона
        /// </summary>
        public Vector2[] Points => _points;

        /// <summary>
        /// AABB полигона
        /// </summary>
        public AABB AABB
        {
            get
            {
                if (_dirty)
                {
                    ComputeAABB();

                    _dirty = false;
                }

                return _aabb;
            }
        }

        /// <summary>
        /// Конструктор полигона
        /// </summary>
        /// <param name="position">Позиция полигона</param>
        /// <param name="points">Точки полигона</param>
        public PolygonShape(Vector2 position, Vector2[] points)
        {
            _position = position;
            _rotation = 0f;
            _points = points;
            _aabb = new AABB();
            _dirty = true;
        }

        /// <summary>
        /// Конструктор полигона
        /// </summary>
        /// <param name="points">Точки полигона</param>
        public PolygonShape(Vector2[] points) : this(Vector2.Zero, points)
        {
        }

        /// <summary>
        /// Установить позицию и поворот полигона
        /// </summary>
        public void Set(Vector2 position, float rotation)
        {
            Translate(position - _position);
            Rotate(rotation - _rotation);
        }

        /// <summary>
        /// Повернуть полигон
        /// </summary>
        /// <param name="deltaAngle">Изменение угла</param>
        public void Rotate(float deltaAngle)
        {
            _rotation += deltaAngle;
            deltaAngle = (float) Math.PI * deltaAngle / 180f;

            float cos = (float) Math.Cos(deltaAngle);
            float sin = (float) Math.Sin(deltaAngle);

            for (var i = 0; i < _points.Length; i++)
            {
                Vector2 point = _points[i] - _position;

                _points[i].X = _position.X + point.X * cos - point.Y * sin;
                _points[i].Y = _position.Y + point.X * sin + point.Y * cos;
            }

            _dirty = true;
        }

        /// <summary>
        /// Переместить полигон
        /// </summary>
        /// <param name="deltaTranslation">Изменение позиции</param>
        public void Translate(Vector2 deltaTranslation)
        {
            _position += deltaTranslation;

            for (var i = 0; i < _points.Length; i++)
            {
                _points[i] += deltaTranslation;
            }

            _dirty = true;
        }

        /// <summary>
        /// Расчитать AABB
        /// </summary>
        private void ComputeAABB()
        {
            Vector2 min = _points[0];
            Vector2 max = min;

            for (var i = 1; i < _points.Length; ++i)
            {
                min = Vector2.Min(min, _points[i]);
                max = Vector2.Max(max, _points[i]);
            }

            _aabb.X1 = min.X;
            _aabb.X2 = max.X;
            _aabb.Y1 = min.Y;
            _aabb.Y2 = max.Y;
        }
    }
}