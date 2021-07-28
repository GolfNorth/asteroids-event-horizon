using System;
using System.Collections.Generic;

namespace NonUnity.Ecs
{
    public interface IEcsComponentPool
    {
        void EntityDestroyed(uint entityId);
    }

    public class EcsComponentPool<T> : IEcsComponentPool where T : struct
    {
        private T[] _components;

        private readonly Dictionary<uint, int> _entityToIndex;
        private readonly Dictionary<int, uint> _indexToEntity;

        private int _componentCount;

        public EcsComponentPool()
        {
            _components = new T[EcsConfig.DefaultComponentPoolCapacity];
            _entityToIndex = new Dictionary<uint, int>(EcsConfig.DefaultComponentPoolCapacity);
            _indexToEntity = new Dictionary<int, uint>(EcsConfig.DefaultComponentPoolCapacity);
        }

        public void InsertData(uint entityId, ref T component)
        {
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

        public void RemoveData(uint entityId)
        {
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

        public ref T GetData(uint entityId)
        {
            return ref _components[_entityToIndex[entityId]];
        }

        public void EntityDestroyed(uint entityId)
        {
            if (_entityToIndex.ContainsKey(entityId))
            {
                RemoveData(entityId);
            }
        }
    }
}