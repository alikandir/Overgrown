using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public int maxUses = 4; // Maximum number of uses before needing to refill
    private int currentUses;

    void Start()
    {
        currentUses = maxUses; // Start with a full can
    }

    public bool UseCan()
    {
        if (currentUses > 0)
        {
            currentUses--;
            Debug.Log("Watering can used. Remaining uses: " + currentUses);
            return true; // Successfully used the can
        }
        else
        {
            Debug.Log("Watering can is empty. Please refill.");
            return false; // Can is empty
        }
    }

    public void RefillCan()
    {
        currentUses = maxUses;
        Debug.Log("Watering can refilled.");
    }
}
