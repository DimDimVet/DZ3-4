using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DamageCollision : MonoBehaviour, ICollisionsComponent
{
    public int Damage = 10;

    public void Execute(List<Collider> colliders)
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            HealtComponent healt = colliders[i].GetComponent<HealtComponent>();
            if (healt!=null)
            {
                healt.Healt -= Damage;
            }
        }
        float3 temp = gameObject.transform.position;
        temp.z = 40f;
        gameObject.transform.position = temp;
    }
}
