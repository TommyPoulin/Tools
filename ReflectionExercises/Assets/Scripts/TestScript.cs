using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [DropdownString("John", "Rick", "Bob")] public string nameChoice;
    [DropdownString("Paul", "Bob", "Jeff")] public string age;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.N))
            OutputName();
    }

    public void OutputName() {
        Debug.Log(nameChoice);
    }

    [CallOnGameOver]
    public void GameOver() {
        Debug.Log("Test script -> Game Over");
    }
}

public class Cat
{
    string loser;

    bool Dumb() {
        return true;
    }

    public bool Dog() {
        return false;
    }
}
