using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollisionsComponent : MonoBehaviour, IConvertGameObjectToEntity, ICollisionsComponent
{
    public Collider Collider;

    public List<MonoBehaviour> CollisionAction = new List<MonoBehaviour>();
    [HideInInspector] public List<IActionCollision> CollisionsActionComponent = new List<IActionCollision>();

    [HideInInspector] public List<Collider> collisionsList;

    private void Start()
    {
        for (int i = 0; i < CollisionAction.Count; i++)
        {
            if (CollisionAction[i] is IActionCollision component)
            {
                CollisionsActionComponent.Add(component);
            }
            else
            {
                Debug.Log("Нет компонентов с IActionCollision");
            }
        }
    }
    public void Execute()
    {
        if (CollisionsActionComponent!=null)
        {

            for (int i = 0; i < CollisionsActionComponent.Count; i++)
            {
                CollisionsActionComponent[i].ListGameObjects = new List<GameObject>();

                collisionsList.ForEach(coll=>
                {
                    CollisionsActionComponent[i].ListGameObjects.Add(coll.gameObject);
                });

                CollisionsActionComponent[i].Execute();
            }
        }
       
    }
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;
        switch (Collider)
        {
            case SphereCollider sphere:
                sphere.ToWorldSpaceSphere(out float3 sphereCenter, out float sphereRadius);
                entityManager.AddComponentData(entity, new ColliderData
                {
                    TypeCollider = TypeCollider.Sphere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRadius,
                    isInitalTakeOff = true
                });
                break;

            case CapsuleCollider capsule:
                capsule.ToWorldSpaceCapsule(out float3 capsuleStart, out float3 capsuleEnd, out float capsuleRadius);
                entityManager.AddComponentData(entity, new ColliderData
                {
                    TypeCollider = TypeCollider.Capsule,
                    CapsuleStart = capsuleStart - position,
                    CapsuleEnd = capsuleEnd - position,
                    CapsuleRadius = capsuleRadius,
                    isInitalTakeOff = true
                });
                break;

            case BoxCollider box:
                box.ToWorldSpaceBox(out float3 boxCenter, out float3 boxHalfExtents, out quaternion boxOrientation);
                entityManager.AddComponentData(entity, new ColliderData
                {
                    TypeCollider = TypeCollider.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExtents = boxHalfExtents,
                    BoxOrientation = boxOrientation,
                    isInitalTakeOff = true
                });
                break;

            default:
                break;
        }

        Collider.enabled = false;
    }

}

//structure
public struct ColliderData : IComponentData
{
    public TypeCollider TypeCollider;
    public float3 SphereCenter;
    public float SphereRadius;
    public float3 CapsuleStart;
    public float3 CapsuleEnd;
    public float CapsuleRadius;
    public float3 BoxCenter;
    public float3 BoxHalfExtents;
    public quaternion BoxOrientation;
    public bool isInitalTakeOff;
}

public enum TypeCollider
{
    Sphere=0,
    Capsule=1,
    Box=2
}
