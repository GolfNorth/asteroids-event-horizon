using UnityVector = UnityEngine.Vector2;
using NumericsVector = System.Numerics.Vector2;

namespace Asteroids.Common
{
    /// <summary>
    /// Расширения для векторов
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Преобразование из <see cref="System.Numerics.Vector2"/> в <see cref="UnityEngine.Vector2"/>
        /// </summary>
        public static UnityVector Convert(this NumericsVector vector)
        {
            return new UnityVector(vector.X, vector.Y);
        }

        /// <summary>
        /// Преобразование из <see cref="System.Numerics.Vector2"/> в <see cref="UnityEngine.Vector2"/>
        /// </summary>
        public static NumericsVector Convert(this UnityVector vector)
        {
            return new NumericsVector(vector.x, vector.y);
        }

        /// <summary>
        /// Преобразование из <see cref="System.Numerics.Vector2"/> в <see cref="UnityEngine.Vector2"/>
        /// </summary>
        public static UnityVector[] Convert(this NumericsVector[] vector)
        {
            if (vector == null)
                return null;

            UnityVector[] result = new UnityVector[vector.Length];

            for (int i = 0; i < vector.Length; i++)
            {
                result[i] = vector[i].Convert();
            }

            return result;
        }

        /// <summary>
        /// Преобразование из <see cref="System.Numerics.Vector2"/> в <see cref="UnityEngine.Vector2"/>
        /// </summary>
        public static NumericsVector[] Convert(this UnityVector[] vector)
        {
            if (vector == null)
                return null;

            NumericsVector[] result = new NumericsVector[vector.Length];

            for (int i = 0; i < vector.Length; i++)
            {
                result[i] = vector[i].Convert();
            }

            return result;
        }
    }
}