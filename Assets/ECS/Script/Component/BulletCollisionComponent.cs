using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BulletCollisionComponent : MonoBehaviour, IConvertGameObjectToEntity, IBulletCollisionComponent
{
    public Collider Collider;

    //������� � ���� �������� �������
    public List<MonoBehaviour> CollisionAction = new List<MonoBehaviour>();
    //�������� ���� ��� �������� ��������-�������� ����������� ICollisionsComponent 
    [HideInInspector] public List<IBulletCollisionComponent> ComponentCollisions = new List<IBulletCollisionComponent>();

    private void Start()
    {
        if (CollisionAction != null)//�������� �� null ���� ��������
        {
            for (int i = 0; i < CollisionAction.Count; i++)
            {
                if (CollisionAction[i] is IBulletCollisionComponent component)//�������� �� ������� ICollisionsComponent ���� ��������
                {
                    ComponentCollisions.Add(component);//������� � ���� ICollisionsComponent ��������
                }
                else
                {
                    Debug.Log("��� ����������� � IBulletCollisionComponent");
                }
            }
        }

    }

    //��������� ICollisionsComponent, ���� �������� �� CollisionSystem
    public void ExecuteBullet(List<Collider> colliders)
    {
        //�������� �� null ���� �ICollisionsComponent
        if (ComponentCollisions != null)
        {

            for (int i = 0; i < ComponentCollisions.Count; i++)
            {
                //������� ����� �� ���� �������� �� ����������� � ��������� ���� � ���������� ������������
                ComponentCollisions[i].ExecuteBullet(colliders);
            }
        }

    }

    //������������ � �������� ������� ������ �� ���� ����������
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;
        switch (Collider)
        {
            case SphereCollider sphere:
                //������� ��������� ���������� ������� �������
                sphere.ToWorldSpaceSphere(out float3 sphereCenter, out float sphereRadius);
                //������� � �������� ��������� ������� (����� ��������� ColliderData)
                entityManager.AddComponentData(entity, new ColliderData2
                {
                    TypeCollider = TypeCollider.Sphere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRadius,
                    isInitalTakeOff = true
                });
                break;

            case CapsuleCollider capsule:
                //������� ��������� ���������� ������� �������
                capsule.ToWorldSpaceCapsule(out float3 capsuleStart, out float3 capsuleEnd, out float capsuleRadius);
                //������� � �������� ��������� ������� (����� ��������� ColliderData)
                entityManager.AddComponentData(entity, new ColliderData2
                {
                    TypeCollider = TypeCollider.Capsule,
                    CapsuleStart = capsuleStart - position,
                    CapsuleEnd = capsuleEnd - position,
                    CapsuleRadius = capsuleRadius,
                    isInitalTakeOff = true
                });
                break;

            case BoxCollider box:
                //������� ��������� ���������� ������� �������
                box.ToWorldSpaceBox(out float3 boxCenter, out float3 boxHalfExtents, out quaternion boxOrientation);
                //������� � �������� ��������� ������� (����� ��������� ColliderData)
                entityManager.AddComponentData(entity, new ColliderData2
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
        //�������� ���������(������ ������ �������)
        Collider.enabled = false;
    }
}

//structure
public struct ColliderData2 : IComponentData
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