using System;
using System.Collections.Generic;

namespace NonUnity.Ecs
{
    public class EcsComponentManager
    {
        private readonly Dictionary<string, byte> _componentTypes;

        private readonly Dictionary<string, IEcsComponentPool> _componentPools;

        private byte _nextComponentType;

        public EcsComponentManager()
        {
            _componentTypes = new Dictionary<string, byte>();
            _componentPools = new Dictionary<string, IEcsComponentPool>();
        }

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

        public byte GetComponentType<T>() where T : struct
        {
            string typeName = nameof(T);

            if (!_componentPools.ContainsKey(typeName))
            {
                throw new ArgumentException("Component not registered before use.");
            }

            return _componentTypes[typeName];
        }

        public void AddComponent<T>(uint entityId, ref T component) where T : struct
        {
            GetComponentArray<T>().InsertData(entityId, ref component);
        }

        public ref T GetComponent<T>(uint entityId) where T : struct
        {
            return ref GetComponentArray<T>().GetData(entityId);
        }

        public void RemoveComponent<T>(uint entityId) where T : struct
        {
            GetComponentArray<T>().RemoveData(entityId);
        }

        public void EntityDestroyed(uint entityId)
        {
            foreach (KeyValuePair<string, IEcsComponentPool> pair in _componentPools)
            {
                IEcsComponentPool pool = pair.Value;

                pool.EntityDestroyed(entityId);
            }
        }

        private EcsComponentPool<T> GetComponentArray<T>() where T : struct
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