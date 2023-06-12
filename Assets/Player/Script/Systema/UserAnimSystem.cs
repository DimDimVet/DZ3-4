using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class UserAnimSystem : ComponentSystem
{
    private EntityQuery animQuery;
    protected override void OnCreate()
    {
        animQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
                                   ComponentType.ReadOnly<UserInputDataComponent>());
    }
    protected override void OnUpdate()
    {
        Entities.With(animQuery).ForEach
            (
            (Entity entity, UserInputDataComponent userInput, ref InputData inputData) =>
            {
                if (userInput.CurrentAnim != null && userInput.CurrentAnim is IAnimComponent ability)
                {
                    //pull
                    bool isPull;
                    if (inputData.Pull > 0f)
                    {
                        isPull = true;
                    }
                    else
                    {
                        isPull = false;
                    }
                    ability.GetPull(isPull);

                    //move
                    float2 move; 
                    if (inputData.Move.x != 0f | inputData.Move.y != 0f)
                    {
                        move = inputData.Move;
                    }
                    else
                    {
                        move.x = 0f;
                        move.y = 0f;
                    }
                    ability.GetMove(move);

                }
            }
            );
    }
}
