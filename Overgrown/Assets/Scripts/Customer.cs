using UnityEngine;

public class Customer : MonoBehaviour
{
    public CurrencyManager currencyManager;
    public float interactionRange = 2f;
    public int plantPrice = 10; // Price for each plant

    void Update()
    {
        if (Vector3.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position) <= interactionRange)
        {
            TryBuyPlantFromPlayer();
        }
    }

    void TryBuyPlantFromPlayer()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player.onHand != null && player.onHand.CompareTag("PlantOnHand"))
        {
            BuyPlant(player.onHand);
            player.DropItem();
        }
    }

    public void BuyPlant(GameObject plant)
    {
        currencyManager.EarnMoney(plantPrice);
        Destroy(plant);
        Debug.Log($"Sold a plant for ${plantPrice}!");
    }
}

