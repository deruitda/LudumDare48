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

        public TreeFork(TreeConfig treeConfig, BranchDirection baseBranchDirection, BranchBase branchBase)
        {
            _treeConfig = treeConfig;
            _baseBranchDirection = baseBranchDirection;
            _branchBase = branchBase;

            createBranches();
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
        private void createBranches()
        {
            List<BranchDirection> possibleBranchDirections = getPossibleBranchDirections();

            int random = UnityEngine.Random.Range(0, possibleBranchDirections.Count());
            BranchDirection firstBranchDirection = possibleBranchDirections[random];
            _branch1 = new Branch(_treeConfig, _branchBase, firstBranchDirection);
            possibleBranchDirections.Remove(firstBranchDirection);

            int random2 = UnityEngine.Random.Range(0, possibleBranchDirections.Count());
            BranchDirection secondBranchDirection = possibleBranchDirections[random2];
            _branch2 = new Branch(_treeConfig, _branchBase, secondBranchDirection);
        }
        public void grow()
        {
            growBranches();
        }
        private void growBranches()
        {
            _branch1.grow();
            _branch2.grow();
        }
    }
}
