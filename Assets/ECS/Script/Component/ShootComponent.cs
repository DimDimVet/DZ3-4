using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour, IShootComponent
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform outBullet;
    //private Rigidbody currentBulletVelocity;
    //private GameObject bulletGO;
    public float ShootDelay;
    private float shootTime = float.MinValue;
    //private void Start()
    //{
    //    bulletGO = GetComponent<GameObject>();
    //}

    public void Shoot()
    {
        if (Time.time < shootTime + ShootDelay)
        {
            return;
        }
        else
        {
            shootTime = Time.time;
        }

        Instantiate(bullet, outBullet.position, outBullet.rotation);
       // currentBulletVelocity.AddForce(outBullet.up * bulletSpeed, ForceMode.Force);

    }
}
