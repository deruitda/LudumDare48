using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject _dirtPrefab;
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

    private GameObject[,] _terrainTiles;
    private GameObject _seed;

    void Start()
    {
        // spawn de dirt
        _terrainTiles = Spawner.SpawnDirtTerrain(_dirtPrefab, _dirtWidth, _dirtDepth);

        // spawn de seed
        _seed = Spawner.SpawnPrefab(_seedPrefab, _seedXPos, _seedYPos);
    }
}
