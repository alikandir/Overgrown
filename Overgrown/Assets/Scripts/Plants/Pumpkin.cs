using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Plant
{
    [SerializeField] float growtime = 5f;
    [SerializeField] float overgrowntime = 4f;
    [SerializeField] float beforeovergrown = 12f;
    [SerializeField] float explosionRange = 3f;
    [SerializeField] Mesh [] meshs;
    MeshFilter filter;

    public Pumpkin()
    {
        plantName = "Rotten Pumpkin";
        growTime = growtime;
        overgrownTime = overgrowntime;
        beforeOvergrown = beforeovergrown;
    }
    private void Start()
    {
        filter = GetComponent<MeshFilter>();
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
        if (IsOverGrown())
        {
            Debug.Log("bura bozuk");
            OverGrownEffect();

        }
        else if (EarlyOvergrown())
        {
            filter.mesh = meshs[3];
            Debug.Log("early");
        }
        else if (ReadyToHarvest())
        {
            filter.mesh = meshs[2];
            Debug.Log("Harvest");
        }
        else if (IsWatered())
        {
            filter.mesh = meshs[1];
        }
    }
}
