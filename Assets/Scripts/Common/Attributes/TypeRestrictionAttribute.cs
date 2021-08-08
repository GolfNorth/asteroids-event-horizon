using System;
using UnityEngine;

namespace Asteroids.Common
{
    /// <summary>
    /// Атрибут ограничения значения поля по заданному типу. Применимо только к <see cref="UnityEngine.Object"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class TypeRestrictionAttribute : PropertyAttribute
    {
        public Type Type;
        public bool AllowSceneObjects = true;

        public TypeRestrictionAttribute(Type _type)
        {
            Type = _type;
        }
    }
}