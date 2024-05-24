using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    
    [SerializeField]PlayerController controller;


    private void OnTriggerEnter(Collider other)
     {  //if (other.CompareTag("table"))
            {
                Debug.Log(other.gameObject.name);
                controller.tableInventory = other.gameObject.GetComponent<TableInventory>();
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == controller.tableInventory)
                controller.tableInventory = null;
    }
    
}
