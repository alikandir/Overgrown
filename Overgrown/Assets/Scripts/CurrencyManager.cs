using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public int money = 0;
    public TMP_Text moneyText;

    void Start()
    {
        UpdateMoneyUI();
    }

    public void EarnMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    void UpdateMoneyUI()
    {
        moneyText.text = $"Money: ${money}";
    }
}
