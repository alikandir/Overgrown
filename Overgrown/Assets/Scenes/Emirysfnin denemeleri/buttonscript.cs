using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonscript : MonoBehaviour
{
    [SerializeField] Plant[] bitki;
    public void watr()
    {
        foreach (var item in bitki)
        {
            item.Water();
        }
        
    }
}
