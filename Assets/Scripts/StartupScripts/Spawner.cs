using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Spawner
{
    public static GameObject[,] SpawnDirtTerrain(List<GameObject> prefabs, int width, int depth)
    {
        GameObject[,] terrainTiles = new GameObject[width, depth];

        for (int w = 0; w < width; w++)
            for (int d = 0; d < depth; d++)
            {
                var prefab = GetRandomDirtTerrainPrefab(prefabs);
                var gO = GameObject.Instantiate(prefab);
                gO.transform.position = new Vector2(w, -d);
                var tile = gO.GetComponent<BaseTile>();
                tile.PosX = w;
                tile.PosY = d;
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

    private static GameObject GetRandomDirtTerrainPrefab(List<GameObject> gameObjects)
    {
        int rand = Random.Range(0, gameObjects.Count);

        return gameObjects[rand];
    }
}
