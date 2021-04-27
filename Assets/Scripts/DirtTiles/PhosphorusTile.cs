using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhosphorusTile : BaseTile
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private int _nutritionalValue;
    [SerializeField]
    private int _minDepthToSpawn;

    private PhosphorusComposition _phosphorusComposition;
    public override SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }
    public override Sprite SelecetedSprite { get => GameMaster.SpriteRepo.PhosphorusSelectedSprite; }
    public override Sprite DefaultSprite { get => GameMaster.SpriteRepo.PhosphorusDefaultSprite; }
    public override int MinDepthToSpawn { get => _minDepthToSpawn; set => _minDepthToSpawn = value; }
    public override ISoilComposition SoilComposition { get => _phosphorusComposition ?? new PhosphorusComposition(_nutritionalValue); set => _phosphorusComposition = value as PhosphorusComposition; }

    public PhosphorusTile(int x, int y) : base(x, y)
    {
        _phosphorusComposition = new PhosphorusComposition(_nutritionalValue);
    }
}
