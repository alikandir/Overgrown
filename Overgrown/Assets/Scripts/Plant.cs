using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Plant 
{
    private string plantName;
    private float growTime;
    private float overgrownTime;
    private GameObject product;

    private float initialTime;
    bool isWatered;

    public Plant(string name, float gTime, float ogTime, GameObject pr)
    {
        plantName = name;
        growTime = gTime;
        overgrownTime = ogTime;
        product = pr;
        isWatered = false;
    }

    
    public bool ReadyToHarvest
    {
        get
        {
            if (isWatered)
                return Time.time-initialTime >= growTime;
            return false;
        }
    }

    public bool IsOverGrown
    {
        get
        {   if (isWatered)
                return Time.time - initialTime >= overgrownTime;
            else return false;
        }
    }

    public GameObject getProduct() 
    { 
        if (ReadyToHarvest)
            return product;
        else 
            return null;
    }

    public void Water()
    {
        initialTime = Time.time;
        isWatered = true;
    }



}
