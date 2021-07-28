using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace NonUnity.Ecs
{
    /// <summary>
    /// Менеджер сущностей
    /// </summary>
    internal sealed class EcsEntityManager
    {
        /// <summary>
        /// Очередь доступных сущностей
        /// </summary>
        private readonly Queue<uint> _availableEntities;

        /// <summary>
        /// Сигнатура сущности
        /// </summary>
        private readonly BitVector32[] _signatures;

        /// <summary>
        /// Количество задействованных сущностей
        /// </summary>
        private int _livingEntityCount;

        /// <summary>
        /// Конструктор менеджера сущностей
        /// </summary>
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

        /// <summary>
        /// Создать сущность
        /// </summary>
        public uint CreateEntity()
        {
            if (_livingEntityCount > EcsConfig.MaxEntitiesCount)
            {
                throw new OverflowException("Too many entities in existence.");
            }

            uint entityId = _availableEntities.Dequeue();

            _livingEntityCount++;

            return entityId;
        }

        /// <summary>
        /// Уничтожить сущность
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        public void DestroyEntity(uint entityId)
        {
            if (entityId >= EcsConfig.MaxEntitiesCount)
            {
                throw new IndexOutOfRangeException("Entity out of range.");
            }

            _signatures[entityId] = new BitVector32(0);

            _availableEntities.Enqueue(entityId);

            _livingEntityCount--;
        }

        /// <summary>
        /// Задать сигнатуру сущности
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <param name="signature">Сигнатура сущности</param>
        public void SetSignature(uint entityId, BitVector32 signature)
        {
            if (entityId >= EcsConfig.MaxEntitiesCount)
            {
                throw new IndexOutOfRangeException("Entity out of range.");
            }

            _signatures[entityId] = signature;
        }

        /// <summary>
        /// Получить сигнатуру сущности
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        public BitVector32 GetSignature(uint entityId)
        {
            if (entityId >= EcsConfig.MaxEntitiesCount)
            {
                throw new IndexOutOfRangeException("Entity out of range.");
            }

            return _signatures[entityId];
        }
    }
}