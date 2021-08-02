using System.Numerics;

namespace NonUnity.Game
{
    /// <summary>
    /// Интерфейс визуализатора сущности
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Воскресить объект
        /// </summary>
        void Revive();

        /// <summary>
        /// Уничтожить объект
        /// </summary>
        void Destroy();

        /// <summary>
        /// Позиция объекта
        /// </summary>
        Vector2 Position { set; }

        /// <summary>
        /// Поворот объейта
        /// </summary>
        float Rotation { set; }
    }
}