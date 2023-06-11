using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class UserInputSystem : ComponentSystem
{
    private EntityQuery inputQuery;
    //private InputAction inputAction;
    //private InputAction shootAction;
    //private InputAction pullAction;
    private MapCurrent inputAction;

    private float2 moveInput;
    private float shootInput;
    private float pullInput;

    protected override void OnCreate()
    {
        inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }

    protected override void OnStartRunning()
    {
        inputAction = new MapCurrent();

        inputAction.UIMap.WASD.performed += context => { moveInput = context.ReadValue<Vector2>(); };
        inputAction.UIMap.WASD.started += context => { moveInput = context.ReadValue<Vector2>(); };
        inputAction.UIMap.WASD.canceled += context => { moveInput = context.ReadValue<Vector2>(); };

        inputAction.Map.WASD.performed += context => { moveInput = context.ReadValue<Vector2>(); };
        inputAction.Map.WASD.started += context => { moveInput = context.ReadValue<Vector2>(); };
        inputAction.Map.WASD.canceled += context => { moveInput = context.ReadValue<Vector2>(); };

        inputAction.Map.Shoot.performed += context => { shootInput = context.ReadValue<float>(); };
        inputAction.Map.Shoot.started += context => { shootInput = context.ReadValue<float>(); };
        inputAction.Map.Shoot.canceled += context => { shootInput = context.ReadValue<float>(); };

        inputAction.Map.Pull.performed += context => { pullInput = context.ReadValue<float>(); };
        inputAction.Map.Pull.started += context => { pullInput = context.ReadValue<float>(); };
        inputAction.Map.Pull.canceled += context => { pullInput = context.ReadValue<float>(); };

        inputAction.Enable();
        //inputAction = new InputAction(name: "move", binding: "<Gamepad>/rightStick");

        //inputAction.AddCompositeBinding("Dpad")
        //    .With(name: "Up", binding: "<Keyboard>/w")
        //    .With(name: "Down", binding: "<Keyboard>/s")
        //    .With(name: "Left", binding: "<Keyboard>/a")
        //    .With(name: "Right", binding: "<Keyboard>/d");

        //inputAction.performed += context => { moveInput = context.ReadValue<Vector2>(); };
        //inputAction.started += context => { moveInput = context.ReadValue<Vector2>(); };
        //inputAction.canceled += context => { moveInput = context.ReadValue<Vector2>(); };
        //inputAction.Enable();
        ////
        //shootAction = new InputAction(name: "shoot", binding: "<Keyboard>/Space");

        //shootAction.Map.Shoot.performed += context => { shootInput = context.ReadValue<float>(); };
        //shootAction.Map.Shoot.started += context => { shootInput = context.ReadValue<float>(); };
        //shootAction.Map.Shoot.canceled += context => { shootInput = context.ReadValue<float>(); };
        //shootAction.Enable();
        ////
        //pullAction = new InputAction(name: "pull", binding: "<Keyboard>/Tab");

        //pullAction.performed += context => { pullInput = context.ReadValue<float>(); };
        //pullAction.started += context => { pullInput = context.ReadValue<float>(); };
        //pullAction.canceled += context => { pullInput = context.ReadValue<float>(); };
        //pullAction.Enable();
    }

    protected override void OnStopRunning()
    {
        inputAction.Disable();
        //shootAction.Disable();
        //pullAction.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.With(inputQuery).ForEach
            (
            (Entity entity, ref InputData inputData) =>
            {
                inputData.Move = moveInput;
                inputData.Shoot = shootInput;
                inputData.Pull = pullInput;
            }
            );
    }
}

