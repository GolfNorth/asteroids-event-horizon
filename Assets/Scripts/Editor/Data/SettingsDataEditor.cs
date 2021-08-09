using Asteroids.Data;
using UnityEditor;
using UnityEngine;

namespace Asteroids.Editor
{
    [CustomEditor(typeof(SettingsData), true)]
    public sealed class SettingsDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SettingsData settingsData = (SettingsData) target;

            if (GUILayout.Button("Reset settings"))
            {
                settingsData.Default();
                EditorUtility.SetDirty(settingsData);
            }
        }
    }
}