using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLevel : MonoBehaviour {
    public float turnSpeed;

    private Targetter targetter;
    private Transform target;

    public TowerLevelData levelData;

    public GameObject projectile;

    public Transform fireLocation;
    private float myTime = 0;

    private void Awake()
    {
        targetter = GetComponentInChildren<Targetter>();
        targetter.setRange(levelData.range);
    }

    public void SetValues()
    {
        targetter.setRange(levelData.range);
        Debug.Log(targetter.getRange());
        Debug.Log(levelData.range);
        projectile.GetComponent<BulletMovement>().damageValue = levelData.damage;
    }

    private void FireBullet()
    {
        GameObject bullet = ObjectPoolerv2.singleton.GetPooledObject();
        bullet.SetActive(true);
        bullet.transform.position = fireLocation.position;
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
            if (myTime > levelData.attackInterval)
            {
                Debug.Log("fire");
                myTime = 0;
                FireBullet();
            }
        }

    }

}
