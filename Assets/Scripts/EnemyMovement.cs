using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public int speed;
    public float closeEnough;

    private Transform startPos;
    private Transform nextWaypoint;
    private int currWaypointIdx;
    private Rigidbody rb;
    private bool reachedWaypoint;


	void Awake ()
    {
        startPos = GameObject.FindGameObjectWithTag("Start").transform;
        initialSetup();
    }

    private void initialSetup()
    {
        currWaypointIdx = 0;
        reachedWaypoint = false;
        if (startPos)
            gameObject.transform.position = startPos.position;
        rb = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        nextWaypoint = WaypointsManager.instance.GetInitialWaypoint();
        StartCoroutine( MoveTowardsPosition(nextWaypoint.position));
        //MoveToEndpoint();
    }

    IEnumerator MoveTowardsPosition(Vector3 pos)
    {
        float sqrRemainingDistance = (transform.position - pos).sqrMagnitude;

        while (sqrRemainingDistance > closeEnough)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb.position, pos, speed * Time.deltaTime);
            rb.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - pos).sqrMagnitude;
            yield return null;
        }
        if (WaypointsManager.instance.IsAtFinalWaypoint(currWaypointIdx)) 
            ReachedEnd();
        nextWaypoint = WaypointsManager.instance.GetNextWaypoint(currWaypointIdx);
        currWaypointIdx++;
        reachedWaypoint = true;
    }

    private void ReachedEnd()
    {
        Destroy(gameObject);
    }

    private void MoveToEndpoint()
    {
        while (!WaypointsManager.instance.IsAtFinalWaypoint(currWaypointIdx))
        {
            //Debug.Log("" + currWaypointIdx);
            StartCoroutine(MoveTowardsPosition(nextWaypoint.position));
            nextWaypoint = WaypointsManager.instance.GetNextWaypoint(currWaypointIdx);
            currWaypointIdx++;
        }
    }

    private void Update()
    {
        if (reachedWaypoint)
        {
            StartCoroutine(MoveTowardsPosition(nextWaypoint.position));
            reachedWaypoint = false;
        }
    }
}
