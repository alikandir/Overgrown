using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tulip : Plant
{
    [SerializeField] float growtime = 1f;
    [SerializeField] float overgrowntime = 2f;

    Tulip()
    {
        plantName = "Devil's Tulip";
        growTime = growtime;
        overgrownTime = overgrowntime;
    }

    public override void OverGrownEffect()
    {
        return;
    }
}
