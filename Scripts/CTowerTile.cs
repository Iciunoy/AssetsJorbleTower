using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTowerTile : MonoBehaviour
{
    private int gridPosX;
    private int gridPosY;
    private int tileHeight;
    private string spriteName;
    public Sprite defaultSprite;
    public Sprite bombSprite;
    //private bool placeableHighlight;
    public GameObject highlightObject;
    public GameObject smokeObject;
    private GameObject highlightInstance;
    private GameObject smokeObjectInstance;

    public int GridPosX
    {
        get
        {
            return gridPosX;
        }

        set
        {
            gridPosX = value;
        }
    }
    public int GridPosY
    {
        get
        {
            return gridPosY;
        }

        set
        {
            gridPosY = value;
        }
    }
    public int TileHeight
    {
        get
        {
            return tileHeight;
        }

        set
        {
            tileHeight = value;
        }
    }
    public string SpriteName
    {
        get
        {
            return spriteName;
        }

        set
        {
            spriteName = value;
        }
    }


    // Use this for initialization
    void Start ()
    {
        SpriteName = GetComponent<SpriteRenderer>().sprite.name;

        //placeableHighlight = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.tag == "TilePlaceable" && transform.childCount == 0)
        {
            highlightInstance = Instantiate(highlightObject) as GameObject;
            highlightInstance.transform.SetParent(this.transform);
        }
        if (gameObject.tag != "TilePlaceable" && transform.childCount > 0)
        {
            GameObject.Destroy(transform.GetChild(0).gameObject);
        }
    }

    public Vector2 ReturnGridPos()
    {
        Vector2 p = new Vector2(GridPosX, GridPosY);
        return p;
    }

    public void SetGridPos(int x, int y)
    {
        GridPosX = x;
        GridPosY = y;
    }

    public void TilePlaceable()
    {
        if (gameObject.tag != "TileBomb")
        {
            gameObject.tag = "TilePlaceable";
        }
        //placeableHighlight = true;
    }

    public void CreateNewTile(int h)
    {
        if (gameObject.tag != "TilePlaceable")
        {
            gameObject.tag = "TileOpen";
            GetComponent<SpriteRenderer>().sprite = defaultSprite;
            SpriteName = defaultSprite.name;
        }
        TileHeight = h;
        if (TileHeight > 5)
        {
            float tempBombChance = 35f - (TileHeight * 0.05f);
            RollBombTile(tempBombChance);
        }
    }

    public void ChangeTileType(string tileSpriteName)
    {
        SpriteName = tileSpriteName;
        //Debug.Log("MAKE THE SMOKES NOW");
        if (tileSpriteName == "TempJemSteel")
        {
            Sprite newSprite = Resources.Load(SpriteName, typeof(Sprite)) as Sprite;
            GetComponent<SpriteRenderer>().sprite = newSprite;
            gameObject.tag = "TileTaken";
            return;
        }
        smokeObjectInstance = Instantiate(smokeObject) as GameObject;
        smokeObjectInstance.transform.position = new Vector3(transform.position.x, transform.position.y, -2f);
        gameObject.tag = "TileTaken";
        StartCoroutine(ChangeTileEnum());
    }
    IEnumerator ChangeTileEnum()
    {
        yield return new WaitForSeconds(1);
        //Debug.Log("NOW CHANGE SPRITE");
        Sprite newSprite = Resources.Load(SpriteName, typeof(Sprite)) as Sprite;
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    public void RollBombTile(float bombChance)
    {
        float rng = Random.Range(0f, bombChance);
        if (rng <= 1f)
        {
            gameObject.tag = "TileBomb";
            GetComponent<SpriteRenderer>().sprite = bombSprite;
            SpriteName = bombSprite.name;
        }
    }

    public void CopyTileType(GameObject otherTile)
    {
        gameObject.tag = otherTile.tag;
        CTowerTile otherScript = otherTile.GetComponent<CTowerTile>();
        TileHeight = otherScript.TileHeight;
        SpriteName = otherScript.SpriteName;
        Sprite newSprite = otherTile.GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
