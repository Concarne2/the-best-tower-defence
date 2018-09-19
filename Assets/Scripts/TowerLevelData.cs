using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData.asset", menuName = "TowerDefense/Tower Configuration", order = 1)]
public class TowerLevelData : ScriptableObject {

    /// <summary>
    /// A description of the tower for displaying on the UI
    /// </summary>
    public string description;

    /// <summary>
    /// A description of the tower for displaying on the UI
    /// </summary>
    public string upgradeDescription;

    /// <summary>
    /// The cost to build / upgrade to this level
    /// </summary>
    public int cost;

    /// <summary>
    /// The sell value of the tower
    /// </summary>
    public int sell;

    public float damage;

    public float attackInterval;

    public float range;

    /// <summary>
    /// The tower icon
    /// </summary>
    public Sprite icon;


}
