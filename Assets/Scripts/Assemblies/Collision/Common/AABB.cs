namespace NonUnity.Collision
{
    /// <summary>
    /// Axis-aligned bounding box
    /// </summary>
    public struct AABB
    {
        /// <summary>
        /// Левая граница на горизонтальной оси
        /// </summary>
        public float X1;

        /// <summary>
        /// Правая граница на горизонтальной оси
        /// </summary>
        public float X2;

        /// <summary>
        /// Нижняя граница на вертикальной оси
        /// </summary>
        public float Y1;

        /// <summary>
        /// Верхняя граница на вертикальной оси
        /// </summary>
        public float Y2;

        /// <summary>
        /// Пересекает ли другой AABB
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Intersect(in AABB other)
        {
            return other.X1 <= X2 &&
                   other.X2 >= X1 &&
                   other.Y2 >= Y1 &&
                   other.Y1 <= Y2;
        }
    }
}