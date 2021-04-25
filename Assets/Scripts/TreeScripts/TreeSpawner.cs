using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TreeSpawner
{
    private int _nutrientScore;
    private Assets.Scripts.TreeScripts.Tree _tree;

    public TreeSpawner(GameObject treePreFab, float seedXPos = 0, float seedYPos = 0)
    {

        _tree = new Assets.Scripts.TreeScripts.Tree(treePreFab, seedXPos, seedYPos);
    }

    public void SpawnTree(int nutrientScore)
    {

        int i = 0;
        while (i < nutrientScore)
        {
            _tree.grow();
            i++;
        }
    }


}
