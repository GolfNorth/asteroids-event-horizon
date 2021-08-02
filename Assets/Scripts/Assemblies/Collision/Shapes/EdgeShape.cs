using System;
using System.Numerics;

namespace NonUnity.Collision
{
    /// <summary>
    /// Форма границы
    /// </summary>
    public struct EdgeShape : IShape
    {
        /// <summary>
        /// Тип формы
        /// </summary>
        internal const byte match = 2;

        /// <summary>
        /// Первая точка границы
        /// </summary>
        private Vector2 _pointA;

        /// <summary>
        /// Вторая точка границы
        /// </summary>
        private Vector2 _pointB;

        /// <summary>
        /// Центр границы
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// Поворот границы
        /// </summary>
        private float _rotation;

        /// <summary>
        /// AABB границы
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
        /// Позиция границы
        /// </summary>
        public Vector2 Position => _position;

        /// <summary>
        /// Поворот границы
        /// </summary>
        public float Rotation => 0f;

        /// <summary>
        /// Первая точка границы
        /// </summary>
        public Vector2 PointA => _pointA;

        /// <summary>
        /// Вторая точка границы
        /// </summary>
        public Vector2 PointB => _pointB;

        /// <summary>
        /// AABB границы
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
        /// Конструктор границы
        /// </summary>
        /// <param name="position">Позиция границы</param>
        /// <param name="pointA">Первая точка границы</param>
        /// <param name="pointB">Вторая точка границы</param>
        public EdgeShape(Vector2 position, Vector2 pointA, Vector2 pointB)
        {
            _position = position;
            _rotation = 0f;
            _pointA = pointA;
            _pointB = pointB;
            _aabb = new AABB();
            _dirty = true;
        }

        /// <summary>
        /// Конструктор границы
        /// </summary>
        /// <param name="position">Позиция границы</param>
        /// <param name="pointA">Первая точка границы</param>
        /// <param name="pointB">Вторая точка границы</param>
        public EdgeShape(Vector2 pointA, Vector2 pointB) : this(Vector2.Zero, pointA, pointB)
        {
        }

        /// <summary>
        /// Установить позицию и поворот границы
        /// </summary>
        public void Set(Vector2 position, float rotation)
        {
            Translate(position - _position);
            Rotate(rotation - _rotation);
        }

        /// <summary>
        /// Повернуть границу
        /// </summary>
        /// <param name="deltaAngle">Изменение угла</param>
        public void Rotate(float deltaAngle)
        {
            deltaAngle = (float) Math.PI * deltaAngle / 180f;

            float cos = (float) Math.Cos(deltaAngle);
            float sin = (float) Math.Sin(deltaAngle);

            Vector2 a = _pointA - _position;
            Vector2 b = _pointB - _position;

            _pointA.X = _position.X + a.X * cos - a.Y * sin;
            _pointA.Y = _position.Y + a.X * sin + a.Y * cos;
            _pointB.X = _position.X + b.X * cos - b.Y * sin;
            _pointB.Y = _position.Y + b.X * sin + b.Y * cos;

            _dirty = true;
        }

        /// <summary>
        /// Переместить границу
        /// </summary>
        /// <param name="deltaTranslation">Изменение позиции</param>
        public void Translate(Vector2 deltaTranslation)
        {
            _position += deltaTranslation;
            _pointA += deltaTranslation;
            _pointB += deltaTranslation;
            _dirty = true;
        }

        /// <summary>
        /// Расчитать AABB
        /// </summary>
        private void ComputeAABB()
        {
            Vector2 min = Vector2.Min(_pointA, _pointB);
            Vector2 max = Vector2.Max(_pointA, _pointB);

            _aabb.X1 = min.X;
            _aabb.X2 = max.X;
            _aabb.Y1 = min.Y;
            _aabb.Y2 = max.Y;
        }
    }
}