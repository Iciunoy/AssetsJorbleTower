using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIDeckManager : MonoBehaviour
{

    private CGameManager gmScript;

	// Use this for initialization
	void Start ()
    {
        gmScript = CGameManager.gameManager;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void DeckAdd(string jColor)
    {
        gmScript.AddNewJorbleToDeck(jColor);
    }

    public void DeckRemove(string jColor)
    {
        gmScript.RemoveJorbleFromDeck(jColor);
    }

}
