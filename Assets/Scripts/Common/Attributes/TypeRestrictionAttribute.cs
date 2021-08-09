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
        public bool AllowSceneObjects;

        public TypeRestrictionAttribute(Type type, bool allowSceneObjects = true)
        {
            Type = type;
            AllowSceneObjects = allowSceneObjects;
        }
    }
}