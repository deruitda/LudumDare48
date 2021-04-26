using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Spawner
{
    public static GameObject[,] SpawnDirtTerrain(List<GameObject> prefabs, int width, int depth)
    {
        GameObject[,] terrainTiles = new GameObject[width, depth];
        Dictionary<int, GameObject> prefabsByDepth = new Dictionary<int, GameObject>();

        foreach(var prefab in prefabs)
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

        int i = 0;
        foreach(var gO in gameObjects)
        {
            if (gO.Key <= depth)
                filteredObjects.Add(i++, gO.Value);
        }

        int rand = Random.Range(0, filteredObjects.Count);
        try
        {
            return filteredObjects[rand];
        }
        catch (System.Exception ex)
        {

            throw ex;
        }
    }
}
