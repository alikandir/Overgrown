using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public float money = 0;
    public TMP_Text moneyText;

    void Start()
    {
        UpdateMoneyUI();
    }

    public void EarnMoney(float amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    void UpdateMoneyUI()
    {
        moneyText.text = $"Money: ${money}";
    }
}
