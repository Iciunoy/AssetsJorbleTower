using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAnimPlaceableTile : MonoBehaviour
{
    private Color changeAlpha;
    private float aValue;
    private bool increasing;
    //private Transform tParent;

	// Use this for initialization
	void Start ()
    {
        changeAlpha = GetComponent<SpriteRenderer>().color;
        aValue = 0.9f;
        increasing = false;
        //tParent = transform.parent;
        transform.position = transform.parent.position + Vector3.back;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!CGameManager.glowActive)
        {
            CGameManager.glowActive = true;
        }
        Glow(CGameManager.hightlightAlpha);
	}

    private void Glow(float alphaValue)
    {
        changeAlpha.a = alphaValue;
        GetComponent<SpriteRenderer>().color = changeAlpha;
    }
}
