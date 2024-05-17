using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Plant : MonoBehaviour
{
    
    protected string plantName;
    protected float growTime;
    protected float overgrownTime;
    protected GameObject product;

    private float initialTime;
    bool isWatered;

    

    
    public virtual bool ReadyToHarvest()
    {
            if (isWatered)
                return Time.time- initialTime >= growTime;
            return false;
    }

    public bool IsOverGrown()
    {   if (isWatered)
                return Time.time - (initialTime + growTime) >= overgrownTime;
            else return false;
    }

    public GameObject getProduct() 
    { 
        if (ReadyToHarvest())
            return product;
        else 
            return null;
    }

    public void Water()
    {
        initialTime = Time.time;
        isWatered = true;
    }

    public abstract void OverGrownEffect();


}
