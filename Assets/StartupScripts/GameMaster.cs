using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject _dirtPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // spawn de dirt
        Spawner.SpawnRectangle(_dirtPrefab, 10, 20);
    }
}
