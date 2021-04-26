using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.TreeScripts
{
    class TreeFork
    {
        private TreeConfig _treeConfig;
        private BranchDirection _baseBranchDirection;
        private BranchBase _branchBase;

        private Branch _branch1;
        private Branch _branch2;

        public TreeFork(TreeConfig treeConfig, BranchDirection baseBranchDirection, BranchBase branchBase, int treeLevel)
        {
            _treeConfig = treeConfig;
            _baseBranchDirection = baseBranchDirection;
            _branchBase = branchBase;

            createBranches(treeLevel);
        }

        private List<BranchDirection> getPossibleBranchDirections()
        {
            List<BranchDirection> possibleBranchDirections = new List<BranchDirection>();
            possibleBranchDirections.Add(_baseBranchDirection);
            switch(_baseBranchDirection)
            {
                case BranchDirection.UP:
                    possibleBranchDirections.Add(BranchDirection.UP_LEFT);
                    possibleBranchDirections.Add(BranchDirection.UP_RIGHT);
                    break;
                case BranchDirection.UP_LEFT:
                    possibleBranchDirections.Add(BranchDirection.UP);
                    possibleBranchDirections.Add(BranchDirection.LEFT);
                    break;
                case BranchDirection.UP_RIGHT:
                    possibleBranchDirections.Add(BranchDirection.UP);
                    possibleBranchDirections.Add(BranchDirection.RIGHT);
                    break;
                case BranchDirection.RIGHT:
                    possibleBranchDirections.Add(BranchDirection.UP_RIGHT);
                    break;
                case BranchDirection.LEFT:
                    possibleBranchDirections.Add(BranchDirection.UP_LEFT);
                    break;
            }

            return possibleBranchDirections;
        }
        private void createBranches(int treeLevel)
        {
            List<BranchDirection> possibleBranchDirections = getPossibleBranchDirections();

            int random = UnityEngine.Random.Range(0, possibleBranchDirections.Count());
            BranchDirection firstBranchDirection = possibleBranchDirections[random];
            _branch1 = new Branch(_treeConfig, _branchBase, firstBranchDirection, treeLevel);
            possibleBranchDirections.Remove(firstBranchDirection);

            int random2 = UnityEngine.Random.Range(0, possibleBranchDirections.Count());
            BranchDirection secondBranchDirection = possibleBranchDirections[random2];
            _branch2 = new Branch(_treeConfig, _branchBase, secondBranchDirection, treeLevel);
        }
        public void grow(bool withLeaves)
        {
            growBranches(withLeaves);
        }
        private void growBranches(bool withLeaves)
        {
            _branch1.grow(withLeaves);
            _branch2.grow(withLeaves);
        }

        public List<Branch> getBranches()
        {
            List<Branch> branches = new List<Branch>();
            branches.Add(_branch1);
            branches.Add(_branch2);
            return branches;
        }
    }
}
