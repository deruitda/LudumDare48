using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Spawner
{
    public static GameObject[,] SpawnDirtTerrain(GameObject prefab, int width, int depth)
    {
        GameObject[,] terrainTiles = new GameObject[width, depth];

        for (int w = 0; w < width; w++)
            for (int d = 0; d < depth; d++)
            {
                var gO = GameObject.Instantiate(prefab);
                gO.transform.position = new Vector2(w, -d);
                terrainTiles[w, d] = gO;
            }

        return terrainTiles;
    }

    public static GameObject SpawnPrefab(GameObject prefab, float xPos, float yPos)
    {
        var gO = GameObject.Instantiate(prefab);
        gO.transform.position = new Vector2(xPos, yPos);
        
        return gO;
    }
}
