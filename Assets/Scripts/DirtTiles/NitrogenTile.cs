using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitrogenTile : BaseTile
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private int _nutritionalValue;
    [SerializeField]
    private int _minDepthToSpawn;
    [SerializeField]
    private int _probability;

    private NitrogenComposition _nitrogenComposition;
    public override SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }
    public override Sprite SelecetedSprite { get => GameMaster.SpriteRepo.NitrogenSelectedSprite; }
    public override Sprite DefaultSprite { get => GameMaster.SpriteRepo.NitrogenDefaultSprite; }
    public override int MinDepthToSpawn { get => _minDepthToSpawn; set => _minDepthToSpawn = value; }
    public override ISoilComposition SoilComposition { get => _nitrogenComposition ?? new NitrogenComposition(_nutritionalValue); set => _nitrogenComposition = value as NitrogenComposition; }
    public override int ProbabilityToSpawn { get => _probability; set => _probability = value; }

    public NitrogenTile(int x, int y) : base(x, y)
    {
        _nitrogenComposition = new NitrogenComposition(_nutritionalValue);
    }
}
