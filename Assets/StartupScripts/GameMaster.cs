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

    private const float SEED_START_X_POS = 0F;
    private const float SEED_START_Y_POS = 0.5F;

    void Start()
    {
        // spawn de dirt
        Spawner.SpawnDirtTerrain(_dirtPrefab, _dirtWidth, _dirtDepth);

        // spawn de seed
        Spawner.SpawnPrefab(_seedPrefab, SEED_START_X_POS, SEED_START_Y_POS);
    }
}
