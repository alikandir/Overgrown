using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TableInventory : MonoBehaviour
{   
    public GameObject wateringCanPrefab;
    public GameObject SeedPrefab;
    public GameObject OnTable;
    public enum DeskType 
    {
        EmptyDesk,
        SeedDesk,
        WateringCanDesk,
        SellingDesk

    }
    public DeskType deskType; 

    private void Start() {
        switch (deskType){
            case DeskType.SeedDesk:
            AddObject(Instantiate(SeedPrefab));
            break;
            case DeskType.WateringCanDesk:
            AddObject(Instantiate(wateringCanPrefab));
            break;
            case DeskType.EmptyDesk:
            break;

        }
    }
    public void AddObject(GameObject plant)
    {   if (OnTable==null)
        {
        OnTable = plant;
   

        plant.transform.SetParent(transform);
        plant.transform.localPosition = new Vector3(-4f, 0, 0); // Adjust positions as needed
        Debug.Log($"{gameObject.name} added {plant.name} to the table.");
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} already has an item on the table: {OnTable.name}");
        }
        
    }

    public GameObject TakeObject()
    {
        if (OnTable)
        {   
            if (deskType==DeskType.SeedDesk){
                return Instantiate(OnTable);
            }
            else{
                return OnTable;
            }
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} has no item to take.");
            return null;
        }
        
    }
}
