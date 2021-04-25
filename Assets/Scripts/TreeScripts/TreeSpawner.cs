using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TreeScripts
{
    public class TreeSpawner
    {
        private int _nutrientScore;
        private Tree _tree;
        public TreeSpawner(TreeConfig treeConfig)
        {

            _tree = new Tree(treeConfig);
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
}
