using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTile : MonoBehaviour
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public bool IsRooted { get; set; }
    public bool[] AdjacentRoots { get; set; }
    public abstract Sprite SelecetedSprite { get; }
    public abstract Sprite DefaultSprite { get; }
    public abstract SpriteRenderer SpriteRenderer { get; set; }
    private SpriteRepository _spriteRepository;
    private GameMaster _gameMaster;
    private const int MAX_SELECTION_DISTANCE = 1;
    public abstract ISoilComposition SoilComposition { get; set; }
    public Dictionary<NeighborDirections, BaseTile> Neighbors { get; set; }

    private Color DefaultColor;

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
        DefaultColor = SpriteRenderer.color;
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

    // TODO: Move all this code to some other input controller class and out of the base class
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
                        DeselectTile(_gameMaster.CurrentSelectedTile);
                        SelectTile(tile);                        
                    }
                }
            }
        }
    }

    public void SelectTile(BaseTile tile)
    {
        tile.IsRooted = true;
        tile.SpriteRenderer.sprite = _spriteRepository.RootSprite;
        _gameMaster.CurrentSelectedTile = tile;
        tile.HighlightNeighbors();
    }

    public void DeselectTile(BaseTile tile)
    {
        tile.UnHighlightNeighbors();
    }

    private bool IsValidTileSelection(BaseTile selectedTile)
    {
        // can't select already selected tile
        if (selectedTile.IsRooted)
            return false;

        // return if the tile is adjacent
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

    private void HighlightNeighbors()
    {
        foreach (var neighbor in Neighbors)
        {
            neighbor.Value.SpriteRenderer.color = Color.red;
        }
    }

    private void UnHighlightNeighbors()
    {
        foreach (var neighbor in Neighbors)
        {
            neighbor.Value.SpriteRenderer.color = DefaultColor;
        }
    }
}
