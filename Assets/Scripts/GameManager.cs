using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public GameObject enemy;
    public GameObject chickenTowerGhost;
    private ChickenGhost ghost;
    public GameObject chickenTower;
    public GameObject radiusVisualizer;

    private bool towerCreation = false;
    public bool clickTower = false;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        chickenTowerGhost = Instantiate(chickenTowerGhost, Vector3.zero, Quaternion.identity);
        ghost = chickenTowerGhost.GetComponent<ChickenGhost>();
        chickenTowerGhost.SetActive(false);
        radiusVisualizer.SetActive(false);
    }

    public void ShowTowerSelected(Tower selectedTower)
    {
        radiusVisualizer.transform.position = selectedTower.transform.position;
        radiusVisualizer.transform.localScale=new Vector3(selectedTower.range*4,selectedTower.range*4,1);
        radiusVisualizer.SetActive(true);
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
        ghost.SetPositionFromMouse();
        chickenTowerGhost.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (towerCreation)
        {
            ghost.SetPositionFromMouse();

            if (Input.GetMouseButtonDown(0))
            {
                if (!ghost.cantPlace)
                {
                    towerCreation = false;
                    Instantiate(chickenTower, chickenTowerGhost.transform.position, chickenTowerGhost.transform.rotation);
                    chickenTowerGhost.SetActive(false);
                }
            }
        }
	}
}
