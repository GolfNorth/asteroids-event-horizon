using System.Collections.Generic;
using System.Collections.Specialized;

namespace NonUnity.Ecs
{
    public class EcsEntityManager
    {
        private readonly Queue<uint> _availableEntities;

        private readonly BitVector32[] _signatures;

        private int _livingEntityCount;

        public EcsEntityManager()
        {
            _signatures = new BitVector32[EcsConfig.MaxEntitiesCount];
            _availableEntities = new Queue<uint>(EcsConfig.MaxEntitiesCount);

            for (uint i = 0; i < EcsConfig.MaxEntitiesCount; i++)
            {
                _signatures[i] = new BitVector32();
                _availableEntities.Enqueue(i);
            }
        }

        public uint CreateEntity()
        {
            uint entityId = _availableEntities.Dequeue();

            _livingEntityCount++;

            return entityId;
        }

        public void DestroyEntity(uint entityId)
        {
            _signatures[entityId] = new BitVector32(0);

            _availableEntities.Enqueue(entityId);

            _livingEntityCount--;
        }

        public void SetSignature(uint entityId, BitVector32 signature)
        {
            _signatures[entityId] = signature;
        }

        public BitVector32 GetSignature(uint entityId)
        {
            return _signatures[entityId];
        }
    }
}