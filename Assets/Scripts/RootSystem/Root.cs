using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public Root ParentRoot { get; set; }
    public List<Root> ConnectedRoots { get; set; }

    public int PosX { get; private set; }
    public int PosY { get; private set; }
    public Root(int x, int y)
    {
        PosX = x;
        PosY = y;
        ConnectedRoots = new List<Root>();
    }

    public void AddConnectedRoot(Root root)
    {
        ConnectedRoots.Add(root);
    }
}
