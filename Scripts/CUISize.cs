using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUISize : MonoBehaviour
{
    public float sizeX;
    public float sizeY;
    public float offsetX;
    public float offsetY;

    // Use this for initialization
    void Start ()
    {
        float newSizeX = (sizeX * Screen.width);
        float newSizeY = (sizeY * Screen.height);
        float newOffsetX = (offsetX * Screen.width);
        float newOffsetY = (offsetY * Screen.height);
        RectTransform thisRect = gameObject.GetComponent<RectTransform>();
        Vector3 offsetPosition = new Vector3((int)newOffsetX, (int)newOffsetY, 0f);
        thisRect.anchoredPosition3D = offsetPosition;
        Vector2 scaledSize = new Vector2((int)newSizeX, (int)newSizeY);
        thisRect.sizeDelta = scaledSize;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
