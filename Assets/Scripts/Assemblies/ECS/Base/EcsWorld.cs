using System.Collections.Specialized;

namespace NonUnity.Ecs
{
    public class EcsWorld
    {
        private readonly EcsComponentManager _componentManager;
        private readonly EcsEntityManager _entityManager;
        private readonly EcsSystemManager _systemManager;

        public EcsWorld()
        {
            _componentManager = new EcsComponentManager();
            _entityManager = new EcsEntityManager();
            _systemManager = new EcsSystemManager();
        }

        public uint CreateEntity()
        {
            return _entityManager.CreateEntity();
        }

        public void DestroyEntity(uint entityId)
        {
            _entityManager.DestroyEntity(entityId);
            _componentManager.EntityDestroyed(entityId);
            _systemManager.EntityDestroyed(entityId);
        }

        public void RegisterComponent<T>() where T : struct
        {
            _componentManager.RegisterComponent<T>();
        }

        public void AddComponent<T>(uint entityId, ref T component) where T : struct
        {
            _componentManager.AddComponent<T>(entityId, ref component);

            BitVector32 signature = _entityManager.GetSignature(entityId);
            signature[_componentManager.GetComponentType<T>()] = true;
            _entityManager.SetSignature(entityId, signature);

            _systemManager.EntitySignatureChanged(entityId, ref signature);
        }

        public void RemoveComponent<T>(uint entityId) where T : struct
        {
            _componentManager.RemoveComponent<T>(entityId);

            BitVector32 signature = _entityManager.GetSignature(entityId);
            signature[_componentManager.GetComponentType<T>()] = false;
            _entityManager.SetSignature(entityId, signature);

            _systemManager.EntitySignatureChanged(entityId, ref signature);
        }

        public T GetComponent<T>(uint entityId) where T : struct
        {
            return _componentManager.GetComponent<T>(entityId);
        }

        public byte GetComponentType<T>() where T : struct
        {
            return _componentManager.GetComponentType<T>();
        }

        public T RegisterSystem<T>() where T : EcsSystem, new()
        {
            return _systemManager.RegisterSystem<T>();
        }

        public void SetSystemSignature<T>(ref BitVector32 signature) where T : EcsSystem
        {
            _systemManager.SetSignature<T>(ref signature);
        }
    }
}