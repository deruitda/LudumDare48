using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.TreeScripts
{
    public class Tree
    {
        private List<Branch> _branches;
        private TreeConfig _treeConfig;
        private float _height;
        private TreeFork _treeFork;
        public Tree(TreeConfig treeConfig)
        {
            _treeConfig = treeConfig;
            _branches = new List<Branch>();
        }

        public void grow()
        {
            _height++;

            if(_height == _treeConfig.initialForkSize)
            {
                createFork();
            }
            else if(_height > _treeConfig.initialForkSize)
            {
                _treeFork.grow();
            }
            else
            {
                // build trunk
                Spawner.SpawnPrefab(_treeConfig.treePreFab, _treeConfig.seedXPos, _treeConfig.seedYPos + _height);
            }

            growBranches();

            //int rand = Random.Range(0, 100);
            //if (rand <= _treeConfig.chanceOfCreatingABranch * 100)
            //{
            //    createBranch();
            //}
        }

        private void createFork()
        {

            BranchBase branchBase = new BranchBase(_treeConfig.seedXPos, _height);
            _treeFork = new TreeFork(_treeConfig, BranchDirection.UP, branchBase);
        }

        private void createBranch()
        {
            BranchDirection branchDirection = BranchDirection.RIGHT;
            //Decide direction of branch
            if (Random.Range(0, 2) == 1)
            {
                branchDirection = BranchDirection.LEFT;
            }
            BranchBase branchBase = new BranchBase(_treeConfig.seedXPos, _height);

            _branches.Add(new Branch(_treeConfig, branchBase, branchDirection));
        }

        private void growBranches()
        {
            foreach(var branch in _branches)
            {
                branch.grow();
            }
        }
    }
}
