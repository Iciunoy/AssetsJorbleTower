using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTowerGrid : MonoBehaviour
{
    private GameObject[,] tiles = new GameObject[11, 10];
    private int tileX;
    private int tileY;
    public GameObject tilePrefab;
    public GameObject jemObject;
    private GameObject tileInstance;
    private GameObject jemInstance;
    private CGameManager gmScript;
    private CCamera camScript;
    private int newScrollHeight;

	// Use this for initialization
	void Start ()
    {
        gmScript = CGameManager.gameManager;
        //gmScript.DealNewDeck();
        camScript = Camera.main.gameObject.GetComponent<CCamera>();
        gmScript.ScrolledHeight = 0;
        gmScript.UpdateNewHeight(0);
        for (int i = 9; i >= 0; i--)
        {
            for (int k = 10; k >= 0; k--)
            {
                tileInstance = Instantiate(tilePrefab);
                tileInstance.transform.SetParent(this.transform); 
                tileInstance.transform.position += new Vector3(0.5f + k, 0.5f+i, 0f);
                tileInstance.GetComponent<CTowerTile>().SetGridPos(k, i);
                tileInstance.GetComponent<CTowerTile>().CreateNewTile(i);
                tiles[k, i] = tileInstance.gameObject;
                if (i < 1)
                {
                    
                    ApplyJorble("gray", k, i);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

        if ((newScrollHeight) > gmScript.ScrolledHeight && !CGameManager.pauseForAnimation)
        {
            //WHEN I GET BACK, MAKE SURE THIS SHIT SCROLLS AND DOES IT AT THE RIGHT TIME, NOT RIGHT AWAY.
            if (newScrollHeight > 4)
            {
                ScrollGrid(newScrollHeight - gmScript.ScrolledHeight);
                foreach (GameObject smoke in GameObject.FindGameObjectsWithTag("AnimSmoke"))
                {
                    smoke.transform.position -= new Vector3(0f, newScrollHeight - gmScript.ScrolledHeight, 0f);
                }
            }
            gmScript.ScrolledHeight = newScrollHeight;
            int placeableTiles = GameObject.FindGameObjectsWithTag("TilePlaceable").Length;
            if (placeableTiles < 1)
            {
                gmScript.FinishTower(true);
            }
        }
        if (Input.GetMouseButtonDown(0) && !CGameManager.pauseForAnimation)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //DESELECT JORBLE
            if (hit.collider != null && hit.collider.tag != "TilePlaceable")
            {
                gmScript.ResetJorbleSlot();
            }
            //APPLIES JORBLE TO SLOT IF ONE IS SELECTED
            if (hit.collider != null && hit.collider.tag == "TilePlaceable" && gmScript.SelectedSlot >= 0)
            {
                Vector2 orig = hit.collider.gameObject.GetComponent<CTowerTile>().ReturnGridPos();
                string tempc = gmScript.ReturnJorbleFromDeck(gmScript.SelectedSlot).JorbleColor;
                //ApplyJorble(tempc, (int)orig.x, (int)orig.y);
                ChangeJorbleAnimation(tempc, (int)orig.x, (int)orig.y);
                CGameManager.pauseForAnimation = true;
            }
        }
	}

    
    public void ChangeJorbleAnimation(string placedColor, int placedTileX, int placedTileY)
    {
        jemInstance = Instantiate(jemObject);
        jemInstance.transform.SetParent(this.transform);
        jemInstance.GetComponent<CAnimPlaceJem>().pickedX = placedTileX;
        jemInstance.GetComponent<CAnimPlaceJem>().pickedY = placedTileY;
        ApplyJorble(placedColor, placedTileX, placedTileY);
    }

    public void ApplyJorble(string color, int originX, int originY)
    {
        Debug.Log(color);

        if (color == "gray")
        {
            //THESE ARE THE STARTING BASE BLOCKS
            tiles[originX, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemSteel");
            tiles[originX, originY + 1].GetComponent<CTowerTile>().TilePlaceable();
        }
        if (color == "blue")
        {
            if (tiles[originX, originY].tag == "TileBomb" ||
                tiles[originX, originY + 1].tag == "TileBomb" ||
                tiles[originX, originY + 2].tag == "TileBomb")
            {
                gmScript.FinishTower(true);
                return;
            }
            tiles[originX, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemBlue");
            tiles[originX, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemBlue");
            tiles[originX, originY + 2].GetComponent<CTowerTile>().ChangeTileType("TempJemBlue");
            int finalH = tiles[originX, originY + 2].GetComponent<CTowerTile>().TileHeight;
            tiles[originX, originY + 3].GetComponent<CTowerTile>().TilePlaceable();
            newScrollHeight = finalH;
        }
        if (color == "teal")
        {
            if (tiles[originX, originY].tag == "TileBomb" ||
                tiles[originX, originY + 1].tag == "TileBomb" ||
                tiles[originX, originY + 2].tag == "TileBomb" ||
                tiles[originX, originY + 3].tag == "TileBomb")
            {
                gmScript.FinishTower(true);
                return;
            }
            tiles[originX, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemTeal");
            tiles[originX, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemTeal");
            tiles[originX, originY + 2].GetComponent<CTowerTile>().ChangeTileType("TempJemTeal");
            tiles[originX, originY + 3].GetComponent<CTowerTile>().ChangeTileType("TempJemTeal");
            int finalH = tiles[originX, originY + 3].GetComponent<CTowerTile>().TileHeight;
            tiles[originX, originY + 4].GetComponent<CTowerTile>().TilePlaceable();
            newScrollHeight = finalH;
        }
        if (color == "purple")
        {
            if (tiles[originX, originY].tag == "TileBomb" ||
                tiles[originX, originY + 1].tag == "TileBomb" ||
                tiles[originX, originY + 2].tag == "TileBomb" ||
                tiles[originX, originY + 3].tag == "TileBomb")
            {
                gmScript.FinishTower(true);
                return;
            }
            tiles[originX, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemPurple");
            tiles[originX, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemPurple");
            tiles[originX, originY + 2].GetComponent<CTowerTile>().ChangeTileType("TempJemPurple");
            tiles[originX, originY + 3].GetComponent<CTowerTile>().ChangeTileType("TempJemPurple");
            tiles[originX, originY + 4].GetComponent<CTowerTile>().TilePlaceable();
            if (originX > 0)
            {
                if (tiles[originX - 1, originY + 2].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX - 1, originY + 2].GetComponent<CTowerTile>().ChangeTileType("TempJemPurple");
                tiles[originX - 1, originY + 3].GetComponent<CTowerTile>().TilePlaceable();
            }
            if (originX < 10)
            {
                if (tiles[originX + 1, originY + 2].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX + 1, originY + 2].GetComponent<CTowerTile>().ChangeTileType("TempJemPurple");
                tiles[originX + 1, originY + 3].GetComponent<CTowerTile>().TilePlaceable();
            }
            int finalH = tiles[originX, originY + 3].GetComponent<CTowerTile>().TileHeight;

            newScrollHeight = finalH;
        }
        if (color == "red")
        {
            if (tiles[originX, originY].tag == "TileBomb" ||
                tiles[originX, originY + 1].tag == "TileBomb")
            {
                gmScript.FinishTower(true);
                return;
            }
            tiles[originX, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemRed");
            tiles[originX, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemRed");
            tiles[originX, originY + 2].GetComponent<CTowerTile>().TilePlaceable();
            if (originX > 0)
            {
                if (tiles[originX - 1, originY].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX - 1, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemRed");
                tiles[originX - 1, originY + 1].GetComponent<CTowerTile>().TilePlaceable();
            }
            if (originX < 10)
            {
                if (tiles[originX + 1, originY].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX + 1, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemRed");
                tiles[originX + 1, originY + 1].GetComponent<CTowerTile>().TilePlaceable();
            }
            int finalH = tiles[originX, originY + 1].GetComponent<CTowerTile>().TileHeight;

            newScrollHeight = finalH;
        }
        if (color == "pink")
        {
            if (tiles[originX, originY].tag == "TileBomb" ||
                tiles[originX, originY + 1].tag == "TileBomb" ||
                tiles[originX, originY + 2].tag == "TileBomb")
            {
                gmScript.FinishTower(true);
                return;
            }
            tiles[originX, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemPink");
            tiles[originX, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemPink");
            tiles[originX, originY + 2].GetComponent<CTowerTile>().ChangeTileType("TempJemPink");
            tiles[originX, originY + 3].GetComponent<CTowerTile>().TilePlaceable();
            if (originX > 0)
            {
                if (tiles[originX - 1, originY + 1].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX - 1, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemPink");
                tiles[originX - 1, originY + 2].GetComponent<CTowerTile>().TilePlaceable();
            }
            if (originX < 10)
            {
                if (tiles[originX + 1, originY + 1].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX + 1, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemPink");
                tiles[originX + 1, originY + 2].GetComponent<CTowerTile>().TilePlaceable();
            }
            int finalH = tiles[originX, originY + 1].GetComponent<CTowerTile>().TileHeight;

            newScrollHeight = finalH;
        }
        if (color == "orange")
        {
            if (tiles[originX, originY].tag == "TileBomb" ||
                tiles[originX, originY + 1].tag == "TileBomb" ||
                tiles[originX, originY + 2].tag == "TileBomb")
            {
                gmScript.FinishTower(true);
                return;
            }
            tiles[originX, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemOrange");
            tiles[originX, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemOrange");
            tiles[originX, originY + 2].GetComponent<CTowerTile>().ChangeTileType("TempJemOrange");
            tiles[originX, originY + 3].GetComponent<CTowerTile>().TilePlaceable();
            if (originX > 0)
            {
                if (tiles[originX - 1, originY + 1].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX - 1, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemOrange");
                tiles[originX - 1, originY + 2].GetComponent<CTowerTile>().TilePlaceable();
            }
            if (originX > 1)
            {
                if (tiles[originX - 2, originY + 1].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX - 2, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemOrange");
                tiles[originX - 2, originY + 2].GetComponent<CTowerTile>().TilePlaceable();
            }
            if (originX < 10)
            {
                if (tiles[originX + 1, originY + 1].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX + 1, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemOrange");
                tiles[originX + 1, originY + 2].GetComponent<CTowerTile>().TilePlaceable();
            }
            if (originX < 9)
            {
                if (tiles[originX + 2, originY + 1].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX + 2, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemOrange");
                tiles[originX + 2, originY + 2].GetComponent<CTowerTile>().TilePlaceable();
            }
            int finalH = tiles[originX, originY + 2].GetComponent<CTowerTile>().TileHeight;

            newScrollHeight = finalH;
        }
        if (color == "green")
        {
            if (tiles[originX, originY].tag == "TileBomb" ||
                tiles[originX, originY + 1].tag == "TileBomb")
            {
                gmScript.FinishTower(true);
                return;
            }
            tiles[originX, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemGreen");
            tiles[originX, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemGreen");
            tiles[originX, originY + 2].GetComponent<CTowerTile>().TilePlaceable();
            if (originX > 0)
            {
                if (tiles[originX - 1, originY].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX - 1, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemGreen");
                tiles[originX - 1, originY + 1].GetComponent<CTowerTile>().TilePlaceable();
            }
            if (originX > 1)
            {
                if (tiles[originX - 2, originY].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX - 2, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemGreen");
                tiles[originX - 2, originY + 1].GetComponent<CTowerTile>().TilePlaceable();
            }
            int finalH = tiles[originX, originY + 1].GetComponent<CTowerTile>().TileHeight;

            newScrollHeight = finalH;
        }
        if (color == "lime")
        {
            if (tiles[originX, originY].tag == "TileBomb" ||
                tiles[originX, originY + 1].tag == "TileBomb" ||
                tiles[originX, originY + 2].tag == "TileBomb")
            {
                gmScript.FinishTower(true);
                return;
            }
            tiles[originX, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemLime");
            tiles[originX, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemLime");
            tiles[originX, originY + 2].GetComponent<CTowerTile>().ChangeTileType("TempJemLime");
            tiles[originX, originY + 3].GetComponent<CTowerTile>().TilePlaceable();
            if (originX < 10)
            {
                if (tiles[originX + 1, originY + 1].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX + 1, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemLime");
                tiles[originX + 1, originY + 2].GetComponent<CTowerTile>().TilePlaceable();
            }
            if (originX < 9)
            {
                if (tiles[originX + 2, originY + 1].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX + 2, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemLime");
                tiles[originX + 2, originY + 2].GetComponent<CTowerTile>().TilePlaceable();
            }
            int finalH = tiles[originX + 2, originY + 2].GetComponent<CTowerTile>().TileHeight;

            newScrollHeight = finalH;
        }
        if (color == "yellow")
        {
            if (tiles[originX, originY].tag == "TileBomb" ||
                tiles[originX, originY + 1].tag == "TileBomb" ||
                tiles[originX, originY + 2].tag == "TileBomb" ||
                tiles[originX, originY + 3].tag == "TileBomb")
            {
                gmScript.FinishTower(true);
                return;
            }
            tiles[originX, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemYellow");
            tiles[originX, originY + 1].GetComponent<CTowerTile>().ChangeTileType("TempJemYellow");
            tiles[originX, originY + 2].GetComponent<CTowerTile>().ChangeTileType("TempJemYellow");
            tiles[originX, originY + 3].GetComponent<CTowerTile>().ChangeTileType("TempJemYellow");
            tiles[originX, originY + 4].GetComponent<CTowerTile>().TilePlaceable();
            if (originX > 0)
            {
                if (tiles[originX - 1, originY].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX - 1, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemYellow");
                tiles[originX - 1, originY + 1].GetComponent<CTowerTile>().TilePlaceable();
            }
            if (originX > 1)
            {
                if (tiles[originX - 2, originY].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX - 2, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemYellow");
                tiles[originX - 2, originY + 1].GetComponent<CTowerTile>().TilePlaceable();
            }
            if (originX < 10)
            {
                if (tiles[originX + 1, originY].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX + 1, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemYellow");
                tiles[originX + 1, originY + 1].GetComponent<CTowerTile>().TilePlaceable();
            }
            if (originX < 9)
            {
                if (tiles[originX + 2, originY].tag == "TileBomb")
                {
                    gmScript.FinishTower(true);
                    return;
                }
                tiles[originX + 2, originY].GetComponent<CTowerTile>().ChangeTileType("TempJemYellow");
                tiles[originX + 2, originY + 1].GetComponent<CTowerTile>().TilePlaceable();
            }
            int finalH = tiles[originX, originY + 3].GetComponent<CTowerTile>().TileHeight;

            newScrollHeight = finalH;
        }
        gmScript.UpdateNewHeight(newScrollHeight);
    }

    public void ScrollGrid(int scrollValue)
    {
        //First, move the rows down
        //int rowsRemaining = 8 - scrollValue;

        //Debug.Log("SCROLL PLZ");
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 11; x++)
            {
                if (y < 10 - scrollValue)
                {
                    Vector2 temp = tiles[x, y].GetComponent<CTowerTile>().ReturnGridPos();
                    tiles[x, y].GetComponent<CTowerTile>().CopyTileType(tiles[x, y + scrollValue]);
                    tiles[x, y].GetComponent<CTowerTile>().SetGridPos((int)temp.x, (int)temp.y);
                }
                if (y >= 10 - scrollValue)
                {
                    int newH = tiles[x, (y - 1)].GetComponent<CTowerTile>().TileHeight + 1;
                    tiles[x, y].tag = "TileOpen";
                    tiles[x, y].GetComponent<CTowerTile>().CreateNewTile(newH);
                }
            }
        }
    }
    
}
