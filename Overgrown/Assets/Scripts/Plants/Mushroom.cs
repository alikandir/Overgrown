using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Plant
{

    [SerializeField] float growtime = 8f;
    [SerializeField] float overgrowntime = 6f;
   
    public Mushroom()
    {
        plantName = "Psychedelic Mushroom";
        growTime = growtime;
        overgrownTime = overgrowntime;
        
    }

    public override void OverGrownEffect()
    {
        return;
    }

    private void Update()
    {
        if (ReadyToHarvest())
        {
            Debug.Log("�abuk harvet mantar");
        }
        if (IsOverGrown())
        {
            OverGrownEffect();
        }
    }
}
