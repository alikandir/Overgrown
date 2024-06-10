using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plurb : Plant
{
    [SerializeField] float growtime = 2f;
    [SerializeField] float overgrowntime = 10f;
    MeshFilter filter;
    [SerializeField] float beforeovergrown=10;
    [SerializeField] Mesh [] meshs;

    public Plurb()
    {
        plantName = "P'lurp";
        growTime = growtime;
        overgrownTime = overgrowntime;
        beforeOvergrown = beforeovergrown;
    }
    private void Start()
    {

        filter = gameObject.GetComponent<MeshFilter>();
        filter.mesh = meshs[0];
    }
    public override void OverGrownEffect()
    {
        Destroy(gameObject);
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
        else if(IsWatered())
        {
            filter.mesh = meshs[1];
        }
    }
}
