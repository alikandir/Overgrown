using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform handTransform; // Where the watering can or plant will be held
    public float interactionRange = 2f; // Range within which the player can interact
    public KeyCode pickUpKey = KeyCode.E; // Key to pick up items, water plants, harvest, and refill
    public KeyCode placeKey = KeyCode.Q; // Key to place items on the table
    public TableInventory tableInventory; // Reference to the table inventory

    public GameObject onHand; // Item currently held by the player
    public Collider objectInFront=null;
    private bool isHoldingCan = false;

    private CapsuleCollider capsuleCollider;
    [SerializeField] RandomTaskGeneration taskMan;
    private void Start() {
        capsuleCollider=GetComponent<CapsuleCollider>();
        
    }
    void Update()
    {
        HandleInput();
    }
    private void HandleInput(){
        if (objectInFront==null){return;}
        if (Input.GetKeyDown(pickUpKey))
        {
            switch (objectInFront.tag)
            {
                case "Table":
                TryTakeItemFromTable(objectInFront.GetComponent<TableInventory>());
                break;
                case "Pot":
                Pot pot=objectInFront.GetComponent<Pot>();
                if (onHand==null){
                    Debug.Log("tryinggg");
                    PickUpItem(pot.Harvest());
                    break;
                }
                else if (onHand.tag=="WateringCan"){
                    TryWaterPlant(pot.plantInPot);
                    break;
                }
                break;
                
                
                case "WaterSource":
                WaterSource waterSource=objectInFront.GetComponent<WaterSource>();
                TryRefillCan(waterSource);
                break;
            } 
        }

        else if (Input.GetKeyDown(placeKey))
        {
            if (objectInFront.tag=="Table"){
                TryPlaceItemOnTable(objectInFront.GetComponent<TableInventory>());
            }
            else if (objectInFront.tag=="Pot"){
                Pot pot=objectInFront.GetComponent<Pot>();
                TryPlanting(pot);
            }


        }
    }
    private void OnTriggerEnter(Collider other) {
        objectInFront = other;
        
    }

    private void OnTriggerExit(Collider other) {
        objectInFront=null;
    }
    

    void PickUpItem(GameObject item)
    {
        if (onHand)
        {
            return;
        }
        onHand = item;
        onHand.transform.SetParent(handTransform);
        onHand.transform.localPosition = Vector3.zero;
        onHand.transform.localRotation = Quaternion.identity;
        isHoldingCan = item.CompareTag("WateringCan");
    }

    public void DropItem()
    {
        if (onHand != null)
        {
            onHand.transform.SetParent(null);
            onHand = null;
            isHoldingCan = false;
        }
    }

    void TryWaterPlant(Plant plant)
    {
        if (isHoldingCan)
        {
            if (plant != null && !plant.isHarvested)
                {
                    plant.Water();
                    Debug.Log("watered");
                    return; // Exit the method once a plant is watered
                }
                
            
            else
            {
                Debug.Log("Cannot water plant. Watering can is empty or not found.");
            }
        }
        else
        {
            Debug.Log("Not holding a watering can.");
        }
    }


    void TryPlaceItemOnTable(TableInventory table)
    {
        if (onHand && table.OnTable==null)
        {   
            if(onHand.GetComponent<Plant>() && taskMan.TaskMatch(onHand.GetComponent<Plant>()))
            {
                taskMan.NewTask();
                Destroy(onHand);
                onHand=null;
                taskMan.AddMoney(2);
                return;
            }
                table.AddObject(onHand);
                
                DropItem();
                if (isHoldingCan){
                    isHoldingCan=false;
                }
                return; // Exit the method once a plant is placed on the table
        }
            
        
        
    }

    void TryTakeItemFromTable(TableInventory table)
    {

        if (onHand == null)
        {
            onHand = table.TakeObject();
            if (table.deskType!=TableInventory.DeskType.SeedDesk){
                table.OnTable = null;
            }
            
            if (onHand.GetComponent<WateringCan>() != null){
                isHoldingCan=true;
            }
            if (onHand != null)
            {
            Vector3 itemGlobalScale = onHand.transform.lossyScale;
            
            // Set the parent to the handTransform
            onHand.transform.SetParent(handTransform);
            
            // Reset the local position
            onHand.transform.localPosition = Vector3.zero;
            
            // Calculate and apply the new local scale to maintain the global scale
            Vector3 newLocalScale = itemGlobalScale.DivideBy(handTransform.lossyScale);
            onHand.transform.localScale = newLocalScale;
                return; // Exit the method once a plant is taken from the table
            }
   
        }
    }

    void TryRefillCan(WaterSource waterSource)
    {
       
        if (waterSource != null)
        {
            WateringCan can = onHand.GetComponent<WateringCan>();
            if (can != null)
            {
                can.RefillCan();
                return; // Exit the method once the watering can is refilled
            }
            
        }
    }

    public void TryPlanting(Pot pot){
        if (onHand == null){return;}
        if (onHand.GetComponent<Seed>()==null){return;}
        Seed seed= onHand.GetComponent<Seed>();
        pot.PlantSeed(seed);
        onHand=null;
    }
            
}
// Extension method to divide one Vector3 by another
public static class Vector3Extensions
{
    public static Vector3 DivideBy(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }
}