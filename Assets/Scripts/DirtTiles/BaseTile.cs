using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTile : MonoBehaviour
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public bool IsRooted { get; set; }
    public abstract Sprite SelecetedSprite { get; }
    public abstract Sprite DefaultSprite { get; }
    public abstract SpriteRenderer SpriteRenderer { get; set; }
    private SpriteRepository _spriteRepository;
    private GameMaster _gameMaster;
    private const int MAX_SELECTION_DISTANCE = 1;
    public abstract ISoilComposition SoilComposition { get; set; }

    public BaseTile(int x, int y)
    {
        PosX = x;
        PosY = y;
    }

    void Start()
    {
        // TODO: Cache these somewhere so this isn't done every time we create a tile
        _spriteRepository = GameObject.Find("SpriteRepo").GetComponent<SpriteRepository>();
        _gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    void OnMouseEnter()
    {
        if (IsRooted)
            return;

        SpriteRenderer.sprite = SelecetedSprite;
    }

    void OnMouseExit()
    {
        if (IsRooted)
            return;

        SpriteRenderer.sprite = DefaultSprite;
    }


    void Update()
    {
        RootTileIfClicked();
    }

    private void RootTileIfClicked()
    {
        // if LMB clicked
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // did we hit something
            if (hit.collider != null)
            {
                BaseTile tile = hit.collider.gameObject.GetComponent<BaseTile>();

                // if we hit a tile, root it
                if (tile != null)
                {
                    if (IsValidTileSelection(tile))
                    {
                        tile.IsRooted = true;
                        tile.SpriteRenderer.sprite = _spriteRepository.RootSprite;
                        _gameMaster.CurrentSelectedTile = tile;
                    }
                }
            }
        }
    }

    private bool IsValidTileSelection(BaseTile selectedTile)
    {
        // if the currently selected tile is selected again, return false since we don't want to re-root it
        if (selectedTile.PosX == _gameMaster.CurrentSelectedTile.PosX
            && selectedTile.PosY == _gameMaster.CurrentSelectedTile.PosY)
            return false;

        return IsTileAdjacent(selectedTile);
    }

    private bool IsTileAdjacent(BaseTile selectedTile)
    {
        int selectedX = selectedTile.PosX;
        int selectedY = selectedTile.PosY;
        int currentX = _gameMaster.CurrentSelectedTile.PosX;
        int currentY = _gameMaster.CurrentSelectedTile.PosY;

        // if the tile is too far away, return false;
        if ( Mathf.Abs(selectedX - currentX) > MAX_SELECTION_DISTANCE 
            || Mathf.Abs(selectedY - currentY) > MAX_SELECTION_DISTANCE)
            return false;

        for (int x = currentX - MAX_SELECTION_DISTANCE; x <= currentX + MAX_SELECTION_DISTANCE; x++)
            for (int y = currentY; y <= currentY + MAX_SELECTION_DISTANCE; y++)
                if (x == selectedX && y == selectedY)
                    return true;

        return false;
    }
}
