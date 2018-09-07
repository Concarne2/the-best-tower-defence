using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// The cost to upgrade to this level
    /// </summary>
    public int cost;

    /// <summary>
    /// The sell cost of the tower
    /// </summary>
    public int sell;

    public float damage;

    /// <summary>
    /// The tower icon
    /// </summary>
    public Sprite icon;


}
