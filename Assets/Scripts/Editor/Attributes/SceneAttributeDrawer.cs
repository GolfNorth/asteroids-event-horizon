using System;
using System.Collections.Generic;
using System.IO;
using Asteroids.Common;
using UnityEditor;
using UnityEngine;

namespace Asteroids.Editor
{
    /// <summary>
    /// Отрисовщик атрибутка <see cref="SceneAttribute"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public sealed class SceneAttributeDrawer : PropertyDrawer
    {
        /// <summary>
        /// Список всех сцен
        /// </summary>
        private readonly string[] _scenes;

        public SceneAttributeDrawer()
        {
            _scenes = GetScenes();
        }

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            int selected = Array.IndexOf(_scenes, property.stringValue);

            EditorGUI.BeginChangeCheck();

            int index = EditorGUI.Popup(rect, label.text, selected, _scenes);

            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = _scenes[index];
            }
        }

        /// <summary>
        /// Получить список сцен
        /// </summary>
        private string[] GetScenes()
        {
            List<string> result = new List<string>();

            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (!scene.enabled)
                    continue;

                string name = Path.GetFileNameWithoutExtension(scene.path);

                result.Add(name);
            }

            return result.ToArray();
        }
    }
}