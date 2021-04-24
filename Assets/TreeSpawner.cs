using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner
{
    private GameObject _treePreFab;

    private int _nutrientScore;
    private float _seedXPos;
    private float _seedYPos;

    public TreeSpawner(GameObject treePreFab, float seedXPos = 0, float seedYPos = 0)
    {
        this._treePreFab = treePreFab;
        this._seedXPos = seedXPos;
        this._seedYPos = seedYPos;
    }

    public void SpawnTree(int nutrientScore)
    {

        int seedYPos = (int)Math.Floor(_seedYPos);
        int i = 0;
        while (i < nutrientScore)
        {
            Spawner.SpawnPrefab(_treePreFab, _seedXPos, seedYPos + i);
            i++;
        }
    }


}
