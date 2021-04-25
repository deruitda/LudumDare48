using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.TreeScripts
{
    public class Tree
    {
        private List<Branch> _branches;
        private TreeConfig _treeConfig;
        private float _height;
        public Tree(TreeConfig treeConfig)
        {
            _treeConfig = treeConfig;
            _branches = new List<Branch>();
        }

        public void grow()
        {
            _height++;
            Spawner.SpawnPrefab(_treeConfig.treePreFab, _treeConfig.seedXPos, _treeConfig.seedYPos + _height);

            growBranches();

            int rand = Random.Range(0, 100);
            if (rand <= _treeConfig.chanceOfCreatingABranch * 100)
            {
                createBranch();
            }
        }

        private void createBranch()
        {
            BranchBase branchBase = new BranchBase(_treeConfig.seedXPos, _height);

            _branches.Add(new Branch(_treeConfig, branchBase));
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
