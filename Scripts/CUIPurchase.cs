using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIPurchase : MonoBehaviour
{
    private bool isWindowActive;
    private RectTransform rTransform;

    private string JColor;
    private Sprite jSprite;
    private int jCost;
    private int jOwned;
    private int jMax;
    private Text costText;
    private Text ownedText;
    private Text maxText;
    private CGameManager gmScript;
    private Button buttonNo;
    private Button buttonYes;

    // Use this for initialization
    void Start ()
    {
        gmScript = CGameManager.gameManager;
        //buttonYes = transform.GetChild(5).gameObject.GetComponent<Button>();
        //buttonYes.onClick.AddListener(() => { this.PurchaseJorble(); });
        //buttonNo = transform.GetChild(6).gameObject.GetComponent<Button>();
        //buttonNo.onClick.AddListener(() => { ActivateWindow(JColor, jSprite); });
        isWindowActive = false;
        rTransform = gameObject.GetComponent<RectTransform>();
        rTransform.localScale = Vector3.zero;

        costText = transform.GetChild(2).gameObject.GetComponent<Text>();
        ownedText = transform.GetChild(3).gameObject.GetComponent<Text>();
        maxText = transform.GetChild(4).gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ActivateWindow(string newColor)
    {
        if (isWindowActive)
        {
            isWindowActive = false;
            rTransform.localScale = Vector3.zero;
        }
        else
        {
            isWindowActive = true;
            rTransform.localScale = Vector3.one;
            JColor = newColor;
            if (JColor == "blue")
            {
                jSprite = Resources.Load<Sprite>("JemPreviewBlue");
            }
            if (JColor == "red")
            {
                jSprite = Resources.Load<Sprite>("JemPreviewRed");
            }
            if (JColor == "green")
            {
                jSprite = Resources.Load<Sprite>("JemPreviewGreen");
            }
            if (JColor == "teal")
            {
                jSprite = Resources.Load<Sprite>("JemPreviewTeal");
            }
            if (JColor == "pink")
            {
                jSprite = Resources.Load<Sprite>("JemPreviewPink");
            }
            if (JColor == "lime")
            {
                jSprite = Resources.Load<Sprite>("JemPreviewLime");
            }
            if (JColor == "yellow")
            {
                jSprite = Resources.Load<Sprite>("JemPreviewYellow");
            }
            if (JColor == "orange")
            {
                jSprite = Resources.Load<Sprite>("JemPreviewOrange");
            }
            if (JColor == "purple")
            {
                jSprite = Resources.Load<Sprite>("JemPreviewPurple");
            }
            transform.GetChild(0).GetComponent<Image>().sprite = jSprite;
            jCost = CGameManager.jManager.CheckJorbleCost(newColor);
            costText.text = "COST: " + jCost.ToString();
            jOwned = CGameManager.jManager.CheckJorblesInCollection(newColor);
            ownedText.text = "OWN: " + jOwned.ToString();
            jMax = CGameManager.jManager.CheckMaxJorbles(newColor);
            maxText.text = "MAX: " + jMax.ToString();
        }
    }

    public void PurchaseJorble()
    {
        if (JColor != null)
        {
            gmScript.AddNewJorbleToCollection(JColor);
        }
        ActivateWindow(JColor);
    }
}
