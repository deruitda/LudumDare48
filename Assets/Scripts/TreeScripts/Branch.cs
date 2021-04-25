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
        private TreeConfig _treeConfig;
        public Branch(TreeConfig treeConfig, BranchBase branchBase, BranchDirection branchDirection)
        {
            _treeConfig = treeConfig;
            _branchBase = branchBase;
            _branchDirection = branchDirection;
        }

        public void grow()
        {
            _growMagnitude++;

            switch (_branchDirection)
            {
                case BranchDirection.LEFT:
                case BranchDirection.RIGHT:    
                    int growDirectionMultiplier = _branchDirection == BranchDirection.RIGHT ? 1 : -1;
                    Spawner.SpawnPrefab(_treeConfig.branchPreFab, _branchBase.posX + (_growMagnitude * growDirectionMultiplier), _branchBase.posY);
                    break;
                case BranchDirection.UP:
                    Spawner.SpawnPrefab(_treeConfig.branchPreFab, _branchBase.posX, _growMagnitude + _branchBase.posY);
                    break;
                case BranchDirection.UP_LEFT:
                case BranchDirection.UP_RIGHT:
                    int growDirectionMultiplierDiag = _branchDirection == BranchDirection.UP_RIGHT ? 1 : -1;
                    Spawner.SpawnPrefab(_treeConfig.branchPreFab, _branchBase.posX + (_growMagnitude * growDirectionMultiplierDiag), _branchBase.posY + _growMagnitude);
                    break;
            }
        }

    }
}
