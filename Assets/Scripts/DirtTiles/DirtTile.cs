using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtTile : BaseTile
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private int _nutritionalValue;
    [SerializeField]
    private Sprite _defaultSprite;
    [SerializeField]
    private Sprite _selectedSprite;
    private DirtComposition _dirtComposition;

    public override ISoilComposition SoilComposition { get => _dirtComposition; set => _dirtComposition = value as DirtComposition; }
    public override SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }
    public override Sprite SelecetedSprite { get => _selectedSprite; }
    
    public override Sprite DefaultSprite { get => _defaultSprite; }

    public DirtTile(int x, int y) : base(x, y)
    {
        _dirtComposition = new DirtComposition(_nutritionalValue);
    }
}
