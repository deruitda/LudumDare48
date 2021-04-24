using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject _dirtPrefab;
    [SerializeField]
    private int _dirtWidth;
    [SerializeField]
    private int _dirtHeight;
    // Start is called before the first frame update
    void Start()
    {
        // spawn de dirt
        Spawner.SpawnRectangle(_dirtPrefab, _dirtWidth, _dirtHeight);
    }
}
