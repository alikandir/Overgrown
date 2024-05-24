using UnityEngine;

public class Customer : MonoBehaviour
{
    public GameObject orderBubble; // Assign in the inspector, this will be the order bubble prefab
    public Vector3 targetPosition; // The position where the customer should move
    public float speed = 2f; // Movement speed

    private bool isOrderCompleted = false;
    private CurrencyManager currencyManager;
    private 

    void Update()
    {
        // Move towards the target position
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    public void DisplayOrder()
    {
        // Instantiate the order bubble above the customer
        GameObject bubble = Instantiate(orderBubble, transform.position + Vector3.up * 2, Quaternion.identity);
        bubble.transform.SetParent(this.transform); // Attach the bubble to the customer
    }

    public void CompleteOrder()
    {
        isOrderCompleted = true;
        // Implement any additional logic for order completion
        // E.g., play animation, sound, etc.
        Destroy(gameObject); // Remove customer from the scene
    }
    public void BuyPlant(Plant plant)
    {
        currencyManager.EarnMoney(plant.GetPlantPrice());
        Destroy(plant);
        Debug.Log($"Sold a plant for ${plant.GetPlantPrice()}!");
    }
}


    


