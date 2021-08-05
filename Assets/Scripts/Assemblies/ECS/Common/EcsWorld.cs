using System.Collections.Generic;
using System.Collections.Specialized;

namespace NonUnity.Ecs
{
    /// <summary>
    /// Пространство сущностей
    /// </summary>
    public sealed class EcsWorld
    {
        /// <summary>
        /// Конфигуратор пространства сущностей
        /// </summary>
        internal readonly EcsSettings Settings;

        /// <summary>
        /// Менеджер компонентов
        /// </summary>
        private readonly EcsComponentManager _componentManager;

        /// <summary>
        /// Менеджер сущностей
        /// </summary>
        private readonly EcsEntityManager _entityManager;

        /// <summary>
        /// Менеджер фильтров
        /// </summary>
        private readonly EcsFilterManager _filterManager;

        /// <summary>
        /// Конструктор мира сущностей
        /// </summary>
        /// <param name="settings">Конфигуратор пространства сущностей</param>
        public EcsWorld(in EcsSettings settings = default)
        {
            Settings = new EcsSettings
            {
                MaxEntitiesCount = settings.MaxEntitiesCount <= 0
                    ? EcsSettings.DefaultMaxEntitiesCount
                    : settings.MaxEntitiesCount,
                ComponentPoolCapacity = settings.ComponentPoolCapacity <= 0
                    ? EcsSettings.DefaultComponentPoolCapacity
                    : settings.ComponentPoolCapacity,
            };
            _componentManager = new EcsComponentManager(this);
            _entityManager = new EcsEntityManager(this);
            _filterManager = new EcsFilterManager(this);
        }

        /// <summary>
        /// Создать сущность
        /// </summary>
        /// <returns>Идентификатор сущности</returns>
        public uint CreateEntity()
        {
            uint entityId = _entityManager.CreateEntity();
            BitVector32 signature = _entityManager.GetSignature(entityId);

            _filterManager.EntitySignatureChanged(entityId, ref signature);

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
            _filterManager.EntityDestroyed(entityId);
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

            _filterManager.EntitySignatureChanged(entityId, ref signature);

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

            return signature[_componentManager.GetComponentType<T>()];
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

            _filterManager.EntitySignatureChanged(entityId, ref signature);
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
        internal int GetComponentType<T>() where T : struct
        {
            return _componentManager.GetComponentType<T>();
        }

        /// <summary>
        /// Добавить фильтр
        /// </summary>
        /// <returns>Сигнатура фильтра</returns>
        internal BitVector32 AddFilter()
        {
            return _filterManager.AddFilter();
        }

        /// <summary>
        /// Добавить фильтр
        /// </summary>
        /// <typeparam name="T">Тип компонента</typeparam>
        /// <returns>Сигнатура фильтра</returns>
        internal BitVector32 AddFilter<T>() where T : struct
        {
            return _filterManager.AddFilter<T>();
        }

        /// <summary>
        /// Добавить фильтр
        /// </summary>
        /// <typeparam name="T1">Тип компонента</typeparam>
        /// <typeparam name="T2">Тип компонента</typeparam>
        /// <returns>Сигнатура фильтра</returns>
        internal BitVector32 AddFilter<T1, T2>() where T1 : struct where T2 : struct
        {
            return _filterManager.AddFilter<T1, T2>();
        }

        /// <summary>
        /// Добавить фильтр
        /// </summary>
        /// <typeparam name="T1">Тип компонента</typeparam>
        /// <typeparam name="T2">Тип компонента</typeparam>
        /// <typeparam name="T3">Тип компонента</typeparam>
        /// <returns>Сигнатура фильтра</returns>
        internal BitVector32 AddFilter<T1, T2, T3>() where T1 : struct where T2 : struct where T3 : struct
        {
            return _filterManager.AddFilter<T1, T2, T3>();
        }

        /// <summary>
        /// Удалить фильтр
        /// </summary>
        /// <param name="signature">Сигнатура фильтра</param>
        internal void RemoveFilter(BitVector32 signature)
        {
            _filterManager.RemoveFilter(signature);
        }

        /// <summary>
        /// Получить сущности
        /// </summary>
        /// <param name="signature">Сигнатура фильтра</param>
        internal IReadOnlyCollection<uint> GetEntities(BitVector32 signature)
        {
            return _filterManager.GetEntities(signature);
        }
    }
}