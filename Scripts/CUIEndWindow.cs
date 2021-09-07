using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIEndWindow : MonoBehaviour
{
    private bool isWindowActive;
    private RectTransform rTransform;
    private CGameManager gmScript;
    private bool previousState;

    // Use this for initialization
    void Start ()
    {
        gmScript = CGameManager.gameManager;
        isWindowActive = false;
        rTransform = gameObject.GetComponent<RectTransform>();
        rTransform.localScale = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (isWindowActive)
        {
            if (Input.GetMouseButtonDown(0) && !previousState)
            {
                CGameManager.gameManager.SaveGame();
                ActivateWindow(true);
                GameObject.FindGameObjectWithTag("Manager").GetComponent<CUIChangeScene>().ChangeScene("SMenu");
            }
        }
        previousState = Input.GetMouseButton(0);
	}

    public void ActivateWindow(bool bombHit)
    {
        
        if (isWindowActive)
        {
            isWindowActive = false;
            rTransform.localScale = Vector3.zero;
        }
        else
        {
            isWindowActive = true;
            previousState = true;
            rTransform.localScale = Vector3.one;
            if (bombHit)
            {
                transform.GetChild(0).gameObject.GetComponent<Text>().text = "FAILURE";
            }
            if (!bombHit)
            {
                transform.GetChild(0).gameObject.GetComponent<Text>().text = "SUCCESS";
            }
        }
    }
}
