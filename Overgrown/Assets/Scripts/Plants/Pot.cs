using System.Collections.Generic;
using UnityEngine;

public class Pot:MonoBehaviour
{
    public Plant plantInPot=null;

    public Plant pumpkinPrefab;
    public Plant mushroomPrefab;
    public Plant plurbPrefab;
    
    public void PlantSeed(Seed seed){
        if(plantInPot!=null){return;}
        switch (seed.seedType){
            case SeedType.Pumpkin:
            plantInPot=Instantiate(pumpkinPrefab);
            plantInPot.transform.SetParent(transform);
            plantInPot.transform.localPosition = new Vector3(0, 0.5f, 0);
            break;
            case SeedType.Mushroom:
            plantInPot=Instantiate(mushroomPrefab);
            plantInPot.transform.SetParent(transform);
            plantInPot.transform.localPosition = new Vector3(0, 0.5f, 0);
            break;
            case SeedType.Plurb:
            plantInPot=Instantiate(plurbPrefab);
            plantInPot.transform.SetParent(transform);
            plantInPot.transform.localPosition = new Vector3(0, 0.5f, 0);
            break;
        }
        Destroy(seed.gameObject);
    }
    public GameObject Harvest(){
        if (plantInPot.ReadyToHarvest()){
            Debug.Log("returning"+plantInPot.gameObject.name);
            plantInPot.Harvest();
            return plantInPot.gameObject;
        }
        else {
            Debug.Log("notready");
            return null;}

    }
}