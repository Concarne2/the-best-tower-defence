using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGhost : MonoBehaviour {

    public Material cantPlaceMaterial;
    private Material ogMaterial;

    private Renderer rend;

    private Rigidbody rb;
    [HideInInspector]
    public bool cantPlace;


    private void OnEnable()
    {
        
    }

    public void SetPositionFromMouse()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        transform.position = position;
    }

    void Awake () {
        rend = GetComponent<Renderer>();
        ogMaterial = rend.material;
        rb = GetComponent<Rigidbody>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChickenTower"))
        {
            rend.material = cantPlaceMaterial;
            cantPlace = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ChickenTower"))
        {
            rend.material = ogMaterial;
            cantPlace = false;
        }
    }

    public void Move(Vector3 pos)
    {
        transform.position = pos;
    }

}
