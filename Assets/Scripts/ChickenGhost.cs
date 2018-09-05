using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGhost : MonoBehaviour {

    private Rigidbody rb;

	void Awake () {
        rb = GetComponent<Rigidbody>();
	}

    public void Move(Vector3 pos)
    {
        transform.position = pos;
    }

}
