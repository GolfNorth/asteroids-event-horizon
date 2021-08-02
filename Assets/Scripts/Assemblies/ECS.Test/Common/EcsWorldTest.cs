using System;
using System.Collections.Specialized;
using NonUnity.Ecs;
using NUnit.Framework;

namespace NonUnity.ECS.Test
{
    internal struct SomeComponent
    {
        public int Value;
    }

    internal class SomeSystem : EcsSystem
    {
    }

    [TestFixture]
    public class EcsWorldTest
    {
        private EcsWorld _world;

        [SetUp]
        public void SetUp()
        {
            _world = new EcsWorld();
        }

        [Test]
        public void Constructor_NewWorld_HasNoEntity()
        {
            int entityCount = _world.Entities.Count;

            Assert.AreEqual(0, entityCount);
        }

        [Test]
        public void CreateEntity_NewEntity_HasEntity()
        {
            uint entity = _world.CreateEntity();

            int entityCount = _world.Entities.Count;

            Assert.AreEqual(1, entityCount);
        }

        [Test]
        public void CreateEntity_NewEntity_HasNoComponent()
        {
            uint entity = _world.CreateEntity();

            bool hasComponent = _world.HasComponent<SomeComponent>(entity);

            Assert.IsFalse(hasComponent);
        }

        [Test]
        public void DestroyEntity_NewEntity_HasNoEntity()
        {
            uint entity = _world.CreateEntity();

            _world.DestroyEntity(entity);
            int entityCount = _world.Entities.Count;

            Assert.AreEqual(0, entityCount);
        }

        [Test]
        public void AddComponent_NewComponent_HasComponent()
        {
            uint entity = _world.CreateEntity();
            _world.AddComponent<SomeComponent>(entity);

            bool hasComponent = _world.HasComponent<SomeComponent>(entity);

            Assert.IsTrue(hasComponent);
        }

        [Test]
        public void GetComponent_ChangeValue_ValidValue()
        {
            uint entity = _world.CreateEntity();
            ref SomeComponent someComponent = ref _world.AddComponent<SomeComponent>(entity);
            someComponent.Value = 5;

            ref SomeComponent component = ref _world.GetComponent<SomeComponent>(entity);

            Assert.AreEqual(5, component.Value);
        }

        [Test]
        public void GetComponent_GetNonExisted_ThrowError()
        {
            TestDelegate getComponent = () => _world.GetComponent<SomeComponent>(0);

            Assert.Throws<ArgumentException>(getComponent);
        }

        [Test]
        public void RemoveComponent_Remove_HasNoComponent()
        {
            uint entity = _world.CreateEntity();
            _world.AddComponent<SomeComponent>(entity);

            _world.RemoveComponent<SomeComponent>(entity);
            bool hasComponent = _world.HasComponent<SomeComponent>(entity);

            Assert.IsFalse(hasComponent);
        }

        [Test]
        public void GetComponentType_CheckType_ValidType()
        {
            int componentType = _world.GetComponentType<SomeComponent>();

            Assert.AreEqual(1, componentType);
        }

        [Test]
        public void RegisterSystem_NewSystem_DoesNotThrow()
        {
            EcsSystem system = new SomeSystem();

            TestDelegate registerSystem = () => _world.RegisterSystem(system);

            Assert.DoesNotThrow(registerSystem);
        }

        [Test]
        public void RegisterSystem_DoubleSystem_ThrowError()
        {
            EcsSystem system = new SomeSystem();

            _world.RegisterSystem(system);
            TestDelegate registerSystem = () => _world.RegisterSystem(system);

            Assert.Throws<ArgumentException>(registerSystem);
        }

        [Test]
        public void SetSystemSignature_NewSystem_DoesNotThrow()
        {
            EcsSystem system = new SomeSystem();
            BitVector32 signature = new BitVector32();

            _world.RegisterSystem(system);
            TestDelegate setSystemSignature = () => _world.SetSystemSignature<SomeSystem>(ref signature);

            Assert.DoesNotThrow(setSystemSignature);
        }

        [Test]
        public void SetSystemSignature_UnregisteredSystem_ThrowError()
        {
            BitVector32 signature = new BitVector32();

            TestDelegate setSystemSignature = () => _world.SetSystemSignature<SomeSystem>(ref signature);

            Assert.Throws<ArgumentException>(setSystemSignature);
        }
    }
}