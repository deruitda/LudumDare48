using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Spawner
{
    private static int IterationsUntilWater = 250;
    private static int IterationsSinceWater = 0;
    public static GameObject[,] SpawnDirtTerrain(List<GameObject> prefabs, int width, int depth)
    {
        GameObject[,] terrainTiles = new GameObject[width, depth];
        List<TerrainPrefabProbability> prefabProbabilities = new List<TerrainPrefabProbability>();

        foreach (var prefab in prefabs)
        {
            var tileInfo = prefab.GetComponent<BaseTile>();
            prefabProbabilities.Add(new TerrainPrefabProbability
            {
                Depth = tileInfo.MinDepthToSpawn,
                Probability = tileInfo.ProbabilityToSpawn,
                TerrainPrefab = prefab,
            });
        }

        for (int w = 0; w < width; w++)
            for (int d = 0; d < depth; d++)
            {
                var prefab = GetRandomTerrainPrefab(prefabProbabilities, d);
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

    private static GameObject GetRandomTerrainPrefab(List<TerrainPrefabProbability> terrainPrefabProbabilities, int depth)
    {
        if(IterationsSinceWater >= IterationsUntilWater && depth > 7)
        {
            IterationsSinceWater = 0;
            return terrainPrefabProbabilities.FirstOrDefault(t => t.TerrainPrefab.GetComponent<BaseTile>() is WaterTile).TerrainPrefab;
        }

        List<TerrainPrefabProbability> filteredTerrainProbability = terrainPrefabProbabilities.Where(t => t.Depth <= depth).ToList();

        // cdf = cumulative density function: https://stackoverflow.com/questions/4463561/weighted-random-selection-from-array
        int cdf = filteredTerrainProbability.Sum(fs => fs.Probability);

        List<int> list = new List<int>();
        for (int i = 0; i < 100; i++)
        {
            int rand = UnityEngine.Random.Range(0, cdf);
            foreach (var terrainPrefabProbability in filteredTerrainProbability)
            {
                if (rand <= terrainPrefabProbability.Probability)
                {
                    IterationsSinceWater++;
                    return terrainPrefabProbability.TerrainPrefab;
                }

                rand -= terrainPrefabProbability.Probability;
            }
        }

        throw new Exception("Unable to select random prefab");
    }

    private class TerrainPrefabProbability
    {
        public int Depth { get; set; }
        public int Probability { get; set; }
        public GameObject TerrainPrefab { get; set; }
    }
}
