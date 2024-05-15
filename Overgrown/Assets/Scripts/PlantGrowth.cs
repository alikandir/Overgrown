using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public Vector3 maxScale = new Vector3(2f, 2f, 2f); // Maximum size of the plant
    public float growthSpeed = 0.4f; // Growth speed multiplier
    private bool isWatered = false; // Flag to check if the plant is watered
    private bool isFullyGrown = false; // Flag to check if the plant is fully grown

    private Vector3 initialScale; // Initial scale of the plant

    void Start()
    {
        initialScale = transform.localScale; // Store the initial scale of the plant
    }

    void Update()
    {
        // Check if the plant is watered and not fully grown
        if (isWatered && !isFullyGrown)
        {
            GrowPlant(); // Call the GrowPlant method to grow the plant
        }
    }

    public void WaterPlant()
    {
        isWatered = true; // Set the isWatered flag to true
        Debug.Log("Plant is being watered!"); // Display the message in the console
    }

    void GrowPlant()
    {
        // Gradually scale the plant up towards the maximum scale
        transform.localScale = Vector3.Lerp(transform.localScale, maxScale, growthSpeed * Time.deltaTime);

        // Check if the plant has reached its maximum size
        if (Vector3.Distance(transform.localScale, maxScale) < 0.01f)
        {
            transform.localScale = maxScale; // Ensure the plant scale is exactly the max scale
            isFullyGrown = true; // Set the isFullyGrown flag to true
            isWatered = false; // Reset the isWatered flag
            Debug.Log("Plant has fully grown!"); // Display the message in the console
        }
    }
}




