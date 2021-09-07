using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUITextInfo : MonoBehaviour
{
    public enum InfoTextType
    {
        CurrentHeight,
        CurrentPoints,
        RecordHeight,
        JorbleCost,
        JorbleOwned,
        JorbleMax,
        JorbleUsing
    };
    public InfoTextType infoTextType;
    public string jorbleColor;
    private Text txtRef;
    private int textValue;
    private CGameManager gmScript;

    // Use this for initialization
    void Start ()
    {
        txtRef = GetComponent<Text>();
        gmScript = CGameManager.gameManager;
        if (infoTextType == InfoTextType.CurrentHeight)
        {
            textValue = gmScript.CurrentGameHeight;
            txtRef.text = "Height: " + textValue.ToString();
        }
        if (infoTextType == InfoTextType.CurrentPoints)
        {
            textValue = PlayerPrefs.GetInt("Points");
            txtRef.text = "Points: " + textValue.ToString();
        }
        if (infoTextType == InfoTextType.RecordHeight)
        {
            textValue = PlayerPrefs.GetInt("Record Height");
            txtRef.text = "Record Height: " + textValue.ToString();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (infoTextType == InfoTextType.CurrentHeight)
        {
            int value = gmScript.CurrentGameHeight;
            if (textValue != value)
            {
                textValue = value;
                txtRef.text = "Height: " + textValue.ToString();
            }
            
        }
        if (infoTextType == InfoTextType.CurrentPoints)
        {
            int value = gmScript.CurrentPoints;
            if (textValue != value)
            {
                textValue = value;
                txtRef.text = "Points: " + textValue.ToString();
            }
        }
        if (infoTextType == InfoTextType.RecordHeight)
        {
            int value = gmScript.RecordHeight;
            if (textValue != value)
            {
                textValue = value;
                txtRef.text = "Record Height: " + textValue.ToString();
            }
        }
        if (infoTextType == InfoTextType.JorbleCost)
        {
            int value = CGameManager.jManager.CheckJorbleCost(jorbleColor);
            if (textValue != value)
            {
                textValue = value;
                txtRef.text = textValue.ToString();
            }
        }
        if (infoTextType == InfoTextType.JorbleOwned)
        {
            int value = CGameManager.jManager.CheckJorblesInCollection(jorbleColor);
            if (textValue != value)
            {
                textValue = value;
                txtRef.text = textValue.ToString();
            }
        }
        if (infoTextType == InfoTextType.JorbleMax)
        {
            int value = CGameManager.jManager.CheckMaxJorbles(jorbleColor);
            if (textValue != value)
            {
                textValue = value;
                txtRef.text = textValue.ToString();
            }
        }
        if (infoTextType == InfoTextType.JorbleUsing)
        {
            int value = gmScript.CheckJorblesInDeck(jorbleColor);
            if (textValue != value)
            {
                textValue = value;
                txtRef.text = textValue.ToString();
            }
        }
    }
}
