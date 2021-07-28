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
        /// Конструктор мира сущностей
        /// </summary>
        public EcsWorld()
        {
            _componentManager = new EcsComponentManager();
            _entityManager = new EcsEntityManager();
            _systemManager = new EcsSystemManager();
        }

        /// <summary>
        /// Создать сущность
        /// </summary>
        /// <returns>Идентификатор сущности</returns>
        public uint CreateEntity()
        {
            return _entityManager.CreateEntity();
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
        }

        /// <summary>
        /// Зарегистрировать компонент
        /// </summary>
        /// <typeparam name="T">Тип компонента</typeparam>
        public void RegisterComponent<T>() where T : struct
        {
            _componentManager.RegisterComponent<T>();
        }

        /// <summary>
        /// Добавить компонент
        /// </summary>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <param name="component">Значение компонента</param>
        /// <typeparam name="T">Тип компонента</typeparam>
        public void AddComponent<T>(uint entityId, in T component) where T : struct
        {
            _componentManager.AddComponent<T>(entityId, in component);

            BitVector32 signature = _entityManager.GetSignature(entityId);
            signature[_componentManager.GetComponentType<T>()] = true;
            _entityManager.SetSignature(entityId, signature);

            _systemManager.EntitySignatureChanged(entityId, ref signature);
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
        /// <typeparam name="T">Тип компонента</typeparam>
        public ref T GetComponent<T>(uint entityId) where T : struct
        {
            return ref _componentManager.GetComponent<T>(entityId);
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