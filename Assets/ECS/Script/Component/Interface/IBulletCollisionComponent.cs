using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletCollisionComponent 
{
    void ExecuteBullet(List<Collider> colliders);
}
