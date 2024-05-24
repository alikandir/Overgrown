using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plurb : Plant
{
    [SerializeField] float growtime = 2f;
    [SerializeField]float overgrowntime = 20f;
    

    public Plurb()
    {
        plantName = "P'lurp";
        growTime = growtime;
        overgrownTime = overgrowntime;
    }
    public override void OverGrownEffect()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (ReadyToHarvest())
        {
            //Debug.Log("�abuk harvet");
        }
        if (IsOverGrown())
        {
            OverGrownEffect();
        }
    }
}
