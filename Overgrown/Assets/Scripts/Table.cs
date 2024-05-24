using System.Collections.Generic;
using UnityEngine;

public class TableInventory : MonoBehaviour
{
    public GameObject OnTable;
    private bool isEmpty = true; //d
    public void AddPlant(GameObject plant)
    {   if (isEmpty)
        {
        OnTable = plant;
        plant.transform.SetParent(transform);
        plant.transform.localPosition = new Vector3(0, 0.5f, 0); // Adjust positions as needed
        isEmpty = false;
        }
        
    }

    public GameObject TakePlant()
    {
        if (OnTable)
        {
            GameObject plant = OnTable;
            plant.transform.SetParent(null);
            isEmpty = true;
            return plant;
        }
        return null;
    }
}
