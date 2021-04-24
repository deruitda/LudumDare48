using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _dirtPrefabs;
    [SerializeField]
    private GameObject _treePreFab;
    [SerializeField]
    private GameObject _seedPrefab;
    [SerializeField]
    private int _dirtWidth;
    [SerializeField]
    private int _dirtDepth;
    [SerializeField]
    private float _seedXPos;
    [SerializeField]
    private float _seedYPos;

    public GameObject[,] TerrainTiles { get; set; }
    public GameObject[,] TreeTiles { get; set; }
    public GameObject Seed { get; set; }

    void Start()
    {
        ValidatePrefabs();

        // spawn de dirt
        TerrainTiles = Spawner.SpawnDirtTerrain(_dirtPrefabs.ToList(), _dirtWidth, _dirtDepth);

        // spawn de seed
        Seed = Spawner.SpawnPrefab(_seedPrefab, _seedXPos, _seedYPos);

        spawnTree(15);
    }

    private void ValidatePrefabs()
    {
        if(_dirtPrefabs.Any(p => p.GetComponent<BaseTile>() == null))
            throw new MissingComponentException("Prefab is missing BaseTile component");
    }

    private void spawnTree(int nutrientScore)
    {
        int seedYPos = (int) Math.Floor(_seedYPos);
        int i = 0;
        while(i < nutrientScore)
        {
            Spawner.SpawnPrefab(_treePreFab, _seedXPos, seedYPos + i);
            i++;
        }
    }
}
