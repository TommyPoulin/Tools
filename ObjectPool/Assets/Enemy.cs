using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Shoot() {
        Bullet b = GameManager.bulletSystem.factory.Create(Bullet.BulletType.Laser);
    }
}
