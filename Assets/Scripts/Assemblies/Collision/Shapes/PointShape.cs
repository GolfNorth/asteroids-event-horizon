using System.Numerics;

namespace NonUnity.Collision
{
    /// <summary>
    /// Форма точки
    /// </summary>
    public struct PointShape : IShape
    {
        /// <summary>
        /// Тип формы
        /// </summary>
        internal const byte match = 3;

        /// <summary>
        /// Центр точки
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// AABB точки
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
        /// Позиция точки
        /// </summary>
        public Vector2 Position => _position;

        /// <summary>
        /// Поворот точки
        /// </summary>
        public float Rotation => 0f;

        /// <summary>
        /// AABB точки
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
        /// <param name="position">Позиция точки</param>
        public PointShape(Vector2 position)
        {
            _position = position;
            _aabb = new AABB();
            _dirty = true;
        }

        /// <summary>
        /// Установить позицию и поворот точки
        /// </summary>
        public void Set(Vector2 position, float rotation)
        {
            Translate(position - _position);
        }

        /// <summary>
        /// Повернуть точку
        /// </summary>
        /// <param name="deltaAngle">Изменение угла</param>
        public void Rotate(float deltaAngle)
        {
        }

        /// <summary>
        /// Переместить точку
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
            _aabb.X1 = _position.X;
            _aabb.X2 = _position.X;
            _aabb.Y1 = _position.Y;
            _aabb.Y2 = _position.Y;
        }
    }
}