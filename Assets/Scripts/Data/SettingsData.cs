using UnityEngine;

namespace Asteroids.Data
{
    public abstract class SettingsData : ScriptableObject
    {
        /// <summary>
        /// Установить конфигурацию по умолчанию
        /// </summary>
        public abstract void Default();
    }

    /// <summary>
    /// Базовый класс игровых конфигураций
    /// </summary>
    public abstract class SettingsData<T> : SettingsData where T : struct
    {
        [SerializeField, Tooltip("Конфигурация игры")]
        protected T settings;

        /// <summary>
        /// Получить конфигурацию игры
        /// </summary>
        public virtual ref T GetData()
        {
            return ref settings;
        }
    }
}