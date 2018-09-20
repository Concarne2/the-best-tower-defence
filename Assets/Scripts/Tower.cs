using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public TowerLevel[] levels;

    public int currentLevel=0;
    public TowerLevel currentTowerLevel;

    public float getRange()
    {
        return levels[currentLevel].levelData.range;
    }

    private void Awake()
    {
        currentTowerLevel = Instantiate(levels[currentLevel], transform);
        currentTowerLevel.SetValues();
    }

    public int GetBuildCost()
    {
        return levels[0].levelData.cost;
    }

    public int GetUpgradeCost()
    {
        return levels[currentLevel + 1].levelData.cost;
    }

    public bool IsUpgradable()
    {
        return currentLevel < levels.Length;
    }

    public void UpgradeOneLevel()
    {
        if (currentLevel < levels.Length - 1 && currentLevel >= 0)
        {
            currentLevel++;

            if (currentTowerLevel)
                Destroy(currentTowerLevel.gameObject);

            currentTowerLevel = Instantiate(levels[currentLevel], transform);
            currentTowerLevel.SetValues();
        }
    }

    public void SellTower()
    {
        if (currentTowerLevel)
            GameManager.instance.addMoney(currentTowerLevel.levelData.sell);
        Destroy(gameObject);
    }

    //private void OnMouseDown()
    //{
    //    GameManager.instance.ShowTowerSelected(this);
    //}

}
