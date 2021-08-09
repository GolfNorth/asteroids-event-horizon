using System.Collections.Generic;
using NonUnity.Collision;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система столкновений
    /// </summary>
    public sealed class CollisionSystem : BaseSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<BodyComponent> _filter;

        /// <summary>
        /// Все сущности из фильтра
        /// </summary>
        private readonly List<uint> _entities;

        public CollisionSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<BodyComponent>(World);
            _entities = new List<uint>();
        }

        public void Update(float dt)
        {
            _entities.Clear();
            _entities.AddRange(_filter.Entities);

            for (int a = 0; a < _entities.Count - 1; a++)
            {
                for (int b = a + 1; b < _entities.Count; b++)
                {
                    uint entityA = _entities[a];
                    uint entityB = _entities[b];

                    ref BodyComponent bodyA = ref World.GetComponent<BodyComponent>(entityA);
                    ref BodyComponent bodyB = ref World.GetComponent<BodyComponent>(entityB);

                    if ((bodyA.Layer & bodyB.Mask) == 0 || !bodyA.Shape.Intersect(bodyB.Shape))
                        continue;

                    ref CollisionComponent collisionA = ref World.AddComponent<CollisionComponent>(entityA);
                    ref CollisionComponent collisionB = ref World.AddComponent<CollisionComponent>(entityB);

                    collisionA.Other = entityB;
                    collisionB.Other = entityA;
                }
            }
        }
    }
}