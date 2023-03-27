using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomPropertyDrawer(typeof(ResetableAttribute))]
public class ResetableAttributeEditorScript : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        ResetableAttribute resetable = attribute as ResetableAttribute;
    }
}
