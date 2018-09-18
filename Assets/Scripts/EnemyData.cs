using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData.asset", menuName = "TowerDefense/Enemy Configuration", order = 2)]
public class EnemyData : ScriptableObject {

    public float health;

    public int foodBounty;

    public int moneyBounty;

    public float moveSpeed;

}
