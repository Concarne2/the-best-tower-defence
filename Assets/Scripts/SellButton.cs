using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellButton : MonoBehaviour {

    private Button button;


    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        GameManager.instance.SellCurrentTower();
    }
}
