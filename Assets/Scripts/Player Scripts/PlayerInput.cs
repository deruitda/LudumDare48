using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    protected GameMaster GameMaster;
    void Start()
    {
      GameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        ClickEvent();
    }


public void ClickEvent(){
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
                        GameMaster.CurrentSelectedTile.DeselectTile();
                        tile.SelectTile();
                    }
                }
            }
        }
    }
}