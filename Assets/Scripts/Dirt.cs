using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    public int PosX { get; set; }
    public int PosY { get; set; }

    public Composition Composition { get; set; }

    void OnMouseOver()
    {
        Debug.Log("I am dirty");
    }

    void OnMouseExit()
    {
        Debug.Log("Not dirty anymore");
    }
}
