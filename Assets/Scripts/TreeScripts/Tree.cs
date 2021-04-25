using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.TreeScripts
{
    public class Tree
    {
        private GameObject _treePreFab;
        private GameObject _branchPreFab;

        private float _seedXPos;
        private float _seedYPos;

        private List<Branch> _branches;
        private float _height;
        public Tree(GameObject treePreFab, GameObject branchPreFab, float seedXPos = 0, float seedYPos = 0)
        {
            _treePreFab = treePreFab;
            _branchPreFab = branchPreFab;
            _seedXPos = seedXPos;
            _seedYPos = seedYPos;

            _branches = new List<Branch>();
        }

        public void grow()
        {
            _height++;
            Spawner.SpawnPrefab(_treePreFab, _seedXPos, _seedYPos + _height);

            growBranches();

            int rand = Random.Range(0, 100);
            if (rand <= 50)
            {
                createBranch();
            }
        }

        private void createBranch()
        {
            _branches.Add(new Branch(_branchPreFab, _height, _seedXPos));
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
