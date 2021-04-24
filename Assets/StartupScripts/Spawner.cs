using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Spawner
{
    public static void SpawnDirtTerrain(GameObject prefab, int width, int depth)
    {
        int maxRight = width / 2; // TODO what if this is an odd number?
        int maxLeft = -maxRight;

        for (int w = maxLeft; w < maxRight; w++)
            for (int d = 0; d > -depth; d--)
                GameObject.Instantiate(prefab).transform.position = new Vector2(w, d);
    }

    public static void SpawnPrefab(GameObject prefab, float xPos, float yPos)
    {
        GameObject.Instantiate(prefab).transform.position = new Vector2(xPos, yPos);
    }
}
