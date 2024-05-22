using System.Collections.Generic;
using UnityEngine;

public class TableInventory : MonoBehaviour
{
    public List<GameObject> plantsOnTable = new List<GameObject>();

    public void AddPlant(GameObject plant)
    {
        plantsOnTable.Add(plant);
        plant.transform.SetParent(transform);
        plant.transform.localPosition = new Vector3(0, 0.5f, plantsOnTable.Count * 0.5f); // Adjust positions as needed
        plant.tag = "PlantOnTable"; // Change tag to indicate it's on the table
    }

    public GameObject TakePlant()
    {
        if (plantsOnTable.Count > 0)
        {
            GameObject plant = plantsOnTable[plantsOnTable.Count - 1];
            plantsOnTable.RemoveAt(plantsOnTable.Count - 1);
            plant.transform.SetParent(null);
            plant.tag = "PlantOnHand"; // Change tag back to indicate it's being held
            return plant;
        }
        return null;
    }
}
