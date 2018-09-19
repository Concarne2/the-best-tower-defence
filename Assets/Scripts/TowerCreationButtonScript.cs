using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerCreationButtonScript : MonoBehaviour {

    private Button button;

    public GameObject towerGhostPrefab;
    public GameObject towerToBuildPrefab;

    private GameObject ghost;

	void Awake () {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);

        ghost = Instantiate(towerGhostPrefab, Vector3.zero, Quaternion.identity);
        ghost.SetActive(false);
    }

    void TaskOnClick()
    {
        GameManager.instance.StartTowerCreation(ghost, towerToBuildPrefab);
    }
}
