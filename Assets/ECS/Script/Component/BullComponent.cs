using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullComponent : MonoBehaviour/*, ICollisionsComponent*/
{
    public int Speed;
    public Collider CollaiderBullet;
    public Rigidbody RigidbodyBullet;
    public GameObject decalGO;

    private Vector3 startPos;
    private GameObject decal;
    public bool IsMode=false;
    private void Start()
    {
        startPos = transform.position;
    }
    //public void Execute(List<Collider> colliders)
    //{
    //    //isShoot = true;
    //}
    private void Update()
    {

        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        RaycastHit hit;
        if (Physics.Linecast(startPos, transform.position, out hit))  
        {
             if (IsMode)
            {
                //RigidbodyBullet.mass = 0;
                //RigidbodyBullet.AddForce(hit.point.x* Speed, hit.point.y* Speed, hit.point.z, ForceMode.Impulse);
            }
            else
            {
                CollaiderBullet.enabled = false;
                decal = Instantiate(decalGO);
                decal.transform.position = hit.point + hit.normal * 0.001f;
                decal.transform.rotation = Quaternion.LookRotation(-hit.normal);
                Destroy(decal, 1);

                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject,5);
        }
        startPos = transform.position;

    }
}
