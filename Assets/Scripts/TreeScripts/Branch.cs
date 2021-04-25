using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.TreeScripts
{
    enum BranchDirection
    {
        LEFT,
        RIGHT
    }

    class Branch
    {
        private BranchDirection _branchDirection;
        private float _growMagnitude = 0;
        private BranchBase _branchBase;
        private TreeConfig _treeConfig;
        public Branch(TreeConfig treeConfig, BranchBase branchBase)
        {
            _treeConfig = treeConfig;
            _branchBase = branchBase;

            //Decide direction of branch
            if (Random.Range(0, 2) == 1)
            {
                _branchDirection = BranchDirection.LEFT;
            } else
            {
                _branchDirection = BranchDirection.RIGHT;
            }
        }

        public void grow()
        {
            int rand = Random.Range(0, 100);
            if(rand <= _treeConfig.chanceOfBranchGrowing * 100)
            {

                int growDirectionMultiplier = _branchDirection == BranchDirection.RIGHT ? 1 : -1;
                _growMagnitude++;

                Spawner.SpawnPrefab(_treeConfig.branchPreFab, _branchBase.posX  + (_growMagnitude * growDirectionMultiplier), _branchBase.posY);
            }

        }

    }
}
