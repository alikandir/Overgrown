using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tulip : Plant
{
    [SerializeField] float growtime = 1f;
    [SerializeField] float overgrowntime = 2f;
    [SerializeField] GameObject prduct;

    Tulip()
    {
        plantName = "Devil's Tulip";
        growTime = growtime;
        overgrownTime = overgrowntime;
        product = prduct;
    }

    public override void OverGrownEffect()
    {
        return;
    }
}
