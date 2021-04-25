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
        public Branch(TreeConfig treeConfig, BranchBase branchBase, BranchDirection branchDirection)
        {
            _treeConfig = treeConfig;
            _branchBase = branchBase;
            _branchDirection = branchDirection;
            _branchTip = new BranchBase(branchBase.posX, branchBase.posY);
        }

        public void grow()
        {
            _growMagnitude++;
            if(_branchForked)
            {
                _treeFork.grow();
            }
            else if (_growMagnitude > _treeConfig.minSizeOfBranchBeforeFork)
            {

                int rand = Random.Range(0, 100);
                if (rand <= _treeConfig.chanceOfCreatingAFork * 100)
                {
                    createFork();
                } else
                {
                    growBranch();
                }
            } else {
                growBranch();
            }
        }

        private void growBranch()
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
        }
        private void createFork()
        {
            _treeFork = new TreeFork(_treeConfig, _branchDirection, _branchTip);
            _branchForked = true;
        }

    }
}
