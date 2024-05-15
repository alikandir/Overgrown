using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform handTransform; // Where the watering can will be held
    public float interactionRange = 2f; // Range within which the player can interact
    public KeyCode pickUpKey = KeyCode.E; // Key to pick up the watering can
    public KeyCode waterKey = KeyCode.F; // Key to water the plant

    private GameObject wateringCan;
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

        if (isHoldingCan && Input.GetKeyDown(waterKey))
        {
            TryWaterPlant();
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
    {
        wateringCan = can;
        wateringCan.GetComponent<Rigidbody>().isKinematic = true;
        wateringCan.transform.SetParent(handTransform);
        wateringCan.transform.localPosition = Vector3.zero;
        wateringCan.transform.localRotation = Quaternion.identity;
        isHoldingCan = true;
    }

    void DropCan()
    {
        wateringCan.GetComponent<Rigidbody>().isKinematic = false;
        wateringCan.transform.SetParent(null);
        wateringCan = null;
        isHoldingCan = false;
    }

    void TryWaterPlant()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("PlantPot"))
            {
                PlantGrowth plant = collider.GetComponentInChildren<PlantGrowth>();
                if (plant != null)
                {
                    plant.WaterPlant();
                    Debug.Log("Plant has been watered.");
                }
                break;
            }
        }
    }
}



