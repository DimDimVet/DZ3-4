using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollision : MonoBehaviour, IActionCollision
{
    public int Damage = 10;
    public List<GameObject> ListGameObjects { get; set; }

    public void Execute()
    {
        for (int i = 0; i < ListGameObjects.Count; i++)
        {
            HealtComponent healt = ListGameObjects[i].GetComponent<HealtComponent>();
            if (healt!=null)
            {
                healt.Healt -= Damage;
            }
        }
    }
}
