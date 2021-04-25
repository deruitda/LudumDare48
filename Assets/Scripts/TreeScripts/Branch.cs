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
        private float _branchBaseHeight;
        private TreeConfig _treeConfig;
        public Branch(TreeConfig treeConfig, float branchBaseHeight)
        {
            _treeConfig = treeConfig;
            _branchBaseHeight = branchBaseHeight;

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
            int growDirectionMultiplier = _branchDirection == BranchDirection.RIGHT ? 1 : -1;
            _growMagnitude++;

            Spawner.SpawnPrefab(_treeConfig.branchPreFab, _treeConfig.seedXPos + (_growMagnitude * growDirectionMultiplier), _branchBaseHeight);
        }

    }
}
