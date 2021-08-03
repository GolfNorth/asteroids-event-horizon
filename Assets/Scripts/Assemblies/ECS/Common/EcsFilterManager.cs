using System.Collections.Generic;
using System.Collections.Specialized;

namespace NonUnity.Ecs
{
    /// <summary>
    /// Менеджер фильтров
    /// </summary>
    internal sealed class EcsFilterManager
    {
        /// <summary>
        /// Пространство сущностей
        /// </summary>
        private readonly EcsWorld _world;

        /// <summary>
        /// Счетчики сигнатур
        /// </summary>
        private readonly Dictionary<BitVector32, int> _counters;

        /// <summary>
        /// Сущности сигнатур
        /// </summary>
        private readonly Dictionary<BitVector32, HashSet<uint>> _entities;

        /// <summary>
        /// Конструктор менеджера фильтров
        /// </summary>
        /// <param name="world">Пространство сущностей</param>
        public EcsFilterManager(EcsWorld world)
        {
            _counters = new Dictionary<BitVector32, int>();
            _entities = new Dictionary<BitVector32, HashSet<uint>>();
        }

        /// <summary>
        /// Добавить фильтр
        /// </summary>
        /// <returns>Сигнатура фильтра</returns>
        public BitVector32 AddFilter()
        {
            BitVector32 signature = new BitVector32(0);

            AddFilter(signature);

            return signature;
        }

        /// <summary>
        /// Добавить фильтр
        /// </summary>
        /// <typeparam name="T">Тип компонента</typeparam>
        /// <returns>Сигнатура фильтра</returns>
        public BitVector32 AddFilter<T>() where T : struct
        {
            BitVector32 signature = new BitVector32(0)
            {
                [_world.GetComponentType<T>()] = true
            };

            AddFilter(signature);

            return signature;
        }

        /// <summary>
        /// Добавить фильтр
        /// </summary>
        /// <typeparam name="T1">Тип компонента</typeparam>
        /// <typeparam name="T2">Тип компонента</typeparam>
        /// <returns>Сигнатура фильтра</returns>
        public BitVector32 AddFilter<T1, T2>() where T1 : struct where T2 : struct
        {
            BitVector32 signature = new BitVector32(0)
            {
                [_world.GetComponentType<T1>()] = true,
                [_world.GetComponentType<T2>()] = true
            };

            AddFilter(signature);

            return signature;
        }

        /// <summary>
        /// Добавить фильтр
        /// </summary>
        /// <typeparam name="T1">Тип компонента</typeparam>
        /// <typeparam name="T2">Тип компонента</typeparam>
        /// <typeparam name="T3">Тип компонента</typeparam>
        /// <returns>Сигнатура фильтра</returns>
        public BitVector32 AddFilter<T1, T2, T3>() where T1 : struct where T2 : struct where T3 : struct
        {
            BitVector32 signature = new BitVector32(0)
            {
                [_world.GetComponentType<T1>()] = true,
                [_world.GetComponentType<T2>()] = true,
                [_world.GetComponentType<T3>()] = true
            };

            AddFilter(signature);

            return signature;
        }

        /// <summary>
        /// Добавить фильтр
        /// </summary>
        /// <param name="signature">Сигнатура фильтра</param>
        private void AddFilter(BitVector32 signature)
        {
            if (!_counters.ContainsKey(signature))
            {
                _counters.Add(signature, 0);
                _entities.Add(signature, new HashSet<uint>());
            }

            _counters[signature]++;
        }

        /// <summary>
        /// Удалить фильтр
        /// </summary>
        /// <param name="signature">Сигнатура фильтра</param>
        public void RemoveFilter(BitVector32 signature)
        {
            if (!_counters.ContainsKey(signature))
            {
                return;
            }

            _counters[signature]--;

            if (_counters[signature] == 0)
            {
                _counters.Remove(signature);
                _entities.Remove(signature);
            }
        }

        /// <summary>
        /// Получить сущности
        /// </summary>
        /// <param name="signature">Сигнатура фильтра</param>
        public IReadOnlyCollection<uint> GetEntities(BitVector32 signature)
        {
            if (!_entities.ContainsKey(signature))
            {
                return null;
            }

            return _entities[signature];
        }

        /// <summary>
        /// Обработка уничтожения сущности
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        public void EntityDestroyed(uint entityId)
        {
            foreach (KeyValuePair<BitVector32, HashSet<uint>> pair in _entities)
            {
                HashSet<uint> entities = pair.Value;

                entities.Remove(entityId);
            }
        }

        /// <summary>
        /// Обработка изменения сигнатуры сущности
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <param name="entitySignature">Сигнатура сущности</param>
        public void EntitySignatureChanged(uint entityId, ref BitVector32 entitySignature)
        {
            foreach (KeyValuePair<BitVector32, HashSet<uint>> pair in _entities)
            {
                BitVector32 filterSignature = pair.Key;
                HashSet<uint> entities = pair.Value;

                if ((entitySignature.Data & filterSignature.Data) == filterSignature.Data)
                {
                    entities.Add(entityId);
                }
                else
                {
                    entities.Remove(entityId);
                }
            }
        }
    }
}