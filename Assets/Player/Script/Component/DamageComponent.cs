using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : CollisionsComponent, ICollisionsComponent
{
    public int Damage = 10;

    public void Execute(List<Collider> colliders)//получим лист коллайдеров при столкновении
    {
        if (colliders!=null)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
               HealtComponent tempItem = colliders[i].gameObject.GetComponent<HealtComponent>();
                if (tempItem!=null)
                {
                    tempItem.Healt -= Damage;
                }
            }
        }
    }
}
