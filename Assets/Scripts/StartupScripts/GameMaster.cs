using Assets.Scripts.TreeScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _dirtPrefabs;
    [SerializeField]
    private GameObject _treePreFab;
    [SerializeField]
    private GameObject _leavePreFab;
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
    [SerializeField]
    private Slider _waterSlider;
    [SerializeField]
    private Text _waterText;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _gameOverSubtext;
    [SerializeField]
    private int StartingWater;

    private int WaterGained = 0;
    public bool IsGameOver { get; set; }
    public int WaterRemaining { get; private set; }
    public int NutritionScore { get; private set; }
    public SpriteRepository SpriteRepo { get; private set; }
    public BaseTile CurrentSelectedTile { get; set; }
    public GameObject[,] TerrainTiles { get; set; }
    public GameObject[,] TreeTiles { get; set; }
    public GameObject Seed { get; set; }
    public RootSystem RootSystem { get; private set; }

    public int UpdateWaterRemaining(int amount)
    {
        WaterRemaining += amount;
        _waterSlider.value += amount;
        _waterText.text = WaterRemaining.ToString();
        WaterGained += amount;

        if (WaterRemaining <= 0)
            GameOver();

        Debug.Log($"Water level now at: {WaterRemaining}");

        return WaterRemaining;
    }

    public void GameOver()
    {
        IsGameOver = true;
        TreeConfig treeConfig = new TreeConfig
        {
            treePreFab = _treePreFab,
            branchPreFab = _treePreFab,
            leavesPreFab = _leavePreFab,
            seedXPos = _seedXPos,
            seedYPos = _seedYPos,
            chanceOfCreatingABranch = 0.1,
            chanceOfBranchGrowing = 0.5,
            minSizeOfTreeBeforeForking = 15,
            minSizeOfBranchBeforeFork = 12,
            chanceOfCreatingAFork = 0.4,
            percentageOfTreeHasLeaves = 0.3,
            startingWater = StartingWater + WaterGained,
            maxAmountOfForks = 1000
        };
        Destroy(Seed);
        TreeSpawner ts = new TreeSpawner(treeConfig);
        ts.SpawnTree(NutritionScore);

        StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        _gameOverSubtext.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5);

        _gameOverSubtext.gameObject.SetActive(false);
        _gameOverText.gameObject.SetActive(false);
    }

    public int UpdateNutrientScore(int amount)
    {
        NutritionScore += amount;
        _scoreText.text = NutritionScore.ToString();
        Debug.Log($"Nutrition Score: {NutritionScore}");

        return NutritionScore;
    }

    void Start()
    {
        _waterSlider.maxValue = StartingWater;
        _waterText.text = StartingWater.ToString();
        UpdateWaterRemaining(StartingWater);

        RootSystem = new RootSystem();

        GameObject.Instantiate(_spriteRepoPrefab);

        // spawn de dirt
        ValidatePrefabs();
        SpriteRepo = _spriteRepoPrefab.GetComponent<SpriteRepository>();
        TerrainTiles = Spawner.SpawnDirtTerrain(_dirtPrefabs.ToList(), _dirtWidth, _dirtDepth);

        BuildTileNeighborGraph();

        TerrainTiles[(_dirtWidth / 2), 0].GetComponent<BaseTile>().SelectTile(true);

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
