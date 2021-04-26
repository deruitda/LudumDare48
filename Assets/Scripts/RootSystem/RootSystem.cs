using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: This should eventually be a data structure representing the root system
public class RootSystem : MonoBehaviour
{
    public BaseTile DeepestTile { get; private set; }

    public void UpdateDeepestRootTile(BaseTile tile)
    {
        if (DeepestTile == null || tile.PosY > DeepestTile.PosY)
        {
            DeepestTile = tile;
        }
    }
}
