using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIPreview : MonoBehaviour
{
    public Sprite[] jSprites = new Sprite[3];
    private CGameManager gmScript;
    private Image childImage;
    private string jColor;

    public string JColor
    {
        get
        {
            return jColor;
        }

        set
        {
            jColor = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        gmScript = CGameManager.gameManager;
        childImage = transform.GetChild(0).gameObject.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gmScript.SelectedSlot < 0)
        {
            childImage.sprite = null;
        }
        if (gmScript.SelectedSlot >= 0)
        {
            string tempc = gmScript.ReturnJorbleFromDeck(gmScript.SelectedSlot).JorbleColor;
            ChangeJorbleType(tempc);
        }
    }

    public void ChangeJorbleType(string clr)
    {
        JColor = clr;
        if (clr == "blue")
        {
            childImage.sprite = jSprites[0];
        }
        if (clr == "red")
        {
            childImage.sprite = jSprites[1];
        }
        if (clr == "green")
        {
            childImage.sprite = jSprites[2];
        }
        if (clr == "teal")
        {
            childImage.sprite = jSprites[3];
        }
        if (clr == "pink")
        {
            childImage.sprite = jSprites[4];
        }
        if (clr == "lime")
        {
            childImage.sprite = jSprites[5];
        }
        if (clr == "purple")
        {
            childImage.sprite = jSprites[6];
        }
        if (clr == "orange")
        {
            childImage.sprite = jSprites[7];
        }
        if (clr == "yellow")
        {
            childImage.sprite = jSprites[8];
        }
    }
}
