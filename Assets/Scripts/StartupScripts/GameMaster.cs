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
    private GameObject _seedPrefab;
    [SerializeField]
    private int _dirtWidth;
    [SerializeField]
    private int _dirtDepth;
    [SerializeField]
    private float _seedXPos;
    [SerializeField]
    private float _seedYPos;
    public BaseTile CurrentSelectedTile { get; set; }
    public GameObject[,] TerrainTiles { get; set; }
    public GameObject Seed { get; set; }

    void Start()
    {
        ValidatePrefabs();

        // spawn de dirt
        TerrainTiles = Spawner.SpawnDirtTerrain(_dirtPrefabs.ToList(), _dirtWidth, _dirtDepth);

        CurrentSelectedTile = TerrainTiles[10, 0].GetComponent<BaseTile>();

        // spawn de seed
        Seed = Spawner.SpawnPrefab(_seedPrefab, _seedXPos, _seedYPos);
    }

    private void ValidatePrefabs()
    {
        if(_dirtPrefabs.Any(p => p.GetComponent<BaseTile>() == null))
            throw new MissingComponentException("Prefab is missing BaseTile component");
    }
}
