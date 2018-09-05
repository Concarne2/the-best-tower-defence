using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public GameObject enemy;
    public GameObject chickenTowerGhost;
    public GameObject chickenTower;

    private bool towerCreation = false;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        chickenTowerGhost = Instantiate(chickenTowerGhost, Vector3.zero, Quaternion.identity);
        chickenTowerGhost.SetActive(false);
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

    public void StartTowerCreation()
    {
        towerCreation = true;
        Vector3 position= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
        chickenTowerGhost.transform.position = position;
        chickenTowerGhost.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (towerCreation)
        {
            chickenTowerGhost.transform.position =
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

            if (Input.GetMouseButtonDown(0))
            {
                towerCreation = false;
                Instantiate(chickenTower, chickenTowerGhost.transform.position,chickenTowerGhost.transform.rotation) ;
                chickenTowerGhost.SetActive(false);
            }
        }
	}
}
