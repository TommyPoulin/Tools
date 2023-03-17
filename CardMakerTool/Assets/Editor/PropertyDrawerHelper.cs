using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PropertyDrawerHelper
{
    public enum CustomPropertyType
    {
        Boolean,
        Float,
        Integer,
        String,
        LongString,
        Color,
        Texture2D
    }

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

    public static void DrawHorizontalProperty(Rect position, float lineHeight, float yOffset, SerializedProperty property, params PropertyData[] properties) {
        float totalWeight = 0;
        for (int i = 0; i < properties.Length; i++) {
            totalWeight += properties[i].weight;
        }
        float currentX = position.x;
        for (int i = 0; i < properties.Length; i++) {
            Rect propertyRect = new Rect(currentX, position.y + yOffset, position.width * (properties[i].weight / totalWeight), lineHeight);

            SerializedProperty sp = property.FindPropertyRelative(properties[i].propertyName);

            if (properties[i].propertyType != CustomPropertyType.Texture2D)
                DrawField(propertyRect, sp, properties[i]);
            else {
                DrawField(propertyRect, sp, properties[i], sp.stringValue);
            }
            currentX += propertyRect.width;
        }
    }

    public static void DrawField(Rect position, SerializedProperty property, PropertyData data, string imagePath = "") {
        CustomPropertyType propertyType = data.propertyType;

        if (propertyType == CustomPropertyType.Boolean) {
            property.boolValue = EditorGUI.Toggle(position, property.boolValue);
        }
        else if (propertyType == CustomPropertyType.Integer) {
            property.intValue = EditorGUI.IntField(position, property.intValue);
        }
        else if (propertyType == CustomPropertyType.Float) {
            property.floatValue = EditorGUI.FloatField(position, property.floatValue);
        }
        else if (propertyType == CustomPropertyType.String) {
            property.stringValue = EditorGUI.TextField(position, property.stringValue);
        }
        else if (propertyType == CustomPropertyType.Color) {
            property.colorValue = EditorGUI.ColorField(position, property.colorValue);
        }
        else if (propertyType == CustomPropertyType.Texture2D) {
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            if (texture)
                EditorGUI.DrawPreviewTexture(position, texture);
            else {
                EditorGUI.LabelField(position, new GUIContent("Texture not found."));
            }
        }
        else if (propertyType == CustomPropertyType.LongString) {
            GUIStyle style = new GUIStyle(EditorStyles.textArea);
            style.wordWrap = true;

            property.stringValue = EditorGUI.TextArea(position, property.stringValue, style);
        }
        else {
            EditorGUI.PropertyField(position, property, new GUIContent(data.label));
            Debug.Log("Property Type not handled by the PropertyDrawHelper.");
        }
    }

    public class PropertyData
    {
        public string propertyName;
        public string label;
        public CustomPropertyType propertyType;
        public float weight;

        public PropertyData(string propertyName, string label, CustomPropertyType propertyType, int weight) {
            this.propertyName = propertyName;
            this.label = label;
            this.propertyType = propertyType;
            this.weight = weight;
        }
    }
}
