using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    [SerializeField]
    private GameObject _spriteRepoPrefab;

    public SpriteRepository SpriteRepo { get; private set; }
    public BaseTile CurrentSelectedTile { get; set; }
    public GameObject[,] TerrainTiles { get; set; }
    public GameObject Seed { get; set; }

    public 

    void Start()
    {
        ValidatePrefabs();
        GameObject.Instantiate(_spriteRepoPrefab);
        SpriteRepo = _spriteRepoPrefab.GetComponent<SpriteRepository>();
        // spawn de dirt
        TerrainTiles = Spawner.SpawnDirtTerrain(_dirtPrefabs.ToList(), _dirtWidth, _dirtDepth);

        BuildTileNeighborGraph();

        TerrainTiles[10, 0].GetComponent<BaseTile>().SelectTile();

        // spawn de seed
        Seed = Spawner.SpawnPrefab(_seedPrefab, _seedXPos, _seedYPos);
    }

    // TODO Refactor this method so we can build the neighbor graph during tile instantiation
    private void BuildTileNeighborGraph()
    {
        for (int x = 0; x < _dirtWidth; x++)
        {
            for (int y = 0; y < _dirtDepth; y++)
            {
                var currentTile = TerrainTiles[x, y].GetComponent<BaseTile>();
                currentTile.Neighbors = new Dictionary<NeighborDirections, BaseTile>();

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
