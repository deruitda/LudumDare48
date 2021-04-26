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
        private TreeConfig _treeConfig;
        public TreeSpawner(TreeConfig treeConfig)
        {

            _tree = new Tree(treeConfig);
            _treeConfig = treeConfig;
        }

        public void SpawnTree(int nutrientScore)
        {

            double i = 0;
            while (i < nutrientScore)
            {
                double percentageOfTreeCreated = i == 0 ?  0 : (double)(i / nutrientScore);

                bool withLeaves = percentageOfTreeCreated >= (1 - _treeConfig.percentageOfTreeHasLeaves);
                _tree.grow(withLeaves);
                i++;
            }
        }
    }
}
