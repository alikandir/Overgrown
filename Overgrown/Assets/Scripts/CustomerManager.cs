using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public Transform[] sellingPositions; // Assign 3 transforms in the inspector for the front positions
    public Transform queueStartPosition; // Assign the starting position of the queue
    public GameObject customerPrefab; // Assign the customer prefab in the inspector

    private Queue<GameObject> customerQueue = new Queue<GameObject>();

    void Start()
    {
        // Spawn initial customers (for example, 5 customers)
        for (int i = 0; i < 5; i++)
        {
            SpawnCustomer();
        }
    }

    void SpawnCustomer()
    {
        // Instantiate a new customer and add to the queue
        GameObject newCustomer = Instantiate(customerPrefab, queueStartPosition.position, Quaternion.identity);
        customerQueue.Enqueue(newCustomer);

        // Update queue positions
        UpdateCustomerPositions();
    }

    void UpdateCustomerPositions()
    {
        int index = 0;
        foreach (GameObject customer in customerQueue)
        {
            Customer customerScript = customer.GetComponent<Customer>();
            if (index < sellingPositions.Length)
            {
                customerScript.targetPosition = sellingPositions[index].position;
                if (index == 0) // Display order for the first customer in line
                {
                    //customerScript.DisplayOrder();
                    
                }
            }
            else
            {
                // Position customers in the queue behind the selling positions
                customerScript.targetPosition = queueStartPosition.position + new Vector3(0, 0, -2 * (index - sellingPositions.Length + 1));
            }
            index++;
        }
    }

    public void CompleteOrder()
    {
        if (customerQueue.Count > 0)
        {
            // Remove the first customer in the queue
            GameObject completedCustomer = customerQueue.Dequeue();
            Customer customerScript = completedCustomer.GetComponent<Customer>();
            //customerScript.CompleteOrder();

            // Update the positions of the remaining customers
            UpdateCustomerPositions();
        }
    }
}
