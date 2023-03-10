using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<GameObject> enemies;
    public int enemiesToSpawn = 10;

    private void Start() {
        enemies = new List<GameObject>();
        for (int i = 0; i < enemiesToSpawn; i++) {
            enemies.Add(GenerateEnemy());
        }
    }

    private void Update() {

    }

    private GameObject GenerateEnemy() {
        GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/");
        return GameObject.Instantiate(gameObjects[Random.Range(0, gameObjects.Length)]);
    }
}
