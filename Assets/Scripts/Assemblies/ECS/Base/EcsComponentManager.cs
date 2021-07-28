using System;
using System.Collections.Generic;

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
        private readonly Dictionary<string, byte> _componentTypes;

        /// <summary>
        /// Словарь пулов компонентов
        /// </summary>
        private readonly Dictionary<string, IEcsComponentPool> _componentPools;

        /// <summary>
        /// Следующий идентификатор типа компонента
        /// </summary>
        private byte _nextComponentType;

        /// <summary>
        /// Конструктор менеджера компонентов
        /// </summary>
        public EcsComponentManager()
        {
            _componentTypes = new Dictionary<string, byte>();
            _componentPools = new Dictionary<string, IEcsComponentPool>();
        }

        /// <summary>
        /// Зарегистрировать компонент
        /// </summary>
        /// <typeparam name="T">Тип компонента</typeparam>
        public void RegisterComponent<T>() where T : struct
        {
            string typeName = nameof(T);

            if (_componentPools.ContainsKey(typeName))
            {
                throw new ArgumentException("Registering component type more than once.");
            }

            _componentTypes.Add(typeName, _nextComponentType);
            _componentPools.Add(typeName, new EcsComponentPool<T>());

            _nextComponentType++;
        }

        /// <summary>
        /// Получить идентификатор типа компонента
        /// </summary>
        /// <typeparam name="T">Тип компонента</typeparam>
        public byte GetComponentType<T>() where T : struct
        {
            string typeName = nameof(T);

            if (!_componentPools.ContainsKey(typeName))
            {
                throw new ArgumentException("Component not registered before use.");
            }

            return _componentTypes[typeName];
        }

        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="entityId">Иденификатор сущности</param>
        /// <param name="component">Значение компонента</param>
        /// <typeparam name="T">Тип компонента</typeparam>
        public void AddComponent<T>(uint entityId, in T component) where T : struct
        {
            GetComponentPool<T>().InsertData(entityId, in component);
        }

        /// <summary>
        /// Получить компонент
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <typeparam name="T">Тип компонента</typeparam>
        public ref T GetComponent<T>(uint entityId) where T : struct
        {
            return ref GetComponentPool<T>().GetData(entityId);
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
            foreach (KeyValuePair<string, IEcsComponentPool> pair in _componentPools)
            {
                IEcsComponentPool pool = pair.Value;

                pool.EntityDestroyed(entityId);
            }
        }

        /// <summary>
        /// Получить пул компонента
        /// </summary>
        /// <typeparam name="T">Тип компонента</typeparam>
        private EcsComponentPool<T> GetComponentPool<T>() where T : struct
        {
            string typeName = nameof(T);

            if (!_componentPools.ContainsKey(typeName))
            {
                throw new ArgumentException("Component not registered before use.");
            }

            return (EcsComponentPool<T>) _componentPools[typeName];
        }
    }
}