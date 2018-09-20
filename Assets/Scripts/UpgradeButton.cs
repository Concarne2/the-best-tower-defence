using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {

    private Button button;


    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        GameManager.instance.UpgradeCurrentTower();
    }
}
