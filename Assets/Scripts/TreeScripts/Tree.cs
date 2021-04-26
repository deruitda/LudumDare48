using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.TreeScripts
{
    public class Tree
    {
        private TreeConfig _treeConfig;
        private float _treeGrowth;
        private TreeFork _treeFork;
        private List<Branch> _growingBranches;
        private bool _forkCreated = false;
        public Tree(TreeConfig treeConfig)
        {
            _treeConfig = treeConfig;
            _growingBranches = new List<Branch>();
        }

        public void grow(bool withLeaves = false)
        {
            _treeGrowth++;
            growLength(withLeaves);
        }

        private void growLength(bool withLeaves)
        {
            if (_forkCreated)
            {
                for(var i = _growingBranches.Count - 1; i >= 0 ; i--)
                {
                    Branch growingBranch = _growingBranches[i];
                    growingBranch.growBranch(withLeaves);
                    if(_growingBranches.Count < _treeConfig.maxAmountOfForks && growingBranch.needsToCreateTreeFork())
                    {
                        TreeFork newTreeFork = growingBranch.createFork();
                        _growingBranches.RemoveAt(i);
                        _growingBranches.AddRange(newTreeFork.getBranches());
                    }
                }

            }
            else if (_treeGrowth >= _treeConfig.minSizeOfBranchBeforeFork)
            {
                int rand = Random.Range(0, 100);
                if (rand <= 100 * _treeConfig.chanceOfCreatingAFork)
                {
                    createFork();
                }
                else
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

            Spawner.SpawnPrefab(_treeConfig.treePreFab, _treeConfig.seedXPos, _treeConfig.seedYPos + _treeGrowth);
        }

        private void createFork()
        {
            BranchBase branchBase = new BranchBase(_treeConfig.seedXPos, _treeGrowth);
            _treeFork = new TreeFork(_treeConfig, BranchDirection.UP, branchBase, 1);
            _growingBranches.AddRange(_treeFork.getBranches());
            _forkCreated = true;
        }


    }
}
