using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Plant
{

    [SerializeField] float growtime = 8f;
    [SerializeField] float overgrowntime = 6f;
    [SerializeField] GameObject prduct;
    public Mushroom()
    {
        plantName = "Psychedelic Mushroom";
        growTime = growtime;
        overgrownTime = overgrowntime;
        product = prduct;
    }

    public override void OverGrownEffect()
    {
        return;
    }

    private void Update()
    {
        if (ReadyToHarvest())
        {
            Debug.Log("çabuk harvet mantar");
        }
        if (IsOverGrown())
        {
            OverGrownEffect();
        }
    }
}
