using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plurb : Plant
{
    string plantName = "P'lurp";
    [SerializeField] float growTime = 2f;
    [SerializeField]float overgrownTime = 3f;
    [SerializeField] GameObject product;

    public override void OverGrownEffect()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (ReadyToHarvest)
        {
            Debug.Log("çabuk harvest");
        }
        if (IsOverGrown)
        {
            OverGrownEffect();
        }
    }
}
