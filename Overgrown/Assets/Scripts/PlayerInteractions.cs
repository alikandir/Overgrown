using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform handTransform; // Where the watering can or plant will be held
    public float interactionRange = 2f; // Range within which the player can interact
    public KeyCode pickUpKey = KeyCode.E; // Key to pick up items, water plants, harvest, and refill
    public KeyCode placeKey = KeyCode.Q; // Key to place items on the table
    public TableInventory tableInventory; // Reference to the table inventory

    public GameObject onHand; // Item currently held by the player
    private bool isHoldingCan = false;

    void Update()
    {
        if (Input.GetKeyDown(pickUpKey))
        {
            if (isHoldingCan)
            {
                TryRefillCan(); // Attempt to refill the watering can if near a water source
                TryWaterPlant(); // Attempt to water a plant
            }
            else if (onHand == null)
            {
                TryPickUpItem(); // Attempt to pick up an item
                TryTakePlantFromTable(); // Attempt to take a plant from the table
                TryHarvest(); // Attempt to harvest a plant
            }
        }

        if (Input.GetKeyDown(placeKey))
        {
            if (onHand != null)
            {
                TryPlaceItemOnTable(); // Attempt to place the item on the table
            }
        }
    }

    void TryPickUpItem()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);
        Debug.Log("Attempting to pick up an item. Found " + colliders.Length + " colliders in range.");
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("WateringCan") || (collider.GetComponent<Plant>() != null && collider.GetComponent<Plant>().isHarvested))
            {
                PickUpItem(collider.gameObject);
                Debug.Log("Picked up item: " + collider.gameObject.name);
                return; // Exit the method once an item is picked up
            }
        }
    }

    void PickUpItem(GameObject item)
    {
        if (onHand)
        {
            DropItem();
        }
        onHand = item;
        onHand.transform.SetParent(handTransform);
        onHand.transform.localPosition = Vector3.zero;
        onHand.transform.localRotation = Quaternion.identity;
        isHoldingCan = item.CompareTag("WateringCan");
        Debug.Log("Is holding can: " + isHoldingCan);
    }

    public void DropItem()
    {
        if (onHand != null)
        {
            onHand.transform.SetParent(null);
            Debug.Log("Dropped item: " + onHand.name);
            onHand = null;
            isHoldingCan = false;
        }
    }

    void TryWaterPlant()
    {
        if (isHoldingCan)
        {
            WateringCan can = onHand.GetComponent<WateringCan>();
            if (can != null && can.UseCan())
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);
                foreach (Collider collider in colliders)
                {
                    Plant plant = collider.GetComponent<Plant>();
                    if (plant != null && !plant.isHarvested)
                    {
                        plant.Water();
                        Debug.Log("Plant has been watered.");
                        return; // Exit the method once a plant is watered
                    }
                }
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

    void TryHarvest()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);
        Debug.Log("Attempting to harvest. Found " + colliders.Length + " colliders in range.");
        foreach (Collider collider in colliders)
        {
            Plant plant = collider.GetComponent<Plant>();
            if (plant != null && plant.ReadyToHarvest())
            {
                plant.Harvest();
                Debug.Log("Plant harvested: " + plant.name);
                PickUpItem(collider.gameObject);
                return; // Exit the method once a plant is harvested
            }
        }
    }

    void TryPlaceItemOnTable()
    {
        if (Vector3.Distance(transform.position, tableInventory.transform.position) <= interactionRange)
        {
            if (onHand.GetComponent<Plant>() != null && onHand.GetComponent<Plant>().isHarvested)
            {
                Debug.Log("Placing plant on table.");
                tableInventory.AddPlant(onHand);
                DropItem();
                return; // Exit the method once a plant is placed on the table
            }
            else if (onHand.CompareTag("WateringCan"))
            {
                Debug.Log("Placing watering can on table.");
                onHand.transform.SetParent(tableInventory.transform);
                onHand.transform.localPosition = new Vector3(0, 0.5f, 0);
                DropItem();
                return; // Exit the method once the watering can is placed on the table
            }
            else
            {
                Debug.Log("Cannot place this item on the table.");
            }
        }
        else
        {
            Debug.Log("Too far from table to place item.");
        }
    }

    void TryTakePlantFromTable()
    {
        if (onHand == null && Vector3.Distance(transform.position, tableInventory.transform.position) <= interactionRange)
        {
            Debug.Log("Taking plant from table.");
            onHand = tableInventory.TakePlant();
            if (onHand != null)
            {
                onHand.transform.SetParent(handTransform);
                onHand.transform.localPosition = Vector3.zero;
                onHand.transform.localRotation = Quaternion.identity;
                Debug.Log("Took plant from table: " + onHand.name);
                return; // Exit the method once a plant is taken from the table
            }
            else
            {
                Debug.Log("No plants on the table to take.");
            }
        }
    }

    void TryRefillCan()
    {
        if (isHoldingCan)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);
            Debug.Log("Attempting to refill watering can. Found " + colliders.Length + " colliders in range.");
            foreach (Collider collider in colliders)
            {
                WaterSource waterSource = collider.GetComponent<WaterSource>();
                if (waterSource != null)
                {
                    Debug.Log("Water source found: " + waterSource.gameObject.name);
                    WateringCan can = onHand.GetComponent<WateringCan>();
                    if (can != null)
                    {
                        can.RefillCan();
                        Debug.Log("Watering can refilled.");
                        return; // Exit the method once the watering can is refilled
                    }
                    else
                    {
                        Debug.Log("No WateringCan component found on held item.");
                    }
                }
            }
            Debug.Log("No water source found in range.");
        }
        else
        {
            Debug.Log("Not holding a watering can.");
        }
    }
}
