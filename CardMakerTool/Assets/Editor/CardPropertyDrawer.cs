using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(CardStats))]
public class CardPropertyDrawer : PropertyDrawer
{
    float totalHeight = 0;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        SetHeight(0);

        PropertyDrawerHelper.DrawHorizontalProperty(
            position,
            20,
            SetHeight(20),
            property,
            new PropertyDrawerHelper.PropertyData("name", "", PropertyDrawerHelper.CustomPropertyType.String, 8
            ),
            new PropertyDrawerHelper.PropertyData("mana", "", PropertyDrawerHelper.CustomPropertyType.Integer, 1
            ),
            new PropertyDrawerHelper.PropertyData("type", "", PropertyDrawerHelper.CustomPropertyType.Color, 1
            )
            );

        // Check if the image is clicked to change its path
        if (Event.current.type == EventType.MouseDown && new Rect(position.x, position.y + GetPropertyHeight(property, label), position.width, 300).Contains(Event.current.mousePosition)) {
            SerializedProperty sp = property.FindPropertyRelative("imagePath");
            string absPath = EditorUtility.OpenFilePanel("Select Asset", "", "png");
            if (absPath.StartsWith(Application.dataPath))
                sp.stringValue = absPath.Substring(Application.dataPath.Length - "Assets".Length);
        }

        PropertyDrawerHelper.DrawHorizontalProperty(
            position,
            300,
            SetHeight(300),
            property,
            new PropertyDrawerHelper.PropertyData("imagePath", "", PropertyDrawerHelper.CustomPropertyType.Texture2D, 1
            )
            );

        PropertyDrawerHelper.DrawHorizontalProperty(
            position,
            200,
            SetHeight(200),
            property,
            new PropertyDrawerHelper.PropertyData("description", "", PropertyDrawerHelper.CustomPropertyType.LongString, 1
            )
            );

        PropertyDrawerHelper.DrawHorizontalProperty(
            new Rect(position.x + position.width*.8f, position.y, position.width*.2f, position.height),
            20,
            SetHeight(20),
            property,
            new PropertyDrawerHelper.PropertyData("power", "", PropertyDrawerHelper.CustomPropertyType.Integer, 1
            ),
            new PropertyDrawerHelper.PropertyData("defense", "", PropertyDrawerHelper.CustomPropertyType.Integer, 1
            )
            );

        EditorGUI.EndProperty();
    }

    public float SetHeight(float height) {
        float oldHeight = totalHeight;
        if (height != 0)
            totalHeight += height;
        else
            totalHeight = 0;
        return oldHeight;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return totalHeight;
    }
}
