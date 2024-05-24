using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Plant
{
    [SerializeField] float growtime = 1f;
    [SerializeField] float overgrowntime = 2f;
    [SerializeField] float explosionRange = 3f;


    public Pumpkin()
    {
        plantName = "Rotten Pumpkin";
        growTime = growtime;
        overgrownTime = overgrowntime;

    }

    public override void OverGrownEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.CompareTag("plant"))
                Destroy(colliders[i].gameObject);
        }
    }

    private void Update()
    {
        if (ReadyToHarvest())
        {
            Debug.Log("�abuk harvet kabak");
        }
        if (IsOverGrown())
        {
            OverGrownEffect();
        }
    }
}
