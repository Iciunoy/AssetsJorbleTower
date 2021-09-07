using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIPointsAndHeight : MonoBehaviour
{
    public enum PointsOrHeight
    {
        Points,
        Height
    };
    public PointsOrHeight pointsOrHeight;
    private Text txtRef;
    private int textValue;

    // Use this for initialization
    void Start ()
    {
        txtRef = GetComponent<Text>();
        if (pointsOrHeight == PointsOrHeight.Points)
        {
            textValue = CGameManager.gameManager.CurrentPoints;
            txtRef.text = "Points: " + textValue.ToString();
        }
        if (pointsOrHeight == PointsOrHeight.Height)
        {
            textValue = CGameManager.gameManager.RecordHeight;
            txtRef.text = "Record Height: " + textValue.ToString();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (pointsOrHeight == PointsOrHeight.Points)
        {
            textValue = CGameManager.gameManager.CurrentPoints;
            txtRef.text = "Points: " + textValue.ToString();
        }
        if (pointsOrHeight == PointsOrHeight.Height)
        {
            textValue = CGameManager.gameManager.RecordHeight;
            txtRef.text = "Record Height: " + textValue.ToString();
        }
    }
}
