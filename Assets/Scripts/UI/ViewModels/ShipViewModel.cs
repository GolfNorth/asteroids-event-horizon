using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asteroids.Common;
using NonUnity.Ecs;
using NonUnity.Game;
using UnityEngine;

namespace Asteroids.UI.ViewModels
{
    /// <summary>
    /// ViewModel корабля
    /// </summary>
    public sealed class ShipViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        /// <summary>
        /// Корабль заспаунен
        /// </summary>
        private bool _spawned;

        /// <summary>
        /// Координаты корабля
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// Угол поворота корабля
        /// </summary>
        private float _rotation;

        /// <summary>
        /// Скорость корабля
        /// </summary>
        private float _speed;

        /// <summary>
        /// Число зарядов лазера
        /// </summary>
        private int _laserCharges;

        /// <summary>
        /// Время доступности лазера
        /// </summary>
        private float _laserNextFire;

        /// <summary>
        /// Пространство сущностей
        /// </summary>
        private EcsWorld _world;

        /// <summary>
        /// Фильтр сущности корабля
        /// </summary>
        private EcsFilter<ShipComponent> _filter;

        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Корабль заспаунен
        /// </summary>
        public bool Spawned
        {
            get => _spawned;
            private set
            {
                if (_spawned == value)
                    return;

                _spawned = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Координаты корабля
        /// </summary>
        public Vector2 Position
        {
            get => _position;
            private set
            {
                if (_position == value)
                    return;

                _position = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Скорость корабля
        /// </summary>
        public float Speed
        {
            get => _speed;
            private set
            {
                if (_speed == value)
                    return;

                _speed = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Угол поворота корабля
        /// </summary>
        public float Rotation
        {
            get => _rotation;
            private set
            {
                if (_rotation == value)
                    return;

                _rotation = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Число зарядов лазера
        /// </summary>
        public int LaserCharges
        {
            get => _laserCharges;
            private set
            {
                if (_laserCharges == value)
                    return;

                _laserCharges = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Время доступности лазера
        /// </summary>
        public float LaserNextFire
        {
            get => _laserNextFire;
            private set
            {
                if (_laserNextFire == value)
                    return;

                _laserNextFire = value;

                OnPropertyChanged();
            }
        }

        private void Awake()
        {
            _world = Context.Instance.Game.World;
            _filter = new EcsFilter<ShipComponent>(_world);
        }

        private void LateUpdate()
        {
            if (!_spawned && _filter.Entities.Count == 0)
                return;

            if (_filter.Entities.Count == 0)
            {
                Spawned = false;
                Position = Vector2.zero;
                Rotation = 0;
                Speed = 0;
                LaserCharges = 0;
                LaserNextFire = 0;
            }

            foreach (uint entity in _filter.Entities)
            {
                ref TransformComponent transformComponent = ref _world.GetComponent<TransformComponent>(entity);
                ref MovementComponent movementComponent = ref _world.GetComponent<MovementComponent>(entity);
                ref LaserGunComponent laserGunComponent = ref _world.GetComponent<LaserGunComponent>(entity);

                Spawned = true;
                Position = transformComponent.Position.Convert();
                Rotation = transformComponent.Rotation;
                Speed = movementComponent.Velocity.Length();
                LaserCharges = laserGunComponent.Charges;
                LaserNextFire = laserGunComponent.NextFire > 0 ? laserGunComponent.NextFire : 0;

                break;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}