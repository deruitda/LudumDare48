using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Spawner
{
    public static void SpawnRectangle(GameObject prefab, int width, int height)
    {
        for (int w = 0; w < width; w++)
            for (int h = 0; h < height; h++)
                GameObject.Instantiate(prefab).transform.position = new Vector2(w, h);
    }
}
