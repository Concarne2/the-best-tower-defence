using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    

    public TowerLevel[] levels;

    public int currentLevel=0;

    public float getRange()
    {
        Debug.Log(currentLevel);
        return levels[currentLevel].levelData.range;
    }


    //private void OnMouseDown()
    //{
    //    GameManager.instance.ShowTowerSelected(this);
    //}

}
