using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : BaseTile
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private int _nutritionalValue;
    [SerializeField]
    private int _minDepthToSpawn;
    private WaterComposition _waterComposition;
    [SerializeField]
    private int _probability;
    public override ISoilComposition SoilComposition { get => _waterComposition ?? new WaterComposition(_nutritionalValue); set => _waterComposition = value as WaterComposition; }
    public override SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }
    public override Sprite SelecetedSprite { get => GameMaster.SpriteRepo.WaterSelectedSprite; }

    public override Sprite DefaultSprite { get => GameMaster.SpriteRepo.WaterDefaultSprite; }
    public override int MinDepthToSpawn { get => _minDepthToSpawn; set => _minDepthToSpawn = value; }
    public override int ProbabilityToSpawn { get => _probability; set => _probability = value; }

    public WaterTile(int x, int y) : base(x, y)
    {
        _waterComposition = new WaterComposition(_nutritionalValue);
    }
}
