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

        BuildTileNeighborGraph();

        CurrentSelectedTile = TerrainTiles[10, 0].GetComponent<BaseTile>();

        // spawn de seed
        Seed = Spawner.SpawnPrefab(_seedPrefab, _seedXPos, _seedYPos);
    }

    private void BuildTileNeighborGraph()
    {
        for (int x = 0; x < _dirtWidth; x++)
        {
            for (int y = 0; y < _dirtDepth; y++)
            {
                var currentTile = TerrainTiles[x, y].GetComponent<BaseTile>();
                currentTile.Neighbors = new Dictionary<NeighborDirections, BaseTile>();

                Debug.Log($"X:{x} Y:{y}");

                if (x > 0)
                    currentTile.Neighbors[NeighborDirections.LEFT] = TerrainTiles[x - 1, y].GetComponent<BaseTile>();

                if (x > 0 && y < _dirtDepth - 1)
                    currentTile.Neighbors[NeighborDirections.LEFT_BOTTOM] = TerrainTiles[x - 1, y + 1].GetComponent<BaseTile>();

                if (y < _dirtDepth - 1)
                    currentTile.Neighbors[NeighborDirections.MIDDLE_BOTTOM] = TerrainTiles[x, y + 1].GetComponent<BaseTile>();

                if (x < _dirtWidth - 1)
                    currentTile.Neighbors[NeighborDirections.RIGHT] = TerrainTiles[x + 1, y].GetComponent<BaseTile>();
                
                if(x < _dirtWidth - 1 && y < _dirtDepth - 1)
                    currentTile.Neighbors[NeighborDirections.RIGHT_BOTTOM] = TerrainTiles[x + 1, y + 1].GetComponent<BaseTile>();
            }
        }
    }

    private void ValidatePrefabs()
    {
        if (_dirtPrefabs.Any(p => p.GetComponent<BaseTile>() == null))
            throw new MissingComponentException("Prefab is missing BaseTile component");
    }
}
