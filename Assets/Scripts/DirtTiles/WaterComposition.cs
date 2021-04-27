using UnityEngine;

internal class WaterComposition : ISoilComposition
{
    [SerializeField]
    private int _nutritionalValue;

    public WaterComposition(int nutritionalValue)
    {
        _nutritionalValue = nutritionalValue;
    }
    public int GetNutritionalValue()
    {
        return 0;
    }
}