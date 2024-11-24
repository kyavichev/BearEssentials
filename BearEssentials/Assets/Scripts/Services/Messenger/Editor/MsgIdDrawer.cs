#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Bears.Core.Editor
{
    /// <summary>
    /// A property drawer for MsgId. In the future we can convert the name field into a dropdown.
    /// </summary>
    [CustomPropertyDrawer(typeof(SerializedMsgId), useForChildren:true)]
    [CustomPropertyDrawer(typeof(SerializedMsgId<>), useForChildren:true)]
    [CustomPropertyDrawer(typeof(SerializedMsgId<,>), useForChildren:true)]
    [CustomPropertyDrawer(typeof(SerializedMsgId<,,>), useForChildren:true)]
    [CustomPropertyDrawer(typeof(SerializedMsgId<,,,>), useForChildren:true)]
    public class MsgIdDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Type baseType = GetBaseType(fieldInfo.FieldType);

            if (baseType.IsGenericType)
            {
                Type[] typeArgs =  baseType.GenericTypeArguments;
                
                label.text += " (";
                for (int i = 0; i < typeArgs.Length; i++)
                {
                    label.text += i > 0 ? "," + typeArgs[i].Name : typeArgs[i].Name;
                }
                label.text += ")";
            }
            
            EditorGUI.PropertyField(position, property, label, property.isExpanded);

            // update hash
            var nameProp = property.FindPropertyRelative("_name");
            var hashProp = property.FindPropertyRelative("_hash");
            hashProp.intValue = nameProp.stringValue.GetHashCode();
        }

        private static Type GetBaseType(Type type)
        {
            Type baseType = type;
            while (baseType.BaseType != null && baseType.BaseType != typeof(object))
            {
                baseType = baseType.BaseType;
            }
            return baseType;
        }
    }
}
#endif // UNITY_EDITOR