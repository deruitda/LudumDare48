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
    public abstract int MinDepthToSpawn { get; set; }
    public abstract int ProbabilityToSpawn { get; set; }
    public Dictionary<NeighborDirections, BaseTile> Neighbors { get; set; }
    protected GameMaster GameMaster;
    private const int MAX_SELECTION_DISTANCE = 1;
    private Color DefaultColor;

    public BaseTile(int x, int y)
    {
        PosX = x;
        PosY = y;
    }

    void Awake()
    {
        GameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
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


    void Update()
    {

    }

    public void SelectDirtTile(bool isInitialSelection = false)
    {
        IsRooted = true;
        SpriteRenderer.sprite = GameMaster.SpriteRepo.RootSprite;
        GameMaster.CurrentSelectedTile = this;
        //GameMaster.RootSystem.UpdateDeepestRootTile(this);
        GameMaster.RootSystem.AddRootToRootSystem(this);
        if (this.GetType() == typeof(WaterTile))
            GameMaster.UpdateWaterRemaining(10);
        else if(!isInitialSelection)
        {
            // don't update the water if we are selecting the initial root at the start of the game
            GameMaster.UpdateWaterRemaining(-1); // TODO change this from a hardcoded amount?
        }
        GameMaster.UpdateNutrientScore(SoilComposition.GetNutritionalValue());
        HighlightSelectableTiles();
    }

    public void SelectRootTile()
    {

        GameMaster.CurrentSelectedTile = this;
        HighlightSelectableTiles();
    }

    public void DeselectTile()
    {
        UnhighlightSelectableTiles();
    }

    public bool IsValidTileSelection()
    {
        // reselect root
        if (IsRooted)        
            return true;        

        // return if the tile is adjacent
        return IsTileAdjacentToCurrent();
    }

    private bool IsTileAdjacentToCurrent()
    {
        int selectedX = PosX;
        int selectedY = PosY;
        int currentX = GameMaster.CurrentSelectedTile.PosX;
        int currentY = GameMaster.CurrentSelectedTile.PosY;

        if ((selectedX == (currentX - 1) || selectedX == (currentX + 1))
            && (selectedY == currentY))
            return true;

        if (selectedX == currentX && selectedY == currentY + 1)
            return true;

        return false;
    }

    private void HighlightSelectableTiles()
    {
        HighlightNeighbors();
        HighlightRoots();
    }

    private void HighlightRoots()
    {
        foreach(var root in GameMaster.RootSystem.GetRoots())
        {
            root.SpriteRenderer.color = new Color(92, 252, 71, .5f);
        }
    }

    private void HighlightNeighbors()
    {
        foreach (var neighbor in Neighbors)
        {
            if (!neighbor.Value.IsRooted)
                neighbor.Value.SpriteRenderer.color = new Color(92, 252, 71, .5f);
        }
    }

    private void UnhighlightSelectableTiles()
    {
        UnHighlightNeighbors();
        UnHighlightRoots();
    }

    private void UnHighlightRoots()
    {
        foreach (var root in GameMaster.RootSystem.GetRoots())
        {
            root.SpriteRenderer.color = DefaultColor;
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
