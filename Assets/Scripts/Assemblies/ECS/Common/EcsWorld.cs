using System.Collections.Generic;
using System.Collections.Specialized;

namespace NonUnity.Ecs
{
    /// <summary>
    /// Мир сущностей
    /// </summary>
    public sealed class EcsWorld
    {
        /// <summary>
        /// Менеджер компонентов
        /// </summary>
        private readonly EcsComponentManager _componentManager;

        /// <summary>
        /// Менеджер сущностей
        /// </summary>
        private readonly EcsEntityManager _entityManager;

        /// <summary>
        /// Менеджер систем
        /// </summary>
        private readonly EcsSystemManager _systemManager;

        /// <summary>
        /// Коллекция сущностей
        /// </summary>
        private readonly List<uint> _entities;

        /// <summary>
        /// Коллекция сущностей
        /// </summary>
        public IReadOnlyList<uint> Entities => _entities;

        /// <summary>
        /// Конструктор мира сущностей
        /// </summary>
        public EcsWorld()
        {
            _componentManager = new EcsComponentManager();
            _entityManager = new EcsEntityManager();
            _systemManager = new EcsSystemManager();
            _entities = new List<uint>(EcsConfig.MaxEntitiesCount);
        }

        /// <summary>
        /// Создать сущность
        /// </summary>
        /// <returns>Идентификатор сущности</returns>
        public uint CreateEntity()
        {
            uint entityId = _entityManager.CreateEntity();

            _entities.Add(entityId);

            return entityId;
        }

        /// <summary>
        /// Уничтожить сущность
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        public void DestroyEntity(uint entityId)
        {
            _entityManager.DestroyEntity(entityId);
            _componentManager.EntityDestroyed(entityId);
            _systemManager.EntityDestroyed(entityId);

            _entities.Remove(entityId);
        }

        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <typeparam name="T">Тип компонента</typeparam>
        public ref T AddComponent<T>(uint entityId) where T : struct
        {
            ref T component = ref _componentManager.AddComponent<T>(entityId);

            BitVector32 signature = _entityManager.GetSignature(entityId);
            signature[_componentManager.GetComponentType<T>()] = true;
            _entityManager.SetSignature(entityId, signature);

            _systemManager.EntitySignatureChanged(entityId, ref signature);

            return ref component;
        }

        /// <summary>
        /// Наличие компонента
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <typeparam name="T">Тип компонента</typeparam>
        public bool HasComponent<T>(uint entityId) where T : struct
        {
            BitVector32 signature = _entityManager.GetSignature(entityId);

            return signature[_componentManager.GetComponentType<T>()] == true;
        }

        /// <summary>
        /// Удалить компонент
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <typeparam name="T">Тип компонента</typeparam>
        public void RemoveComponent<T>(uint entityId) where T : struct
        {
            _componentManager.RemoveComponent<T>(entityId);

            BitVector32 signature = _entityManager.GetSignature(entityId);
            signature[_componentManager.GetComponentType<T>()] = false;
            _entityManager.SetSignature(entityId, signature);

            _systemManager.EntitySignatureChanged(entityId, ref signature);
        }

        /// <summary>
        /// Получить компонент
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <param name="forceCreate">Принудительно создать компонент</param>
        /// <typeparam name="T">Тип компонента</typeparam>
        public ref T GetComponent<T>(uint entityId, bool forceCreate = false) where T : struct
        {
            return ref _componentManager.GetComponent<T>(entityId, forceCreate);
        }

        /// <summary>
        /// Получить идентификатор типа компонента
        /// </summary>
        /// <typeparam name="T">Тип компонента</typeparam>
        public byte GetComponentType<T>() where T : struct
        {
            return _componentManager.GetComponentType<T>();
        }

        /// <summary>
        /// Зарегистрировать систему
        /// </summary>
        /// <param name="system">Экземпляр системы</param>
        /// <typeparam name="T">Тип системы</typeparam>
        public void RegisterSystem<T>(T system) where T : EcsSystem
        {
            _systemManager.RegisterSystem<T>(system);
        }

        /// <summary>
        /// Задать сигнатуру системы
        /// </summary>
        /// <param name="signature">Сигнатура системы</param>
        /// <typeparam name="T">Тип системы</typeparam>
        public void SetSystemSignature<T>(ref BitVector32 signature) where T : EcsSystem
        {
            _systemManager.SetSignature<T>(ref signature);
        }
    }
}