using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Plant : MonoBehaviour
{
    public string plantName;
    protected float growTime;
    protected float overgrownTime;
    protected float plantPrice;
    protected float beforeOvergrown;

    private float initialTime;
    private bool isWatered;
    public bool isHarvested { get; private set; } // Flag to check if the plant is harvested
    public List<GameObject> growthStages;
    public GameObject currentStage;
    public virtual bool ReadyToHarvest()
    {
        if (isWatered)
            return Time.time - initialTime >= growTime;
        return false;
    }

    public bool EarlyOvergrown()
    {
        if(isWatered)
            return Time.time - (initialTime + growTime) >= beforeOvergrown;
        return false;
    }

    public bool IsOverGrown()
    {
        if (isWatered && !isHarvested)
            return Time.time - (initialTime + growTime+beforeOvergrown) >= overgrownTime;
        else
            return false;
    }


    public float GetPlantPrice()
    {
        return plantPrice;
    }
    public void Water()
    {
        initialTime = Time.time;
        isWatered = true;
    }

    public bool IsWatered()
    {
        return isWatered;
    }

    public void Harvest()
    {
        isHarvested = true;
    }

    public abstract void OverGrownEffect();
}
