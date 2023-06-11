using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public interface IAnim
{
    void GetMove(float2 currentMove);
    void GetPull(bool currentPull);
}
