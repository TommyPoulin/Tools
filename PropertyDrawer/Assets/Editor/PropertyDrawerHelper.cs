using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PropertyDrawerHelper
{
    public static void DrawHorizontalProperty(string label, Rect position, float lineHeight, float yOffset, SerializedProperty property, params PropertyData[] properties) {
        Rect labelRect = new Rect(position.x, position.y + yOffset, position.width * .3f, lineHeight);
        EditorGUI.LabelField(labelRect, label);

        for (int i = 0; i < properties.Length; i++) {
            Rect propertyLabelRect = new Rect(position.x + labelRect.width + (position.width * .7f / properties.Length * i), position.y + yOffset, (position.width * .7f / properties.Length * .3f), lineHeight);
            Rect propertyRect = new Rect(propertyLabelRect.x + propertyLabelRect.width, position.y + yOffset, (position.width * .7f / properties.Length * .6f), lineHeight);

            SerializedProperty sp = property.FindPropertyRelative(properties[i].propertyName);
            EditorGUI.LabelField(propertyLabelRect, properties[i].label);
            DrawField(propertyRect, sp, properties[i]);
        }
    }

    public static void DrawField(Rect position, SerializedProperty property, PropertyData data) {
        SerializedPropertyType propertyType = data.propertyType;

        if (propertyType == SerializedPropertyType.Boolean) {
            property.boolValue = EditorGUI.Toggle(position, property.boolValue);
        }
        else if (propertyType == SerializedPropertyType.Integer) {
            property.intValue = EditorGUI.IntField(position, property.intValue);
        }
        else if (propertyType == SerializedPropertyType.Float) {
            property.floatValue = EditorGUI.FloatField(position, property.floatValue);
        }
        else if (propertyType == SerializedPropertyType.String) {
            property.stringValue = EditorGUI.TextField(position, property.stringValue);
        } else {
            EditorGUI.PropertyField(position, property, new GUIContent(data.label));
            Debug.Log("Property Type not handled by the PropertyDrawHelper.");
        }
    }

    public class PropertyData
    {
        public string propertyName;
        public string label;
        public SerializedPropertyType propertyType;

        public PropertyData(string propertyName, string label, SerializedPropertyType propertyType) {
            this.propertyName = propertyName;
            this.label = label;
            this.propertyType = propertyType;
        }
    }
}
