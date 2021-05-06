using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: This should eventually be a data structure representing the root system
public class RootSystem : MonoBehaviour
{
    public BaseTile DeepestTile { get; private set; }
    private List<BaseTile> _roots;

    public RootSystem()
    {
        _roots = new List<BaseTile>();
    }

    public List<BaseTile> GetRoots()
    {
        return _roots;
    }

    public void UpdateDeepestRootTile(BaseTile tile)
    {
        if (DeepestTile == null || tile.PosY > DeepestTile.PosY)
        {
            DeepestTile = tile;
        }
    }

    public void AddRootToRootSystem(BaseTile tile)
    {
        _roots.Add(tile);
    }
}
