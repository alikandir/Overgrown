using System.Collections.Generic;
using UnityEngine;
public class CustomerManager : MonoBehaviour
{
    public SellPosition[] sellingPositions; // Assign 3 transforms in the inspector for the front positions
    public SellPosition[] waitPositions;
    public Transform queueStartPosition; // Assign the starting position of the queue
    public GameObject customerPrefab; // Assign the customer prefab in the inspector
    private Customer[] customers;
    
    

    void Start()
    {
        // Spawn initial customers (for example, 5 customers)
        for (int i = 0; i <1; i++)
        {
            SpawnCustomer();
        }
    }

    void  SpawnCustomer()
    {
        // Instantiate a new customer and add to the queue
        GameObject newCustomer = Instantiate(customerPrefab, queueStartPosition.position, Quaternion.identity);
        Customer customer=newCustomer.GetComponent<Customer>();
        if (sellingPositions[0].isOccupied==false){
            customer.targetPosition = sellingPositions[0].transform.position;
            sellingPositions[0].isOccupied = true;
        }
        else if (sellingPositions[1].isOccupied==false){
            customer.targetPosition = sellingPositions[1].transform.position;
            sellingPositions[1].isOccupied = true;
        }
        else if (sellingPositions[2].isOccupied==false){
            customer.targetPosition = sellingPositions[2].transform.position;
            sellingPositions[2].isOccupied = true;
        }
        else if (waitPositions[0].isOccupied==false){
            customer.targetPosition = waitPositions[0].transform.position;
            waitPositions[0].isOccupied=true;
        }
        else if (waitPositions[1].isOccupied==false){
            customer.targetPosition = waitPositions[1].transform.position;
            waitPositions[1].isOccupied=true;
        }
        else if (waitPositions[2].isOccupied==false){
            customer.targetPosition = waitPositions[2].transform.position;
            waitPositions[2].isOccupied=true;
        }
    }

    void UpdateCustomerPositions()
    {
       
    }

    public void CompleteOrder()
    {
        return;
    }
}
