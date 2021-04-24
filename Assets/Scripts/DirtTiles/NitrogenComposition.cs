using UnityEngine;

public class NitrogenComposition : ISoilComposition
{
    [SerializeField]
    private int _nutritionalValue;

    public NitrogenComposition(int nutritionalValue)
    {
        _nutritionalValue = nutritionalValue;
    }
    public int GetNutritionalValue()
    {
        return _nutritionalValue;
    }
}