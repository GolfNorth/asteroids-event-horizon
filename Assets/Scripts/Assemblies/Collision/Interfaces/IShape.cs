using System.Numerics;

namespace NonUnity.Collision
{
    /// <summary>
    /// Интерфейс формы объекта
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// Тип формы
        /// </summary>
        byte Match { get; }

        /// <summary>
        /// Позиция формы
        /// </summary>
        Vector2 Position { get; }

        /// <summary>
        /// Поворот формы
        /// </summary>
        float Rotation { get; }

        /// <summary>
        /// AABB объекта
        /// </summary>
        AABB AABB { get; }

        /// <summary>
        /// Установить позицию и поворот формы
        /// </summary>
        void Set(Vector2 position, float rotation);

        /// <summary>
        /// Повернуть форму
        /// </summary>
        /// <param name="deltaAngle">Изменение угла</param>
        void Rotate(float deltaAngle);

        /// <summary>
        /// Переместить форму
        /// </summary>
        /// <param name="deltaTranslation">Изменение позиции</param>
        void Translate(Vector2 deltaTranslation);
    }
}