using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTile : MonoBehaviour
{
    public int PosX { get; set; }
    public int PosY { get; set; }

    public abstract Sprite SelecetedSprite { get; }
    public abstract Sprite DefaultSprite { get; }
    public abstract SpriteRenderer SpriteRenderer { get; set; }

    public abstract ISoilComposition SoilComposition { get; set; }

    public BaseTile(int x, int y)
    {
        PosX = x;
        PosY = y;
    }

    void OnMouseOver()
    {
        Debug.Log("I am dirty");
    }

    void OnMouseExit()
    {
        Debug.Log("Not dirty anymore");
    }
}
