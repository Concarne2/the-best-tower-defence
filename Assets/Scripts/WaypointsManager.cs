using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsManager : MonoBehaviour {

    public static WaypointsManager instance = null;

    private Transform[] waypoints;

    // Use this for initialization
    void Awake()
    {
        if (!instance)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        GetWaypoints();
    }

    private void GetWaypoints()
    {
        //  waypoints = GetComponentsInChildren<Transform>();    this includes the parent

        List<Transform> transforms = new List<Transform>(GetComponentsInChildren<Transform>());
        transforms.Remove(transform);
        waypoints = transforms.ToArray();

        Debug.Log("" + waypoints.Length);

    }

    public bool IsAtFinalWaypoint(int currIndex)
    {
        if (currIndex < waypoints.Length - 1)
            return false;
        else
            return true;
    }

    public Transform GetInitialWaypoint()
    {
        return waypoints[0];
    }

    public Transform GetNextWaypoint(int currIndex)
    {
        if (currIndex >= waypoints.Length - 1)
            return waypoints[waypoints.Length - 1];
        else if (currIndex >= 0)
            return waypoints[currIndex + 1];
        else
            return waypoints[0];
    }


}
