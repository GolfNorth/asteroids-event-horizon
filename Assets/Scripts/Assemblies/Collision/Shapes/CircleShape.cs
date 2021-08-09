using System.Numerics;

namespace NonUnity.Collision
{
    /// <summary>
    /// Форма круга
    /// </summary>
    public struct CircleShape : IShape
    {
        /// <summary>
        /// Тип формы
        /// </summary>
        internal const byte match = 1;

        /// <summary>
        /// Центр круга
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// Радиус круга
        /// </summary>
        private float _radius;

        /// <summary>
        /// AABB круга
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
        /// Позиция круга
        /// </summary>
        public Vector2 Position => _position;

        /// <summary>
        /// Радиус круга
        /// </summary>
        public float Radius => _radius;

        /// <summary>
        /// Поворот круга
        /// </summary>
        public float Rotation => 0f;

        /// <summary>
        /// AABB круга
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
        /// Конструктор круга
        /// </summary>
        /// <param name="position">Центр круга</param>
        /// <param name="radius">Радиус круга</param>
        public CircleShape(Vector2 position, float radius)
        {
            _position = position;
            _radius = radius;
            _aabb = new AABB();
            _dirty = true;
        }

        /// <summary>
        /// Конструктор круга
        /// </summary>
        /// <param name="radius">Радиус круга</param>
        public CircleShape(float radius) : this(Vector2.Zero, radius)
        {
        }

        /// <summary>
        /// Установить позицию и поворот круга
        /// </summary>
        public void Set(Vector2 position, float rotation)
        {
            Translate(position - _position);
        }

        /// <summary>
        /// Повернуть круг
        /// </summary>
        /// <param name="deltaAngle">Изменение угла</param>
        public void Rotate(float deltaAngle)
        {
        }

        /// <summary>
        /// Переместить круг
        /// </summary>
        /// <param name="deltaTranslation">Изменение позиции</param>
        public void Translate(Vector2 deltaTranslation)
        {
            if (deltaTranslation == Vector2.Zero)
                return;

            _position += deltaTranslation;
            _dirty = true;
        }

        /// <summary>
        /// Расчитать AABB
        /// </summary>
        private void ComputeAABB()
        {
            _aabb.X1 = _position.X - _radius;
            _aabb.X2 = _position.X + _radius;
            _aabb.Y1 = _position.Y - _radius;
            _aabb.Y2 = _position.Y + _radius;
        }
    }
}