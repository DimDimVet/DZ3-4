using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour, IShoot
{
    //public GameObject Bullet;
    public float ShootDelay;
    private float shootTime = float.MinValue;
    //
    [SerializeField] private ParticleSystem gunParticle;//������� ������
    [SerializeField] private ParticleSystem gunExitParticle;//������� ������

    void IShoot.Shoot()
    {
        if (Time.time < shootTime + ShootDelay)
        {
            return;
        }
        else
        {
            shootTime = Time.time;
        }

        if (gunParticle != null & gunExitParticle != null)
        {
            gunParticle.Play();//��� ������� �������� ��������� ������� ������
            gunExitParticle.Play();
        }
        else
        {
            Debug.Log($"{gunParticle} � {gunExitParticle} �� ��������");
        }
    }


}
