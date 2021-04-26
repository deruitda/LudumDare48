using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.TreeScripts
{
    enum BranchDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        UP_LEFT,
        UP_RIGHT,
        DOWN_LEFT,
        DOWN_RIGHT
    }

    class Branch
    {
        private BranchDirection _branchDirection;
        private float _growMagnitude = 0;
        private BranchBase _branchBase;
        private BranchBase _branchTip;
        private TreeConfig _treeConfig;
        private TreeFork _treeFork;
        private bool _branchForked = false;
        private int _treeLevel;
        private int _branchGrowth;
        public Branch(TreeConfig treeConfig, BranchBase branchBase, BranchDirection branchDirection, int treeLevel)
        {
            _treeConfig = treeConfig;
            _branchBase = branchBase;
            _branchDirection = branchDirection;
            _branchTip = new BranchBase(branchBase.posX, branchBase.posY);
            _treeLevel = treeLevel;
            _branchGrowth = 0;
        }

        public void grow(bool withLeaves)
        {
            _growMagnitude++;
            if(_branchForked)
            {
                _treeFork.grow(withLeaves);
            }
            else if (_growMagnitude > _treeConfig.minSizeOfBranchBeforeFork)
            {

                int rand = Random.Range(0, 100);
                if (rand <= _treeConfig.chanceOfCreatingAFork * 100)
                {
                    createFork();
                } else
                {
                    growBranch(withLeaves);
                }
            } else {
                growBranch(withLeaves);
            }
        }

        private void growBranch(bool withLeaves)
        {

            //grow our branches

            //X direction
            switch (_branchDirection)
            {
                case BranchDirection.LEFT:
                case BranchDirection.UP_LEFT:
                    _branchTip.subPosX();
                    break;
                case BranchDirection.RIGHT:
                case BranchDirection.UP_RIGHT:
                    _branchTip.addPosX();
                    break;
            }

            //y direction
            switch (_branchDirection)
            {
                case BranchDirection.UP:
                case BranchDirection.UP_RIGHT:
                case BranchDirection.UP_LEFT:
                    _branchTip.addPosY();
                    break;
            }

            Spawner.SpawnPrefab(_treeConfig.branchPreFab, _branchTip.posX, _branchTip.posY);

            _branchGrowth++;

            if(withLeaves)
            {
                growLeaves();
            }

        }

        private void growLeaves()
        {
            BranchBase leafPosition1 = new BranchBase(_branchTip.posX, _branchTip.posY);
            BranchBase leafPosition2 = new BranchBase(_branchTip.posX, _branchTip.posY);
            switch (_branchDirection)
            {
                case BranchDirection.UP:
                case BranchDirection.UP_LEFT:
                case BranchDirection.UP_RIGHT:
                    //left and right
                    leafPosition1.addPosX();
                    leafPosition2.subPosX();
                    break;
                case BranchDirection.LEFT:
                case BranchDirection.RIGHT:
                    leafPosition2.addPosY();
                    leafPosition1.subPosY();
                    break;
            }

            Spawner.SpawnPrefab(_treeConfig.leavesPreFab, leafPosition1.posX, leafPosition1.posY);
            Spawner.SpawnPrefab(_treeConfig.leavesPreFab, leafPosition2.posX, leafPosition2.posY);
        }
        private void createFork()
        {
            _treeFork = new TreeFork(_treeConfig, _branchDirection, _branchTip, _treeLevel + 1);
            _branchForked = true;
        }

    }
}
