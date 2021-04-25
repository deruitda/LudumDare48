using UnityEngine;

namespace Assets.Scripts.TreeScripts
{
    public class Tree
    {
        private GameObject _treePreFab;
        private float _seedXPos;
        private float _seedYPos;
        private float height;
        public Tree(GameObject treePreFab, float seedXPos = 0, float seedYPos = 0)
        {
            _treePreFab = treePreFab;
            _seedXPos = seedXPos;
            _seedYPos = seedYPos;
        }

        public void grow()
        {
            height++;
            Spawner.SpawnPrefab(_treePreFab, _seedXPos, _seedYPos + height);
        }
    }
}
