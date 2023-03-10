using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable, IManagable, IGetType<Bullet.BulletType>
{
    public enum BulletType
    {
        Bullet,
        Laser
    }

    public BulletType bulletType;
    public bool alive;

    private void Awake() {
        alive = false;
    }

    private void Initialize() {
        transform.position = new Vector3(Random.Range(-3, 3), Random.Range(3, 4), Random.Range(-3, 3));
    }

    public void Pool() {

    }

    public void Depool() {
        alive = true;
        Initialize();
    }

    public void Refresh() {
        if (transform.position.y < -1)
            alive = false;
    }

    public void FixedRefresh() {
        transform.position -= new Vector3(0, .4f, 0) * Time.deltaTime;
    }

    public bool IsAlive() {
        return alive;
    }

    BulletType IGetType<BulletType>.GetEnumType() {
        return bulletType;
    }
}
