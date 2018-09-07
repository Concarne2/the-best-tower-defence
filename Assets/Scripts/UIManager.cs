using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance = null;

    public Image towerSelectUI;

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

    // Update is called once per frame
    void Update () {
		
	}
}
