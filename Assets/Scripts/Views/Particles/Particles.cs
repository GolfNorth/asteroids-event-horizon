using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Views
{
    /// <summary>
    /// Контроллер системы частиц
    /// </summary>
    public class Particles : MonoBehaviour
    {
        /// <summary>
        /// Параметры префаба системы частиц
        /// </summary>
        [Serializable]
        private class Prefab
        {
            [SerializeField, Tooltip("Имя префаба")]
            private string name;

            [SerializeField, Tooltip("Компонент префаба")]
            private ParticleSystem component;

            /// <summary>
            /// Хэш имени префаба
            /// </summary>
            private int _hash = -1;

            /// <summary>
            /// Имя префаба
            /// </summary>
            public string Name => name;

            /// <summary>
            /// Компонент префаба
            /// </summary>
            public ParticleSystem Component => component;

            /// <summary>
            /// Хэш имени префаба
            /// </summary>
            public int Hash
            {
                get
                {
                    if (_hash == -1)
                    {
                        _hash = Animator.StringToHash(name);
                    }

                    return _hash;
                }
            }
        }

        [SerializeField, Tooltip("Родительский трансформ частиц")]
        private Transform parent;

        [SerializeField, Tooltip("Параметры префабов системы частиц")]
        private Prefab[] prefabs = new Prefab[0];

        /// <summary>
        /// Экземпляр контроллера
        /// </summary>
        public static Particles Instance;

        /// <summary>
        /// Пул частиц
        /// </summary>
        private Dictionary<int, ObjectPool<ParticleSystem>> _particles;

        private void Awake()
        {
            Instance = this;
            _particles = new Dictionary<int, ObjectPool<ParticleSystem>>();

            foreach (Prefab prefab in prefabs)
            {
                CreatePool(prefab);
            }
        }

        /// <summary>
        /// Создать систему частиц в заданной позиции
        /// </summary>
        public void Spawn(int hash, Vector3 position)
        {
            if (!_particles.ContainsKey(hash))
                return;

            ParticleSystem system = _particles[hash].Get();
            system.transform.position = position;
            system.Play();
        }

        /// <summary>
        /// Создать пул
        /// </summary>
        private void CreatePool(Prefab prefab)
        {
            _particles.Add(prefab.Hash, new ObjectPool<ParticleSystem>(
                () => CreatePooledItem(prefab),
                system => system.gameObject.SetActive(true),
                system => system.gameObject.SetActive(false),
                system => Destroy(system.gameObject)
            ));
        }

        /// <summary>
        /// Создать элемент пула
        /// </summary>
        private ParticleSystem CreatePooledItem(Prefab prefab)
        {
            ParticleSystem system = Instantiate(prefab.Component, parent);

            system.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            ReturnToPool returnToPool = system.gameObject.AddComponent<ReturnToPool>();
            returnToPool.Pool = _particles[prefab.Hash];

            return system;
        }
    }
}