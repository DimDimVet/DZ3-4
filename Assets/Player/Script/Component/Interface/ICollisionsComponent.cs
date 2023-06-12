using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionsComponent
{
    void Execute(Collider[] colliders);
}
