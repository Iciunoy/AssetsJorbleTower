using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIHeightText : MonoBehaviour
{
    int height = 0;
    private Text txtRef;
    private CGameManager gm;

    // Use this for initialization
    void Start ()
    {
        txtRef = GetComponent<Text>();
        gm = CGameManager.gameManager;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (height != gm.RecordHeight)
        {
            height = gm.RecordHeight;
            string remaining = gm.RemainingJorbles.ToString();
            txtRef.text = "Height: " + height.ToString() + "\n" + "Remaing Jorbles: " + remaining;
            if (remaining == "0")
            {
                txtRef.text = "Height: " + height.ToString();
            }
        }
	}
}
