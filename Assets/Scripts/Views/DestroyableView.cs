using UnityEngine;

namespace Asteroids.Views
{
    /// <summary>
    /// Уничтожаемый визуализатор
    /// </summary>
    public class DestroyableView : View
    {
        [SerializeField, Tooltip("Имя системы частиц")]
        private string particleName;

        /// <summary>
        /// Хэш имени системы частиц
        /// </summary>
        private int _particleHash;

        private void Awake()
        {
            _particleHash = Animator.StringToHash(particleName);
        }

        public override void Destroy()
        {
            Particles.Instance.Spawn(_particleHash, transform.position);

            base.Destroy();
        }
    }
}