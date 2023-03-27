using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameOverCaller))]
public class GameOverEditorScript : Editor
{
    public override void OnInspectorGUI() {
        if (GUILayout.Button("GameOver")) {
            GameOverCaller gameOverCaller = (GameOverCaller)target;
            gameOverCaller.GameOver();
        }
    }
}
