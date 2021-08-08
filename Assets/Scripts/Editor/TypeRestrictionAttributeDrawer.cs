using System;
using Asteroids.Common;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Asteroids.Editor
{
    /// <summary>
    /// Отрисовщик атрибутка <see cref="TypeRestrictionAttribute"/>.
    /// Проверяет тип поля и если оно не соответсвует, то пытается найти нужный объект или сбрасывает значение.
    /// </summary>
    [CustomPropertyDrawer(typeof(TypeRestrictionAttribute))]
    public class TypeRestrictionAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _rect, SerializedProperty _property, GUIContent _label)
        {
            if (_property.propertyType != SerializedPropertyType.ObjectReference)
                return;

            if (!(attribute is TypeRestrictionAttribute restriction))
                return;

            EditorGUI.BeginChangeCheck();

            Object field = EditorGUI.ObjectField(_rect, _label, _property.objectReferenceValue, typeof(Object),
                restriction.AllowSceneObjects);

            if (EditorGUI.EndChangeCheck())
            {
                if (field != null)
                {
                    Type type = field.GetType();

                    if (!restriction.Type.IsAssignableFrom(type))
                    {
                        if (field is GameObject gameObject)
                        {
                            field = gameObject.GetComponent(restriction.Type);
                        }
                        else if (field is Component component)
                        {
                            field = component.gameObject.GetComponent(restriction.Type);
                        }
                        else
                        {
                            field = null;
                        }
                    }
                }

                _property.objectReferenceValue = field;
            }
        }
    }
}