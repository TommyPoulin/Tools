using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static FactoryPattern<Bullet, Bullet.BulletType> bulletSystem = new FactoryPattern<Bullet, Bullet.BulletType>();

    public float spawnDelay = 1f;
    public float nextSpawn = 0;
    void Start() {
        bulletSystem.Initialize();
    }

    void Update() {
        if (Time.time >= nextSpawn) {
            nextSpawn = Time.time + spawnDelay;
            Bullet b = bulletSystem.factory.Create(Bullet.BulletType.Bullet);
            Debug.Log("");
        }
    }


}
