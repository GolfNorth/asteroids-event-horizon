using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace NonUnity.Ecs
{
    /// <summary>
    /// Менеджер систем
    /// </summary>
    internal sealed class EcsSystemManager
    {
        /// <summary>
        /// Словарь сигнатур
        /// </summary>
        private readonly Dictionary<string, BitVector32> _signatures;

        /// <summary>
        /// Словарь систем
        /// </summary>
        private readonly Dictionary<string, EcsSystem> _systems;

        /// <summary>
        /// Конструктор менеджера систем
        /// </summary>
        public EcsSystemManager()
        {
            _signatures = new Dictionary<string, BitVector32>(EcsConfig.MaxEntitiesCount);
            _systems = new Dictionary<string, EcsSystem>();
        }

        /// <summary>
        /// Зарегистрировать систему
        /// </summary>
        /// <param name="system">Экземпляр системы</param>
        /// <typeparam name="T">Тип системы</typeparam>
        public void RegisterSystem<T>(T system) where T : EcsSystem
        {
            string typeName = nameof(T);

            if (!_systems.ContainsKey(typeName))
            {
                throw new ArgumentException("Registering system more than once.");
            }

            _systems.Add(typeName, system);
        }

        /// <summary>
        /// Задать сигнатуру системы
        /// </summary>
        /// <param name="signature">Сигнатура системы</param>
        /// <typeparam name="T">Тип системы</typeparam>
        public void SetSignature<T>(ref BitVector32 signature) where T : EcsSystem
        {
            string typeName = nameof(T);

            if (!_systems.ContainsKey(typeName))
            {
                throw new ArgumentException("System used before registered.");
            }

            _signatures.Add(typeName, signature);
        }

        /// <summary>
        /// Обработка уничтожения сущности
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        public void EntityDestroyed(uint entityId)
        {
            foreach (KeyValuePair<string, EcsSystem> pair in _systems)
            {
                EcsSystem system = pair.Value;

                system.Entities.Remove(entityId);
            }
        }

        /// <summary>
        /// Обработка изменения сигнатуры сущности
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <param name="entitySignature">Сигнатура сущности</param>
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