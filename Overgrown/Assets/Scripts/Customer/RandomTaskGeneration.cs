using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomTaskGeneration : MonoBehaviour
{
    [SerializeField] Plant[] plants;
    Plant task;
    TextMeshProUGUI textMeshProUGUI;
    int money = 0;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (task)
        {
            textMeshProUGUI.text = "Money: " + money + "\n" + task.plantName ;
        }
        else
        {
            NewTask();
        }

    }
    public bool TaskMatch(Plant p)
    {
        return p.plantName.Equals(task.plantName);
    }

    public void NewTask()
    {
        int i= Random.Range(0, plants.Length);
        task = plants[i];
    }

    public void AddMoney(int i)
    {
        money += i;
    }
}
