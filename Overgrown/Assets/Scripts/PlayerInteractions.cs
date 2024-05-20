using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform handTransform; // Where the watering can will be held
    public float interactionRange = 2f; // Range within which the player can interact
    public KeyCode pickUpKey = KeyCode.E; // Key to pick up the watering can
    public KeyCode interactionKey = KeyCode.F; // Key to water the plant
    public KeyCode harvestKey = KeyCode.Q;

    private GameObject onHand;
    private bool isHoldingCan = false;

    void Update()
    {
        if (Input.GetKeyDown(pickUpKey))
        {
            if (isHoldingCan)
            {
                DropCan();
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

        if(Input.GetKeyDown(harvestKey))
        {
            TryHarvest();
        }
    }

    void TryPickUpCan()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("WateringCan"))
            {
                PickUpCan(collider.gameObject);
                break;
            }
        }
    }

    void PickUpCan(GameObject can)
    {   if(onHand) 
            DropCan();
        onHand = can;
        onHand.transform.SetParent(handTransform);
        onHand.transform.localPosition = Vector3.zero;
        onHand.transform.localRotation = Quaternion.identity;
        isHoldingCan = true;
    }

    void DropCan()
    {
        onHand.transform.SetParent(null);
        onHand = null;
        isHoldingCan = false;
    }

    void TryWaterPlant()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("plant")) 
            {/*
                PlantGrowth plant = collider.GetComponentInChildren<PlantGrowth>();
                if (plant != null)
                {
                    plant.WaterPlant();
                    Debug.Log("Plant has been watered.");
                }
                break;*/

                Plant plant = collider.GetComponent<Plant>();
                plant.Water();
                Debug.Log("Plant has been watered.");
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
                if (collider.GetComponent<Plant>().ReadyToHarvest())
                {
                    Debug.Log("abi ettik harvest");
                PickUpCan(collider.gameObject);
                break;
                }

            }
        }
    }
}



