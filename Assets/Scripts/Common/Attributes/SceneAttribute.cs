using System;
using UnityEngine;

namespace Asteroids.Common
{
    /// <summary>
    /// Атрибут выбора сцены
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SceneAttribute : PropertyAttribute
    {
    }
}