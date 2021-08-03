using System.Collections.Generic;
using System.Collections.Specialized;

namespace NonUnity.Ecs
{
    /// <summary>
    /// Фильтр сущностей
    /// </summary>
    public class EcsFilter
    {
        /// <summary>
        /// Пространство сущностей
        /// </summary>
        private readonly EcsWorld _world;

        /// <summary>
        /// Сигнатура фильтра
        /// </summary>
        private readonly BitVector32 _signature;

        /// <summary>
        /// Типы компонентов фильтра
        /// </summary>
        public IReadOnlyCollection<uint> Entities => _world.GetEntities(_signature);

        /// <summary>
        /// Конструктор фильтра
        /// </summary>
        /// <param name="world">Пространство сущностей</param>
        public EcsFilter(EcsWorld world) : this(world, world.AddFilter())
        {
        }

        /// <summary>
        /// Конструктор фильтра
        /// </summary>
        /// <param name="world">Пространство сущностей</param>
        /// <param name="signature">Сигнатура фильтра</param>
        internal EcsFilter(EcsWorld world, BitVector32 signature)
        {
            _world = world;
            _signature = signature;
        }
    }

    /// <summary>
    /// Фильтр сущностей
    /// </summary>
    /// <typeparam name="T">Тип компонента</typeparam>
    public sealed class EcsFilter<T> : EcsFilter where T : struct
    {
        /// <summary>
        /// Конструктор фильтра
        /// </summary>
        /// <param name="world">Пространство сущностей</param>
        public EcsFilter(EcsWorld world) : base(world, world.AddFilter<T>())
        {
        }
    }

    /// <summary>
    /// Фильтр сущностей
    /// </summary>
    /// <typeparam name="T1">Тип компонента</typeparam>
    /// <typeparam name="T2">Тип компонента</typeparam>
    public sealed class EcsFilter<T1, T2> : EcsFilter where T1 : struct where T2 : struct
    {
        /// <summary>
        /// Конструктор фильтра
        /// </summary>
        /// <param name="world">Пространство сущностей</param>
        public EcsFilter(EcsWorld world) : base(world, world.AddFilter<T1, T2>())
        {
        }
    }

    /// <summary>
    /// Фильтр сущностей
    /// </summary>
    /// <typeparam name="T1">Тип компонента</typeparam>
    /// <typeparam name="T2">Тип компонента</typeparam>
    /// <typeparam name="T3">Тип компонента</typeparam>
    public sealed class EcsFilter<T1, T2, T3> : EcsFilter where T1 : struct where T2 : struct where T3 : struct
    {
        /// <summary>
        /// Конструктор фильтра
        /// </summary>
        /// <param name="world">Пространство сущностей</param>
        public EcsFilter(EcsWorld world) : base(world, world.AddFilter<T1, T2, T3>())
        {
        }
    }
}