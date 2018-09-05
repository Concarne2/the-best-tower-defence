using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public float attackInterval;

    private Targetter targetter;
    private Transform target;

    public Transform fireLocation;
    private float myTime = 0;
    

    private void Awake()
    {
        targetter = GetComponentInChildren<Targetter>();
    }

    private void FireBullet()
    {
        GameObject bullet = ObjectPooler.instance.GetPooledObject();
        bullet.transform.position = fireLocation.position;
        BulletMovement movement = bullet.GetComponent<BulletMovement>();
        movement.setTarget(target);
        bullet.SetActive(true);
    }

    void Update () {

        myTime += Time.deltaTime;

        target = targetter.findClosestTarget();
        if (target)
        {
            if (myTime > attackInterval)
            {
               // Debug.Log("fire");
                myTime = 0;
                FireBullet();
            }
        }
		
	}
}
