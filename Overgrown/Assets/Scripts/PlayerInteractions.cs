using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform handTransform; // Where the watering can or plant will be held
    public float interactionRange = 2f; // Range within which the player can interact
    public KeyCode pickUpKey = KeyCode.E; // Key to pick up the watering can or plant
    public KeyCode interactionKey = KeyCode.F; // Key to interact with the plant
    public KeyCode harvestKey = KeyCode.Q; // Key to harvest the plant
    public KeyCode placePlantKey = KeyCode.R; // Key to place the plant on the table
    public KeyCode sellKey = KeyCode.S; // Key to sell the plant to a customer

    public TableInventory tableInventory; // Reference to the table inventory

    public GameObject onHand; // Make this public so it can be accessed from the Customer script
    private bool isHoldingCan = false;

    void Update()
    {
        if (Input.GetKeyDown(pickUpKey))
        {
            if (isHoldingCan)
            {
                DropItem();
            }
            else
            {
                TryPickUpCan();
            }
        }

        if (isHoldingCan && Input.GetKeyDown(interactionKey))
        {
            TryWaterPlant();
        }

        if (Input.GetKeyDown(harvestKey))
        {
            TryHarvest();
        }

        if (Input.GetKeyDown(placePlantKey))
        {
            if (onHand != null )
            {
                TryPlacePlantOnTable();
            }
        }

        if (Input.GetKeyDown(pickUpKey))
        {
            TryTakePlantFromTable();
        }

        if (Input.GetKeyDown(sellKey))
        {
            TrySellPlantToCustomer();
        }
    }

    void TryPickUpCan()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("WateringCan"))
            {
                PickUpItem(collider.gameObject);
                break;
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
    }

    public void DropItem() // Ensure this is public
    {
        if (onHand != null)
        {
            onHand.transform.SetParent(null);
            onHand = null;
            isHoldingCan = false;
        }
    }

    void TryWaterPlant()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("plant"))
            {
                Plant plant = collider.GetComponent<Plant>();
                if (plant != null)
                {
                    plant.Water();
                    Debug.Log("Plant has been watered.");
                }
            }
        }
    }

    void TryHarvest()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("plant"))
            {
                Plant plant = collider.GetComponent<Plant>();
                if (plant != null && plant.ReadyToHarvest())
                {
                    plant.Harvest();
                    Debug.Log("Plant harvested");
                    PickUpItem(collider.gameObject);
                    break;
                }
            }
        }
    }

    void TryPlacePlantOnTable()
    {
        if (Vector3.Distance(transform.position, tableInventory.transform.position) <= interactionRange)
        {
            Debug.Log("Placing plant on table.");
            tableInventory.AddPlant(onHand);
            onHand = null;
        }
        else
        {
            Debug.Log("Too far from table to place plant.");
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
            }
        }
        else
        {
            Debug.Log("Too far from table to take plant.");
        }
    }

    void TrySellPlantToCustomer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Customer"))
            {
                Customer customer = collider.GetComponent<Customer>();
                if (customer != null && onHand != null && onHand.GetComponent<Plant>().isHarvested)
                {
                    //customer.BuyPlant(onHand);
                    DropItem();
                }
                break;
            }
        }
    }
}
