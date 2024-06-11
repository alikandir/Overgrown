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
    bool [] onlyOnce = {true,true,true,true};
    public Plurb()
    {
        plantName = "P'lurp";
        growTime = growtime;
        overgrownTime = overgrowntime;
        beforeOvergrown = beforeovergrown;
    }
    private void Start()
    {
        currentStage = Instantiate(growthStages[0], transform.position, transform.rotation, transform);
        
    }
    public override void OverGrownEffect()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (IsOverGrown())
        {   if (onlyOnce[3])
            {
                Debug.Log("bura bozuk");
                OverGrownEffect();
                onlyOnce[3] = false;
            }
        }
        else if (EarlyOvergrown())
        {
            if (onlyOnce[2])
            {
                Destroy(currentStage);
                currentStage = Instantiate(growthStages[2], transform.position, transform.rotation, transform);
                
                onlyOnce[2] = false;
            }
        }
        else if (ReadyToHarvest())
        {
            if (onlyOnce[1])
            {
                Destroy(currentStage);
                currentStage = Instantiate(growthStages[1], transform.position, transform.rotation, transform);
                
                onlyOnce[1] = false;
            }
        }
        else if (IsWatered())
        {
            if (onlyOnce[0])
            {
                Destroy(currentStage);
                currentStage = Instantiate(growthStages[0], transform.position, transform.rotation, transform);
                onlyOnce[0]=false;
            }
            
        }
    }
    
}

