using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullComponent : MonoBehaviour/*, ICollisionsComponent*/
{
    public int Speed;
    public Collider CollaiderBullet;
    public GameObject decalGO;

    private Vector3 startPos;
    private GameObject decal;
    private bool isShoot = true;
    private void Start()
    {
        startPos = transform.position;
    }
    public void Execute(List<Collider> colliders)
    {
        //isShoot = true;
    }
    private void Update()
    {

        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        RaycastHit hit;
        if (Physics.Linecast(startPos, transform.position, out hit))
        {
            CollaiderBullet.enabled = false;
            decal = Instantiate(decalGO);
            decal.transform.position = hit.point + hit.normal * 0.001f;
            decal.transform.rotation = Quaternion.LookRotation(-hit.normal);
            Destroy(decal, 1);

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject,5);
        }
        startPos = transform.position;
        //isShoot = false;


    }
}
