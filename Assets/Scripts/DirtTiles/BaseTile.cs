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
    public SpriteRepository SpriteRepository { get; private set; }
    public abstract ISoilComposition SoilComposition { get; set; }

    public BaseTile(int x, int y)
    {
        PosX = x;
        PosY = y;
    }

    void Start()
    {
        SpriteRepository = GameObject.Find("SpriteRepo").GetComponent<SpriteRepository>();
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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null)
            {
                BaseTile tile = hit.collider.gameObject.GetComponent<BaseTile>();
                IsRooted = true;
                tile.SpriteRenderer.sprite = SpriteRepository.RootSprite;
            }
        }
    }
}
