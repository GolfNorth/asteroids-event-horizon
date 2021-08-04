using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Common
{
    /// <summary>
    /// Скрипт возвращения системы частиц в пул
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class ReturnToPool : MonoBehaviour
    {
        /// <summary>
        /// Пул системы частиц
        /// </summary>
        public IObjectPool<ParticleSystem> Pool { get; set; }

        /// <summary>
        /// Система частиц
        /// </summary>
        private ParticleSystem _system;

        private void Start()
        {
            _system = GetComponent<ParticleSystem>();
            var main = _system.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnParticleSystemStopped()
        {
            Pool.Release(_system);
        }
    }
}