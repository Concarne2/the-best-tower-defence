using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public LayerMask towerLayer;
    public GameObject enemy;
    public GameObject chickenTowerGhost;
    private ChickenGhost ghost;
    public GameObject chickenTower;
    public GameObject radiusVisualizer;
    public float startingFood;
    public float goalFood;
    public int startingMoney;
    public float foodDecreasePerSecond;

    private float foodSmoother=10;

    private GraphicRaycaster gr;
    private PointerEventData ped;
    public float actualCurrentFood;
    public float showingCurrentFood;
    private int currentMoney;

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
        ped = new PointerEventData(null);
        actualCurrentFood = startingFood;
        showingCurrentFood = startingFood;
        currentMoney = startingMoney;
    }

    private void Start()
    {
        gr = UIManager.instance.towerSelectUI.GetComponent<GraphicRaycaster>();
        StartCoroutine(Spawn());
        UIManager.instance.SetFoodValue(actualCurrentFood);
        UIManager.instance.SetMaxFood(goalFood);
        UIManager.instance.SetMoneyValue(startingMoney);
    }

    public void addFood(float food)
    {
        actualCurrentFood += food;
    }

    public void addMoney(int money)
    {
        currentMoney += money;
    }

    public void ShowTowerSelected(Tower selectedTower)
    {
        radiusVisualizer.transform.position = selectedTower.transform.position;
        radiusVisualizer.transform.localScale=new Vector3(selectedTower.range*4,selectedTower.range*4,1);
        radiusVisualizer.SetActive(true);
        UIManager.instance.ShowTowerSelectUI();
    }

    private void ClickOnTower()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, towerLayer))
        {
            Tower tower = hit.transform.GetComponent<Tower>();
            if (tower != null)
            {
                ShowTowerSelected(tower);
                towerCreation = false;
                chickenTowerGhost.SetActive(false);
                clickTower = true;
            }
        }
        else
        {
            clickTower = false;
            radiusVisualizer.SetActive(false);

            UIManager.instance.HideTowerSelectUI();
        }
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
	
    public void GoToVictoryScreen()
    {
        Debug.Log("victory");
    }

	// Update is called once per frame
	void Update () {

        if (actualCurrentFood >= goalFood)
        {
            showingCurrentFood = goalFood;
            UIManager.instance.SetFoodValue(showingCurrentFood);
            GoToVictoryScreen();
            return;
        }

        actualCurrentFood -= foodDecreasePerSecond * Time.deltaTime;
        showingCurrentFood = Mathf.Lerp(showingCurrentFood, actualCurrentFood, Time.deltaTime * foodSmoother);
        UIManager.instance.SetFoodValue(showingCurrentFood);
        UIManager.instance.SetMoneyValue(currentMoney);

        if(Input.GetMouseButtonDown(0))
        {
            ped.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            gr.Raycast(ped, raycastResults);
            if (raycastResults.Count > 0)
                return;
        }
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
                else
                {
                    ClickOnTower();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            ClickOnTower();
        }
	}
}
