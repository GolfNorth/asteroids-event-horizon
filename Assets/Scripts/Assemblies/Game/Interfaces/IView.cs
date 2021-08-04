using System.Numerics;

namespace NonUnity.Game
{
    /// <summary>
    /// Интерфейс визуализатора сущности
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Позиция объекта
        /// </summary>
        Vector2 Position { set; }

        /// <summary>
        /// Поворот объекта
        /// </summary>
        float Rotation { set; }

        /// <summary>
        /// Активировать объект
        /// </summary>
        void Activate();

        /// <summary>
        /// Деактивировать объект
        /// </summary>
        void Deactivate();

        /// <summary>
        /// Уничтожить объект
        /// </summary>
        void Destroy();
    }
}