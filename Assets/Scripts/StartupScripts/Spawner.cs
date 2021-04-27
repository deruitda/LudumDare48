using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Spawner
{
    private static int IterationsUntilWater = 100;
    private static int IterationsSinceWater = 0;
    private static int IterationsUntilPhos = 20;
    private static int IterationsSincePhos = 0;
    public static GameObject[,] SpawnDirtTerrain(List<GameObject> prefabs, int width, int depth)
    {
        GameObject[,] terrainTiles = new GameObject[width, depth];
        Dictionary<int, GameObject> prefabsByDepth = new Dictionary<int, GameObject>();

        foreach (var prefab in prefabs)
        {
            var minDepth = prefab.GetComponent<BaseTile>().MinDepthToSpawn;
            prefabsByDepth.Add(minDepth, prefab);
        }

        for (int w = 0; w < width; w++)
            for (int d = 0; d < depth; d++)
            {
                var prefab = GetRandomDirtTerrainPrefab(prefabsByDepth, d);
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

    // TODO this method should be refactored when we have time
    private static GameObject GetRandomDirtTerrainPrefab(Dictionary<int, GameObject> gameObjects, int depth)
    {
        Dictionary<int, GameObject> filteredObjects = new Dictionary<int, GameObject>();

        if (IterationsSinceWater >= IterationsUntilWater)
        {
            IterationsSinceWater = 0;
            IterationsUntilWater = Random.Range(100, 250);
            return gameObjects[-1];
        }

        if (IterationsSincePhos >= IterationsUntilPhos)
        {
            IterationsSincePhos = 0;
            IterationsUntilPhos = Random.Range(100, 150);
            return gameObjects[10];
        }

        int i = 0;
        foreach (var gO in gameObjects)
        {
            if (gO.Key != -1 && gO.Key != 10 && gO.Key <= depth)
                filteredObjects.Add(i++, gO.Value);
        }

        int rand = Random.Range(0, filteredObjects.Count);
        IterationsSinceWater++;
        IterationsSincePhos++;
        return filteredObjects[rand];
    }
}
