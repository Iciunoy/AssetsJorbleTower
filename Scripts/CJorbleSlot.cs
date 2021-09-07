using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CJorbleSlot : MonoBehaviour
{
    public Sprite[] jSprites = new Sprite[9];
    public int slotNum;
    private CGameManager gmScript;
    private Image childImage;
    private bool jorbleSelected;
    private string jColor;
    private Button myselfButton;

    public bool JorbleSelected
    {
        get
        {
            return jorbleSelected;
        }

        set
        {
            jorbleSelected = value;
        }
    }
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
    public Image ChildImage
    {
        get
        {
            return childImage;
        }

        set
        {
            childImage = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        gmScript = CGameManager.gameManager;
        childImage = transform.GetChild(0).gameObject.GetComponent<Image>();
        if (slotNum + 1 <= gmScript.CheckJorblesInDeck())
        {
            ChangeJorbleType(gmScript.ReturnJorbleFromDeck(slotNum).JorbleColor);
        }
        //ChangeJorbleType(gmScript.ReturnJorbleFromDeck(slotNum).JorbleColor);

        myselfButton = GetComponent<Button>();
        myselfButton.onClick.AddListener(() => { gmScript.SelectJorbleSlot(slotNum); });
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gmScript.RemainingJorbles <= slotNum)
        {
            childImage.sprite = null;
            myselfButton.onClick.RemoveAllListeners();
        }
        if (gmScript.RemainingJorbles > slotNum && gmScript.ReturnJorbleFromDeck(slotNum).JorbleColor != JColor)
        {
            ChangeJorbleType(gmScript.ReturnJorbleFromDeck(slotNum).JorbleColor);
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
        if (clr == "yellow")
        {
            childImage.sprite = jSprites[6];
        }
        if (clr == "purple")
        {
            childImage.sprite = jSprites[7];
        }
        if (clr == "orange")
        {
            childImage.sprite = jSprites[8];
        }
    }
}
