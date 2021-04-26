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
            int multiplier = 6;
            double maxTreeGrowth = 100;
            double theoreticalMaxScore = _treeConfig.startingWater;
            double actualScore = (nutrientScore * maxTreeGrowth) / theoreticalMaxScore;
            // only want 2 ^ 7 end branches
            _treeConfig.minSizeOfBranchBeforeFork = actualScore / multiplier;
            _treeConfig.minSizeOfTreeBeforeForking = actualScore / multiplier;
            _treeConfig.thicknessOfLeaves = (int)actualScore / multiplier;
            double i = 0;
            while (i < actualScore)
            {
                double percentageOfTreeCreated = i == 0 ?  0 : (double)(i / actualScore);

                bool withLeaves = percentageOfTreeCreated >= (1 - _treeConfig.percentageOfTreeHasLeaves);
                _tree.grow(withLeaves);
                i++;
            }

        }
    }
}
