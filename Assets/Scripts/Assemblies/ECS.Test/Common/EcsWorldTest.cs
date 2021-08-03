using System;
using NonUnity.Ecs;
using NUnit.Framework;

namespace NonUnity.ECS.Test
{
    internal struct SomeComponent
    {
        public int Value;
    }

    [TestFixture]
    public sealed class EcsWorldTest
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
            EcsFilter filter = new EcsFilter(_world);

            int entityCount = filter.Entities.Count;

            Assert.AreEqual(0, entityCount);
        }

        [Test]
        public void CreateEntity_NewEntity_HasEntity()
        {
            EcsFilter filter = new EcsFilter(_world);
            uint entity = _world.CreateEntity();

            int entityCount = filter.Entities.Count;

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
            EcsFilter filter = new EcsFilter(_world);
            uint entity = _world.CreateEntity();

            _world.DestroyEntity(entity);
            int entityCount = filter.Entities.Count;

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
    }
}