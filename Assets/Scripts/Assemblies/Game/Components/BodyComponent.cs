using NonUnity.Collision;

namespace NonUnity.Game
{
    /// <summary>
    /// Компонент тела
    /// </summary>
    public struct BodyComponent
    {
        /// <summary>
        /// Слой сущности
        /// </summary>
        public byte Layer;

        /// <summary>
        /// Маска столкновений
        /// </summary>
        public byte Mask;

        /// <summary>
        /// Форма сущности
        /// </summary>
        public IShape Shape;
    }
}