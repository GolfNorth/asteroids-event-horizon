using System;
using System.Collections.Generic;
using NonUnity.Game;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Views
{
    /// <summary>
    /// Управляющий пул визуализаторов
    /// </summary>
    public sealed class ViewPool : MonoBehaviour, IViewFactory
    {
        [SerializeField, Tooltip("Родительский трансформ объектов")]
        private Transform _parent;

        [SerializeField, Tooltip("Визуализатор корабля")]
        private View _shipPrefab;

        [SerializeField, Tooltip("Визуализатор НЛО")]
        private View _ufoPrefab;

        [SerializeField, Tooltip("Визуализатор пули")]
        private View _bulletPrefab;

        [SerializeField, Tooltip("Визуализатор лазера")]
        private View _laserPrefab;

        [SerializeField, Tooltip("Визуализатор маленького астероида")]
        private View _smallAsteroidPrefab;

        [SerializeField, Tooltip("Визуализатор среднего астероида")]
        private View _middleAsteroidPrefab;

        [SerializeField, Tooltip("Визуализатор большого астероида")]
        private View _largeAsteroidPrefab;

        /// <summary>
        /// Пулы визуализаторов
        /// </summary>
        private Dictionary<ViewType, ObjectPool<View>> _pools;

        /// <summary>
        /// Инициализация пулов
        /// </summary>
        private void Awake()
        {
            _pools = new Dictionary<ViewType, ObjectPool<View>>();

            foreach (ViewType viewType in (ViewType[]) Enum.GetValues(typeof(ViewType)))
            {
                CreatePool(viewType);
            }
        }

        /// <summary>
        /// Создать пул визуализаторов
        /// </summary>
        /// <param name="viewType">Тип визуализатора</param>
        private void CreatePool(ViewType viewType)
        {
            View prefab = GetPrefab(viewType);
            ObjectPool<View> objectPool = new ObjectPool<View>(
                () =>
                {
                    View view = Instantiate(prefab, _parent);
                    view.Pool = _pools[viewType];
                    view.gameObject.SetActive(false);

                    return view;
                },
                view => { view.gameObject.SetActive(false); },
                view => { view.gameObject.SetActive(false); },
                view => { Destroy(view.gameObject); }
            );

            _pools.Add(viewType, objectPool);
        }

        /// <summary>
        /// Получить префаб визуализатора
        /// </summary>
        /// <param name="viewType">Тип визуализатора</param>
        private View GetPrefab(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Ship => _shipPrefab,
                ViewType.Ufo => _ufoPrefab,
                ViewType.Bullet => _bulletPrefab,
                ViewType.Laser => _laserPrefab,
                ViewType.SmallAsteroid => _smallAsteroidPrefab,
                ViewType.MiddleAsteroid => _middleAsteroidPrefab,
                ViewType.LargeAsteroid => _largeAsteroidPrefab,
                _ => null
            };
        }

        #region IViewFactory

        public IView CreateShip()
        {
            return _pools[ViewType.Ship].Get();
        }

        public IView CreateAsteroid(AsteroidSize size)
        {
            return size switch
            {
                AsteroidSize.Small => _pools[ViewType.SmallAsteroid].Get(),
                AsteroidSize.Middle => _pools[ViewType.MiddleAsteroid].Get(),
                AsteroidSize.Large => _pools[ViewType.LargeAsteroid].Get(),
                _ => null
            };
        }

        public IView CreateUfo()
        {
            return _pools[ViewType.Ufo].Get();
        }

        public IView CreateBullet()
        {
            return _pools[ViewType.Bullet].Get();
        }

        public IView CreateLaser()
        {
            return _pools[ViewType.Laser].Get();
        }

        #endregion
    }
}