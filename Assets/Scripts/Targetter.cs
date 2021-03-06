﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetter : MonoBehaviour {

    public CapsuleCollider rangeCollider;

    private List<Transform> targets;

    public void setRange(float range)
    {
        rangeCollider.radius = range;
    }

    public float getRange()
    {
        return rangeCollider.radius;
    }

    private void Awake()
    {
        targets = new List<Transform>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            targets.Add(other.transform);
           // Debug.Log("Range Enter target #: " + targets.Count);
        }
    }

    public Transform findClosestTarget()
    {
        UpdateTargets();

        if (targets.Count == 0 || transform == null)
            return null;
        else
        {
            Transform closestTarget = targets[0];
            if (closestTarget != null)
            {
                float closestDistance = (closestTarget.position - transform.position).sqrMagnitude;
                for (int i = 0; i < targets.Count; i++)
                {
                    float targetDistance = (targets[i].position - transform.position).sqrMagnitude;
                    if (targetDistance < closestDistance)
                    {
                        closestTarget = targets[i];
                        closestDistance = targetDistance;
                    }
                }
            }
            return closestTarget;
        }
    }


    private void UpdateTargets()
    {
        for(int i=0;i<targets.Count;i++)
            if (!targets[i])
                targets.Remove(targets[i]);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            targets.Remove(other.transform);
           // Debug.Log("Range Exit target #: " + targets.Count);
        }
    }
}
