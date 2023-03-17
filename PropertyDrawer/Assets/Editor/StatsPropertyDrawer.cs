using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Stats))]
public class StatsPropertyDrawer : PropertyDrawer
{
    const float LINE_HEIGHT = 20;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

        EditorGUI.BeginProperty(position, label, property);

        PropertyDrawerHelper.DrawHorizontalProperty("Position:",
            position,
            LINE_HEIGHT,
            LINE_HEIGHT * 0,
            property,
            new PropertyDrawerHelper.PropertyData("x", "X", SerializedPropertyType.Float
            ),
            new PropertyDrawerHelper.PropertyData("y", "Y", SerializedPropertyType.Float
            ),
            new PropertyDrawerHelper.PropertyData("z", "Z", SerializedPropertyType.Float
            )
            );

        EditorGUI.EndProperty();



        //Objective: Given a label and many variables to renderer, render them like a horizontal layout group





    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return LINE_HEIGHT * 2;
    }
}

