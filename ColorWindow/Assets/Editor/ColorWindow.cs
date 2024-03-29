﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;  //Required for MenuItem, means that this is an Editor script, must be placed in an Editor folder, and cannot be compiled!
using System.Linq;  //Used for Select

public class ColorWindow : EditorWindow
{ //Now is of type EditorWindow

    [MenuItem("Custom Tools/ Color Window")] //This the function below it as a menu item, which appears in the tool bar
    public static void CreateShowcase() //Menu items can call STATIC functions, does not work for non-static since Editor scripts cannot be attached to objects
    {
        EditorWindow window = GetWindow<ColorWindow>("Color Window");
    }

    private Color[] colors;
    private int width = 8;
    private int height = 8;
    Texture colorTexture;
    Renderer textureTarget;

    Color selectedColor = Color.white;
    Color eraseColor = Color.white;
    float randomness = 0;
    Dictionary<int, Vector2Int> bucketList;

    public void OnEnable() {
        colors = new Color[width * height];
        for (int i = 0; i < colors.Length; i++)
            colors[i] = GetRandomColor();
        colorTexture = EditorGUIUtility.whiteTexture;
    }

    private Color GetRandomColor()  //Built a get random color tool
    {
        return new Color(Random.value, Random.value, Random.value, 1f);
    }

    void OnGUI() //Called every frame in Editor window
    {
        GUILayout.BeginHorizontal();        //Have each element below be side by side
        DoControls();
        DoCanvas();
        GUILayout.EndHorizontal();
    }

    void DoControls() {
        GUILayout.BeginVertical();                                                      //Start vertical section, all GUI draw code after this will belong to same vertical
        GUILayout.Label("ToolBar", EditorStyles.largeLabel);                            //A label that says "Toolbar"
        selectedColor = EditorGUILayout.ColorField("Paint Color", selectedColor);       //Make a color field with the text "Paint Color" and have it fill the selectedColor var
        eraseColor = EditorGUILayout.ColorField("Erase Color", eraseColor);             //Make a color field with the text "Erase Color"
        if (GUILayout.Button("Fill All"))                                               //A button, if pressed, returns true
            colors = colors.Select(c => c = selectedColor).ToArray();                   //Linq expresion, for every color in the color array, sets it to the selected color

        randomness = EditorGUILayout.DelayedFloatField("Randomness", randomness);
        if (randomness > 1)
            randomness = 1;
        if (randomness < 0)
            randomness = 0;

        GUILayout.FlexibleSpace();                                                      //Flexible space uses any left over space in the loadout
        textureTarget = EditorGUILayout.ObjectField("Output Renderer", textureTarget, typeof(Renderer), true) as Renderer;  //Build an object field that accepts a renderer

        if (GUILayout.Button("Save to Object")) {
            Texture2D t2d = new Texture2D(width, height);                               //Create a new texture
            t2d.filterMode = FilterMode.Point;                                          //Simplest non-blend texture mode
            textureTarget.material = new Material(Shader.Find("Diffuse"));              //Materials require Shaders as an arguement, Diffuse is the most basic type
            textureTarget.sharedMaterial.mainTexture = t2d;                             //sharedMaterial is the MAIN RESOURCE MATERIAL. Changing this will change ALL objects using it, .material will give you the local instance

            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    int index = j + i * height;
                    t2d.SetPixel(i, height - 1 - j, colors[index]);                     //Color every pixel using our color table, the texture is 8x8 pixels large, but strecthes to fit
                }
            }
            t2d.Apply();                                                                //Apply all changes to texture
        }
        GUILayout.EndVertical();                                                        //end vertical section
    }

    void DoCanvas() {
        Event evt = Event.current;                     //Grab the current event

        Color oldColor = GUI.color;                    //GUI color uses a static var, need to save the original to reset it
        GUILayout.BeginHorizontal();                   //All following gui will be on one horizontal line until EndHorizontal is called
        for (int i = 0; i < width; i++) {
            GUILayout.BeginVertical();                //All following gui will be in a vertical line
            for (int j = 0; j < height; j++) {
                int index = j + i * height;           //Rememeber, this is just like a 2D array, but in 1D
                Rect colorRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)); //Reserve a square, which will autofit to the size given
                if ((evt.type == EventType.MouseDown || evt.type == EventType.MouseDrag) && colorRect.Contains(evt.mousePosition)) //Can now paint while dragging update
                {
                    if (evt.button == 0)                //If mouse button pressed is left
                        colors[index] = new Color(Random.Range(selectedColor.r - randomness, selectedColor.r + randomness), Random.Range(selectedColor.g - randomness, selectedColor.g + randomness), Random.Range(selectedColor.b - randomness, selectedColor.b + randomness)); //Set the color of the index
                    else if (evt.button == 2) {
                        Bucket(i, j);
                    }
                    else
                        colors[index] = eraseColor;   //Set the color of the index
                    evt.Use();                        //The event was consumed, if you try to use event after this, it will be non-sensical
                }

                GUI.color = colors[index];            //Same as a 2D array
                GUI.DrawTexture(colorRect, colorTexture); //This is colored by GUI.Color!!!
            }
            GUILayout.EndVertical();                  //End Vertical Zone
        }
        GUILayout.EndHorizontal();                    //End horizontal zone
        GUI.color = oldColor;                         //Restore the old color
    }

    void Bucket(int w, int h) {
        safetyCount = 0;
        int clickedIndex = w * this.height + h;
        int index = clickedIndex;
        Color clickedColor = colors[index];
        colors[index] = selectedColor;

        bucketList = new Dictionary<int, Vector2Int>();

        CheckNeighbours(w, h, clickedColor);
    }

    static int safetyCount = 0;

    void CheckNeighbours(int w, int h, Color clickedColor) {
        safetyCount++;
        if(safetyCount > width * height) {
            return;
        }

        bucketList.Add(bucketList.Count, new Vector2Int(w, h));
        Dictionary<int, int[]> neighbours = new Dictionary<int, int[]>();

        if (w + 1 < width)
            neighbours.Add(neighbours.Count, new int[] { w + 1, h });
        if (w - 1 >= 0)
            neighbours.Add(neighbours.Count, new int[] { w - 1, h });
        if (h + 1 < height)
            neighbours.Add(neighbours.Count, new int[] { w, h + 1 });
        if (h - 1 >= 0)
            neighbours.Add(neighbours.Count, new int[] { w, h - 1 });

        foreach (KeyValuePair<int, int[]> entry in neighbours) {
            int index = entry.Value[0] * this.height + entry.Value[1];
            if (colors[index] == clickedColor) {
                colors[index] = selectedColor;
                if (bucketList.ContainsValue(new Vector2Int(entry.Value[0], entry.Value[1])) == false)
                    CheckNeighbours(entry.Value[0], entry.Value[1], clickedColor);
            }
        }
    }
}
