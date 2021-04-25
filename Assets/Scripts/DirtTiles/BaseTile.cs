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
    public abstract ISoilComposition SoilComposition { get; set; }
    public Dictionary<NeighborDirections, BaseTile> Neighbors { get; set; }
    private GameMaster _gameMaster;
    private const int MAX_SELECTION_DISTANCE = 1;
    private Color DefaultColor;

    public BaseTile(int x, int y)
    {
        PosX = x;
        PosY = y;
    }

    void Awake()
    {
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
                    if (tile.IsValidTileSelection())
                    {
                        _gameMaster.CurrentSelectedTile.DeselectTile();
                        tile.SelectTile();                        
                    }
                }
            }
        }
    }

    public void SelectTile()
    {
        IsRooted = true;
        SpriteRenderer.sprite = _gameMaster.SpriteRepo.RootSprite;
        _gameMaster.CurrentSelectedTile = this;
        HighlightNeighbors();
    }

    public void DeselectTile()
    {
        UnHighlightNeighbors();
    }

    private bool IsValidTileSelection()
    {
        // can't select already selected tile
        if (IsRooted)
            return false;

        // return if the tile is adjacent
        return IsTileAdjacentToCurrent();
    }

    private bool IsTileAdjacentToCurrent()
    {
        int selectedX = PosX;
        int selectedY = PosY;
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
            neighbor.Value.SpriteRenderer.color = new Color(92, 252, 71, .5f);
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
