using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAnimPlaceJem : MonoBehaviour
{
    private CGameManager gmScript;
    private float objScale;
    private Vector3 tmpScale;
    private string pickedColor;
    public int pickedX;
    public int pickedY;
    
	// Use this for initialization
	void Start ()
    {

        gmScript = CGameManager.gameManager;
        pickedColor = gmScript.ReturnJorbleFromDeck(gmScript.SelectedSlot).JorbleColor;
        if (pickedColor == "blue")
        {
            Sprite newSprite = Resources.Load("TempJorbleBlue", typeof(Sprite)) as Sprite;
            GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        if (pickedColor == "red")
        {
            Sprite newSprite = Resources.Load("TempJorbleRed", typeof(Sprite)) as Sprite;
            GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        if (pickedColor == "green")
        {
            Sprite newSprite = Resources.Load("TempJorbleGreen", typeof(Sprite)) as Sprite;
            GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        if (pickedColor == "teal")
        {
            Sprite newSprite = Resources.Load("TempJorbleTeal", typeof(Sprite)) as Sprite;
            GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        if (pickedColor == "pink")
        {
            Sprite newSprite = Resources.Load("TempJorblePink", typeof(Sprite)) as Sprite;
            GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        if (pickedColor == "lime")
        {
            Sprite newSprite = Resources.Load("TempJorbleLime", typeof(Sprite)) as Sprite;
            GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        objScale = 1f;
        transform.position = new Vector3(pickedX + 0.5f, pickedY + 0.5f, -1f);

        StartCoroutine(WaitForSmoke());
    }
	
	// Update is called once per frame
	void Update ()
    {
        objScale -= Time.deltaTime * 0.25f;
        tmpScale = Vector3.one * objScale;
        transform.localScale = tmpScale;
	}

    IEnumerator WaitForSmoke()
    {
        gmScript.RemoveJorbleFromDeckInstance(gmScript.SelectedSlot);
        gmScript.ResetJorbleSlot();
        yield return new WaitForSeconds(1);
        gmScript.PauseForAnim(false);
        if (gmScript.RemainingJorbles == 0)
        {
            gmScript.FinishTower(false);
        }
        Destroy(this.gameObject);
    }
}
