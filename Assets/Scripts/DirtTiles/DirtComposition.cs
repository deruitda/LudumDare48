using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DirtComposition : ISoilComposition
{
    [SerializeField]
    private int _nutritionalValue;

    public DirtComposition(int nutritionalValue)
    {
        _nutritionalValue = nutritionalValue;
    }

    public int GetNutritionalValue()
    {
        return _nutritionalValue;
    }
}

