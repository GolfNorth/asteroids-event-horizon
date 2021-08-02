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
        /// Свободные компоненты
        /// </summary>
        private int[] _freeComponents;

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
        /// Количество свободных компонентов
        /// </summary>
        private int _freeComponentCount;

        /// <summary>
        /// Конструктор пула компонента
        /// </summary>
        public EcsComponentPool()
        {
            _components = new T[EcsConfig.DefaultComponentPoolCapacity];
            _freeComponents = new int[EcsConfig.DefaultComponentPoolCapacity];
            _entityToIndex = new Dictionary<uint, int>(EcsConfig.DefaultComponentPoolCapacity);
            _indexToEntity = new Dictionary<int, uint>(EcsConfig.DefaultComponentPoolCapacity);
        }

        /// <summary>
        /// Создать компонент
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        public ref T CreateData(uint entityId)
        {
            if (_entityToIndex.ContainsKey(entityId))
            {
                return ref _components[_entityToIndex[entityId]];
            }

            int newIndex;

            if (_freeComponentCount > 0)
            {
                newIndex = _freeComponents[--_freeComponentCount];
            }
            else
            {
                newIndex = _componentCount;

                if (_componentCount == _components.Length)
                {
                    Array.Resize(ref _components, _componentCount * 2);
                }

                _componentCount++;
            }

            _entityToIndex.Add(entityId, newIndex);
            _indexToEntity.Add(newIndex, entityId);

            return ref _components[newIndex];
        }

        /// <summary>
        /// Удалить компонент
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        public void RemoveData(uint entityId)
        {
            if (!_entityToIndex.ContainsKey(entityId))
            {
                return;
            }

            int indexOfRemovedEntity = _entityToIndex[entityId];

            _components[indexOfRemovedEntity] = default;

            if (_freeComponentCount == _freeComponents.Length)
            {
                Array.Resize(ref _freeComponents, _freeComponentCount * 2);
            }

            _freeComponents[_freeComponentCount++] = indexOfRemovedEntity;

            _entityToIndex.Remove(entityId);
            _indexToEntity.Remove(indexOfRemovedEntity);

            _componentCount--;
        }

        /// <summary>
        /// Получить значение компонента
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <param name="forceCreate">Принудительно создать компонент</param>
        public ref T GetData(uint entityId, bool forceCreate)
        {
            if (!_entityToIndex.ContainsKey(entityId))
            {
                if (forceCreate)
                {
                    return ref CreateData(entityId);
                }

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