using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance = null;

    public Image towerSelectUI;
    public Slider foodBar;
    public Text moneyText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        towerSelectUI.gameObject.SetActive(false);
    }

    public void ShowTowerSelectUI()
    {

        towerSelectUI.gameObject.SetActive(true);
    }

    public void HideTowerSelectUI()
    {

        towerSelectUI.gameObject.SetActive(false);
    }

    public void SetMaxFood(float food)
    {
        foodBar.maxValue = food;
    }

    public void SetFoodValue(float food)
    {
        foodBar.value = food;
    }

    public void SetMoneyValue(int money)
    {
        moneyText.text = "Money: " + money;
    }

}
