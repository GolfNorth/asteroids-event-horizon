using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace NonUnity.Ecs
{
    /// <summary>
    /// Менеджер компонентов
    /// </summary>
    internal sealed class EcsComponentManager
    {
        /// <summary>
        /// Словарь идентификаторов типов комонентов
        /// </summary>
        private readonly Dictionary<Type, int> _componentTypes;

        /// <summary>
        /// Словарь пулов компонентов
        /// </summary>
        private readonly Dictionary<Type, IEcsComponentPool> _componentPools;

        /// <summary>
        /// Число компонентов в пуле
        /// </summary>
        private readonly int _componentPoolCapacity;

        /// <summary>
        /// Следующий идентификатор типа компонента
        /// </summary>
        private int _nextComponentType;

        /// <summary>
        /// Конструктор менеджера компонентов
        /// </summary>
        /// <param name="world">Пространство сущностей</param>
        public EcsComponentManager(EcsWorld world)
        {
            _componentPoolCapacity = world.Settings.ComponentPoolCapacity;
            _nextComponentType = 1;
            _componentTypes = new Dictionary<Type, int>();
            _componentPools = new Dictionary<Type, IEcsComponentPool>();
        }

        /// <summary>
        /// Получить идентификатор типа компонента
        /// </summary>
        /// <typeparam name="T">Тип компонента</typeparam>
        public int GetComponentType<T>() where T : struct
        {
            Type type = typeof(T);

            if (!_componentPools.ContainsKey(type))
            {
                RegisterComponent<T>();
            }

            return _componentTypes[type];
        }

        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="entityId">Иденификатор сущности</param>\
        /// <typeparam name="T">Тип компонента</typeparam>
        public ref T AddComponent<T>(uint entityId) where T : struct
        {
            return ref GetComponentPool<T>().CreateData(entityId);
        }

        /// <summary>
        /// Получить компонент
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <typeparam name="T">Тип компонента</typeparam>
        /// <param name="forceCreate">Принудительно создать компонент</param>
        public ref T GetComponent<T>(uint entityId, bool forceCreate) where T : struct
        {
            return ref GetComponentPool<T>().GetData(entityId, forceCreate);
        }

        /// <summary>
        /// Удалить компонент
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <typeparam name="T">Тип компонента</typeparam>
        public void RemoveComponent<T>(uint entityId) where T : struct
        {
            GetComponentPool<T>().RemoveData(entityId);
        }

        /// <summary>
        /// Обработка уничтожения сущности
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        public void EntityDestroyed(uint entityId)
        {
            foreach (KeyValuePair<Type, IEcsComponentPool> pair in _componentPools)
            {
                IEcsComponentPool pool = pair.Value;

                pool.EntityDestroyed(entityId);
            }
        }

        /// <summary>
        /// Зарегистрировать компонент
        /// </summary>
        /// <typeparam name="T">Тип компонента</typeparam>
        private void RegisterComponent<T>() where T : struct
        {
            Type type = typeof(T);

            if (_componentPools.ContainsKey(type))
            {
                return;
            }

            _componentTypes.Add(type, _nextComponentType);
            _componentPools.Add(type, new EcsComponentPool<T>(_componentPoolCapacity));

            _nextComponentType = BitVector32.CreateMask(_nextComponentType);
        }

        /// <summary>
        /// Получить пул компонента
        /// </summary>
        /// <typeparam name="T">Тип компонента</typeparam>
        private EcsComponentPool<T> GetComponentPool<T>() where T : struct
        {
            Type type = typeof(T);

            if (!_componentPools.ContainsKey(type))
            {
                RegisterComponent<T>();
            }

            return (EcsComponentPool<T>) _componentPools[type];
        }
    }
}