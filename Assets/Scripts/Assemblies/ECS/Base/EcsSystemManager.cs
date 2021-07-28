using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace NonUnity.Ecs
{
    public class EcsSystemManager
    {
        private Dictionary<string, BitVector32> _signatures;

        private Dictionary<string, EcsSystem> _systems;

        public EcsSystemManager()
        {
            _signatures = new Dictionary<string, BitVector32>(EcsConfig.MaxEntitiesCount);
            _systems = new Dictionary<string, EcsSystem>();
        }

        public T RegisterSystem<T>() where T : EcsSystem, new()
        {
            string typeName = nameof(T);

            if (!_systems.ContainsKey(typeName))
            {
                throw new ArgumentException("Registering system more than once.");
            }

            T system = new T();

            _systems.Add(typeName, system);

            return system;
        }

        public void SetSignature<T>(ref BitVector32 signature) where T : EcsSystem
        {
            string typeName = nameof(T);

            if (!_systems.ContainsKey(typeName))
            {
                throw new ArgumentException("System used before registered.");
            }

            _signatures.Add(typeName, signature);
        }

        public void EntityDestroyed(uint entityId)
        {
            foreach (KeyValuePair<string, EcsSystem> pair in _systems)
            {
                EcsSystem system = pair.Value;

                system.Entities.Remove(entityId);
            }
        }

        public void EntitySignatureChanged(uint entityId, ref BitVector32 entitySignature)
        {
            foreach (KeyValuePair<string, EcsSystem> pair in _systems)
            {
                string typeName = pair.Key;
                EcsSystem system = pair.Value;
                BitVector32 systemSignature = _signatures[typeName];

                bool isIntersects = true;

                for (var i = 0; i < 32; i++)
                {
                    if ((entitySignature[i] & systemSignature[i]) != systemSignature[i])
                    {
                        isIntersects = false;

                        break;
                    }
                }

                if (isIntersects)
                {
                    system.Entities.Add(entityId);
                }
                else
                {
                    system.Entities.Remove(entityId);
                }
            }
        }
    }
}