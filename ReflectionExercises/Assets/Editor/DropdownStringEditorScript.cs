using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomPropertyDrawer(typeof(DropdownStringAttribute))]
public class DropdownStringEditorScript : PropertyDrawer
{
    string dropdownText;
    int selectedIndex;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        DropdownStringAttribute dropdown = attribute as DropdownStringAttribute;
        if (dropdownText == "")
            selectedIndex = 0;

        if (property.type == "string") {
            selectedIndex = EditorGUI.Popup(position, selectedIndex, dropdown.options);
            dropdownText = dropdown.options[selectedIndex];
            property.stringValue = dropdownText;
        } else {
            Debug.LogWarning("Only string fields can use the attribute DropdownString");
        }

    }
}
