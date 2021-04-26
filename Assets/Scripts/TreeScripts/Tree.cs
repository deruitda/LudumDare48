using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.TreeScripts
{
    public class Tree
    {
        private TreeConfig _treeConfig;
        private float _height;
        private TreeFork _treeFork;
        private bool _forkCreated = false;
        public Tree(TreeConfig treeConfig)
        {
            _treeConfig = treeConfig;
        }

        public void grow(bool withLeaves = false)
        {
            _height++;
            if(_forkCreated)
            {
                _treeFork.grow(withLeaves);
            }
            else if(_height >= _treeConfig.minSizeOfBranchBeforeFork)
            {
                int rand = Random.Range(0, 100);
                if(rand <= 100 * _treeConfig.chanceOfCreatingAFork)
                {
                   createFork();
                } else
                {
                    buildTrunk();
                }
            }

            else
            {
                // build trunk
                buildTrunk();
            }

        }

        private void buildTrunk()
        {

            Spawner.SpawnPrefab(_treeConfig.treePreFab, _treeConfig.seedXPos, _treeConfig.seedYPos + _height);
        }

        private void createFork()
        {

            BranchBase branchBase = new BranchBase(_treeConfig.seedXPos, _height);
            _treeFork = new TreeFork(_treeConfig, BranchDirection.UP, branchBase);
            _forkCreated = true;
        }


    }
}
