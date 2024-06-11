using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Plant
{
    [SerializeField] float growtime = 5f;
    [SerializeField] float overgrowntime = 4f;
    [SerializeField] float beforeovergrown = 12f;
    [SerializeField] float explosionRange = 3f;
    
    public Pumpkin()
    {
        plantName = "Rotten Pumpkin";
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
            Destroy(currentStage);
            currentStage = Instantiate(growthStages[2], transform.position, transform.rotation, transform);
            Debug.Log("early");
        }
        else if (ReadyToHarvest())
        {   
            Destroy(currentStage);
            currentStage = Instantiate(growthStages[1], transform.position, transform.rotation, transform);
            Debug.Log("Harvest");
        }
        else if (IsWatered())
        {
            Destroy(currentStage);
            currentStage = Instantiate(growthStages[0], transform.position, transform.rotation, transform);
        }
    }
}
