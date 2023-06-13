using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullComponent : MonoBehaviour
{
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform outBullet;
    //private GameObject currentBullet;
    private Rigidbody currentBulletVelocity;
    public float ShootDelay;
    private float shootTime = float.MinValue;
    //public int Speed;
    //[SerializeField]private GameObject lastPosGO;
    //private Vector3 lastPos;
    // Start is called before the first frame update
    void Start()
    {
        //currentBulletVelocity = bullet.GetComponent<Rigidbody>();
        //lastPos = lastPosGO.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < shootTime + ShootDelay)
        {
            return;
        }
        else
        {
            shootTime = Time.time;
        }
        currentBulletVelocity = Instantiate(bullet, outBullet.position, outBullet.rotation);
        currentBulletVelocity.AddForce(outBullet.up* bulletSpeed);
        //currentBullet = Instantiate(bullet,outBullet.transform.position,Quaternion.identity);
        //currentBulletVelocity.velocity = new Vector3(bulletSpeed*-1, 0,currentBulletVelocity.velocity.z);

        //transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        //RaycastHit hit;

        //Debug.DrawLine(lastPos,transform.position);
        //if (Physics.Linecast(lastPos, transform.position, out hit))
        //{
        //    print(hit.transform.name);
        //    Destroy(gameObject);
        //}
        //lastPos = transform.position;
    }
}
