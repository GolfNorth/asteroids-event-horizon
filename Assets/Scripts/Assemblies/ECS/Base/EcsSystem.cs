using System.Collections.Generic;

namespace NonUnity.Ecs
{
    /// <summary>
    /// Базовый класс системы
    /// </summary>
    public abstract class EcsSystem
    {
        /// <summary>
        /// Коллекция сущностей системы
        /// </summary>
        public HashSet<uint> Entities { get; }

        /// <summary>
        /// Конструктор базовой системы
        /// </summary>
        protected EcsSystem()
        {
            Entities = new HashSet<uint>();
        }
    }
}