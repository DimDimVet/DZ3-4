using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionCollision : ICollisionsComponent
{
    List<GameObject> ListGameObjects { get; set; }
}
