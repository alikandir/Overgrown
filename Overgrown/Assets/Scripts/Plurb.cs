using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plurb : Plant
{
    string plantName = "P'lurp";
    [SerializeField] float growtime = 2f;
    [SerializeField]float overgrowntime = 3f;
    [SerializeField] GameObject prduct;

    public Plurb()
    {
        plantName = "P'lurp";
        growTime = growtime;
        overgrownTime = overgrowntime;
        product = prduct; // You may want to set this to a specific GameObject
    }
    public override void OverGrownEffect()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (ReadyToHarvest())
        {
            Debug.Log("çabuk harvet");
        }
        if (IsOverGrown())
        {
            OverGrownEffect();
        }
    }
}
