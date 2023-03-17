using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;

public class CardPreview : EditorWindow
{
    [MenuItem("Card Games/Card Preview")]
    public static void CreateShowcase() {
        EditorWindow window = GetWindow<CardPreview>("Card Preview");
    }

    public List<CardInfo> cards;
    public CardInfo activeCard;
    int cardIndex;

    public void OnEnable() {
        cards = new List<CardInfo>(Resources.LoadAll<CardInfo>("Cards"));
        if (cards.Count > 0) {
            cardIndex = 0;
            activeCard = cards[cardIndex];
        }
    }

    private void OnGUI() {
        DrawTop();
        DrawMiddle();
        DrawBottom();
    }

    void DrawTop() {
        GUILayout.BeginHorizontal();


        if (cards.Count > 0)
            GUILayout.Label(activeCard.name);
        else
            GUILayout.Label("");

        GUILayout.FlexibleSpace();

        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.alignment = TextAnchor.UpperRight;

        GUILayout.BeginVertical();
        if (GUILayout.Button("Load")) {
            Load();
        }
        if (GUILayout.Button("Go to File")) {
            OpenFileLocation();
        }
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    void DrawMiddle() {
        GUILayout.BeginHorizontal();

        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.alignment = TextAnchor.MiddleLeft;

        if (GUILayout.Button("<<<")) {
            Previous();
        }

        // GUILayout.FlexibleSpace();

        GUILayout.BeginVertical();
        if (cards.Count > 0) {
            Editor selectedCardEditor = Editor.CreateEditor(activeCard);
            selectedCardEditor.OnInspectorGUI();
        }
        GUILayout.EndVertical();

        // GUILayout.FlexibleSpace();

        style.alignment = TextAnchor.MiddleRight;

        if (GUILayout.Button(">>>")) {
            Next();
        }

        GUILayout.EndHorizontal();
    }

    void DrawBottom() {
        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.alignment = TextAnchor.LowerCenter;

        if (GUILayout.Button("Create New")) {
            Create();
        }

        GUILayout.FlexibleSpace();

        GUILayout.EndHorizontal();
    }

    void Load() {
        string absPath = EditorUtility.OpenFilePanel("Select Asset", "", "asset");
        if (absPath.StartsWith(Application.dataPath)) {
            string path = absPath.Replace(Application.dataPath + "/Resources/", "");
            path = path.Replace(".asset", "");
            activeCard = Resources.Load<CardInfo>(path);
        }
    }

    void OpenFileLocation() {
        string path = Application.dataPath + "/Resources/Cards/" + activeCard.name;
        path = path.Replace(@"/", @"\");
        EditorUtility.RevealInFinder(path);
    }

    void Previous() {
        if (cardIndex == 0) {
            cardIndex = cards.Count - 1;
        }
        else {
            cardIndex--;
        }
        if (cards.Count > 0)
            activeCard = cards[cardIndex];
    }

    void Next() {
        if (cardIndex == cards.Count - 1) {
            cardIndex = 0;
        }
        else {
            cardIndex++;
        }
        if (cards.Count > 0)
            activeCard = cards[cardIndex];
    }

    void Create() {
        CardInfo cardInfo = ScriptableObject.CreateInstance<CardInfo>();
        AssetDatabase.CreateAsset(cardInfo, "Assets/Resources/Cards/card" + cards.Count);
        AssetDatabase.SaveAssets();

        cards.Add(cardInfo);
        activeCard = cards[cards.Count - 1];
    }
}