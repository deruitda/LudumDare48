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
        private GameObject _branchPreFab;
        private BranchDirection _branchDirection;
        private float _growMagnitude = 0;
        private float _height;
        private float _seedXPos;
        public Branch(GameObject branchPreFab, float height, float seedXPos = 0)
        {
            _branchPreFab = branchPreFab;
            _height = height;
            _seedXPos = seedXPos;

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
            Spawner.SpawnPrefab(_branchPreFab, _seedXPos + _growMagnitude * growDirectionMultiplier, _height);
        }

    }
}
