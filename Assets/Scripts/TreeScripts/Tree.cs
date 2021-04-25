using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.TreeScripts
{
    public class Tree
    {
        private double _chanceOfCreatingABranch = 0.1;

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
            if (rand <= _chanceOfCreatingABranch * 100)
            {
                createBranch();
            }
        }

        private void createBranch()
        {
            _branches.Add(new Branch(_treeConfig, _height));
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
