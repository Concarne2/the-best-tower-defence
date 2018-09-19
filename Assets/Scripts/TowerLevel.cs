using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLevel : MonoBehaviour {
    public float attackInterval;
    public float turnSpeed;

    private Targetter targetter;
    private Transform target;

    public TowerLevelData levelData;

    public GameObject projectile;

    public Transform fireLocation;
    private float myTime = 0;

    public float range
    {
        get
        {
            return targetter.range;
        }

        set
        {
            targetter.range = range;
        }
    }

    private void Awake()
    {
        targetter = GetComponentInChildren<Targetter>();
    }

    private void FireBullet()
    {
        GameObject bullet = ObjectPooler.InstantiatePooled(projectile, fireLocation.position, fireLocation.rotation);
        BulletMovement movement = bullet.GetComponent<BulletMovement>();
        movement.setTarget(target);
    }

    void Update()
    {

        myTime += Time.deltaTime;

        target = targetter.findClosestTarget();
        if (target)
        {
            Quaternion lookrotation = Quaternion.LookRotation(target.position - transform.position);
            Vector3 dir = Quaternion.Lerp(transform.rotation, lookrotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0, dir.y, 0);
            if (myTime > attackInterval)
            {
                // Debug.Log("fire");
                myTime = 0;
                FireBullet();
            }
        }

    }

}
