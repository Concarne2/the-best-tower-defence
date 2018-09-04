using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public GameObject enemy;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start () {
        
        StartCoroutine(Spawn());
		
	}

    IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(enemy);

            yield return new WaitForSeconds(0.5f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
