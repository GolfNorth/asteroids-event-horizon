using System;
using System.Collections.Generic;

namespace NonUnity.Ecs
{
    /// <summary>
    /// Интерфейс пула компонента
    /// </summary>
    internal interface IEcsComponentPool
    {
        void EntityDestroyed(uint entityId);
    }

    /// <summary>
    /// Пул компонента
    /// </summary>
    /// <typeparam name="T">Тип компонента</typeparam>
    internal class EcsComponentPool<T> : IEcsComponentPool where T : struct
    {
        /// <summary>
        /// Массив компонентов
        /// </summary>
        private T[] _components;

        /// <summary>
        /// Словарь индексов
        /// </summary>
        private readonly Dictionary<uint, int> _entityToIndex;

        /// <summary>
        /// Словарь сущностей
        /// </summary>
        private readonly Dictionary<int, uint> _indexToEntity;

        /// <summary>
        /// Количество задействованных компонентов
        /// </summary>
        private int _componentCount;

        /// <summary>
        /// Конструктор пула компонента
        /// </summary>
        public EcsComponentPool()
        {
            _components = new T[EcsConfig.DefaultComponentPoolCapacity];
            _entityToIndex = new Dictionary<uint, int>(EcsConfig.DefaultComponentPoolCapacity);
            _indexToEntity = new Dictionary<int, uint>(EcsConfig.DefaultComponentPoolCapacity);
        }

        /// <summary>
        /// Вставить компонент
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <param name="component">Значение компонента</param>
        public void InsertData(uint entityId, in T component)
        {
            if (_entityToIndex.ContainsKey(entityId))
            {
                throw new ArgumentException("Component added to same entity more than once.");
            }

            int newIndex = _componentCount;

            _entityToIndex.Add(entityId, newIndex);
            _indexToEntity.Add(newIndex, entityId);

            if (newIndex == _components.Length)
            {
                Array.Resize(ref _components, newIndex * 2);
            }

            _components[newIndex] = component;

            _componentCount++;
        }

        /// <summary>
        /// Удалить компонент
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        public void RemoveData(uint entityId)
        {
            if (!_entityToIndex.ContainsKey(entityId))
            {
                throw new ArgumentException("Removing non-existent component.");
            }

            int indexOfRemovedEntity = _entityToIndex[entityId];
            int indexOfLastElement = _componentCount - 1;
            _components[indexOfRemovedEntity] = _components[indexOfLastElement];

            uint entityOfLastElement = _indexToEntity[indexOfLastElement];
            _entityToIndex[entityOfLastElement] = indexOfRemovedEntity;
            _indexToEntity[indexOfRemovedEntity] = entityOfLastElement;

            _entityToIndex.Remove(entityId);
            _indexToEntity.Remove(indexOfLastElement);

            _componentCount--;
        }

        /// <summary>
        /// Получить значение компонента
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        public ref T GetData(uint entityId)
        {
            if (!_entityToIndex.ContainsKey(entityId))
            {
                throw new ArgumentException("Retrieving non-existent component.");
            }

            return ref _components[_entityToIndex[entityId]];
        }

        /// <summary>
        /// Обработка уничтожения сущности
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        public void EntityDestroyed(uint entityId)
        {
            if (_entityToIndex.ContainsKey(entityId))
            {
                RemoveData(entityId);
            }
        }
    }
}