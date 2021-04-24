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
    private Sprite _defaultSprite;
    [SerializeField]
    private Sprite _selectedSprite;

    private NitrogenComposition _nitrogenComposition;
    public override SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }
    public override Sprite SelecetedSprite { get => _selectedSprite; }
    public override Sprite DefaultSprite { get => _defaultSprite; }
    public override ISoilComposition SoilComposition { get => _nitrogenComposition; set => _nitrogenComposition = value as NitrogenComposition; }

    public NitrogenTile(int x, int y) : base(x, y)
    {
        _nitrogenComposition = new NitrogenComposition(_nutritionalValue);
    }
}
