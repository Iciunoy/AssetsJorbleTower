using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIBuyJorble : MonoBehaviour
{

    public string JColor;
    public Sprite jSprite;
    private int jCost;
    private int jOwned;
    private int jMax;
    private Text costText;
    private Text ownedText;
    private Text maxText;
    private CGameManager gmScript;
    private Button myselfButton;
    private GameObject purchaseWindow;

    // Use this for initialization
    void Start ()
    {
        gmScript = CGameManager.gameManager;
        purchaseWindow = GameObject.FindGameObjectWithTag("PurchaseWindow");
        myselfButton = GetComponent<Button>();
        //myselfButton.onClick.AddListener(() => { purchaseWindow.GetComponent<CUIPurchase>().ActivateWindow(JColor); });

        costText = transform.GetChild(2).gameObject.GetComponent<Text>();
        jCost = CGameManager.jManager.CheckJorbleCost(JColor);
        costText.text = "COST: " + jCost.ToString();

        //RELOCATED TO POPUP
        //ownedText = transform.GetChild(3).gameObject.GetComponent<Text>();
        //jOwned = CGameManager.jManager.CheckJorblesInCollection(JColor);
        //ownedText.text = "OWN: " + jOwned.ToString();

        //maxText = transform.GetChild(4).gameObject.GetComponent<Text>();
        //jMax = CGameManager.jManager.CheckMaxJorbles(JColor);
        //maxText.text = "MAX: " + jMax.ToString();

        if (jSprite != null)
        {
            transform.Find("Image").GetComponent<Image>().sprite = jSprite;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		//RELOCATED TO POPUP
        //if (jOwned != CGameManager.jManager.CheckJorblesInCollection(JColor))
  //      {
  //          jOwned = CGameManager.jManager.CheckJorblesInCollection(JColor);
  //          ownedText.text = "OWN: " + jOwned.ToString();
  //      }
	}
}
