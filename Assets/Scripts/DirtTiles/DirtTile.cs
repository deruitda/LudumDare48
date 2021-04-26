using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtTile : BaseTile
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private int _nutritionalValue;   
    private DirtComposition _dirtComposition;

    public override ISoilComposition SoilComposition { get => _dirtComposition ?? new DirtComposition(_nutritionalValue); set => _dirtComposition = value as DirtComposition; }
    public override SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }
    public override Sprite SelecetedSprite { get => GameMaster.SpriteRepo.DirtSelectedSprite; }
    
    public override Sprite DefaultSprite { get => GameMaster.SpriteRepo.DirtDefaultSprite; }

    public DirtTile(int x, int y) : base(x, y)
    {
        _dirtComposition = new DirtComposition(_nutritionalValue);
    }
}
