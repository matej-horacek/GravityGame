using System;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    private Transform playerTarget;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate = 2f;
    public GameObject player;

    private Transform PlayerTarget;
    private float nextFireTime = 0f;

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 spawnPosition;
        public Vector3 shootDirection;
    }

    void Awake()
    {
        playerTarget = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        AimAtPlayer();
        if(Time.time >= nextFireTime)
        {
            if (playerTarget != null)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }
    private void AimAtPlayer()
    {   
        //Debug.Log("aiming at position" + playerTarget.position);
        Vector3 direction = playerTarget.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = targetRotation.eulerAngles;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
    }
    public void Shoot()
    {
        Vector3 pos = firePoint.position;
        Vector3 dir = firePoint.forward;
        OnShoot?.Invoke(this, new OnShootEventArgs
        {
            spawnPosition = pos,
            shootDirection = dir
        });
    }
    /*void OnDrawGizmos()
    {
        if (firePoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(firePoint.position, 0.2f);
        }
    }*/
}
