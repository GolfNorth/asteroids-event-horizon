using Asteroids.Common;
using NonUnity.Game;
using UnityEngine;
using UnityEngine.Pool;
using Vector2 = System.Numerics.Vector2;

namespace Asteroids.Views
{
    /// <summary>
    /// Базовый визуализатор объекта
    /// </summary>
    public class View : MonoBehaviour, IView
    {
        /// <summary>
        /// Пул визуализаторов
        /// </summary>
        public ObjectPool<View> Pool { get; set; }

        /// <summary>
        /// Позиция объекта
        /// </summary>
        public virtual Vector2 Position
        {
            set => transform.localPosition = value.Convert();
        }

        /// <summary>
        /// Поворот объекта
        /// </summary>
        public virtual float Rotation
        {
            set => transform.localRotation = Quaternion.Euler(0, 0, value);
        }

        /// <summary>
        /// Активировать объект
        /// </summary>
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Деактивировать объект
        /// </summary>
        public void Deactivate()
        {
            gameObject.SetActive(false);
            Pool.Release(this);
        }

        /// <summary>
        /// Уничтожить объект
        /// </summary>
        public virtual void Destroy()
        {
            Deactivate();
        }
    }
}