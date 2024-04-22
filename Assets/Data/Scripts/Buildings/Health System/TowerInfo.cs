using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfo : MonoBehaviour
{

    public List<BuildingHealth> towers;

    public Text towerCountText;

    public int towerCount;
    public int totalTower;
    // Start is called before the first frame update
    void Start()
    {
        towerCount = towers.Count;
        totalTower = towerCount;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in towers)
        {
            if (item.Die() && !item.IsCountedAsDead) // Assuming there's a property IsCountedAsDead in BuildingHealth
            {
                towerCount--;
                item.IsCountedAsDead = true;
            }
        }

        UpdateTowerCount();
    }

    void UpdateTowerCount()
    {
        towerCountText.text = "Towers: " + towerCount.ToString()+"/"+totalTower; 
    }
}
