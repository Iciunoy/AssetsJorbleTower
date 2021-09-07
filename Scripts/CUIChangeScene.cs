using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CUIChangeScene : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeScene(string sceneName)
    {
        if (sceneName == "SMenu")
        {

        }
        if (sceneName == "STower")
        {
            int jNumber = CGameManager.gameManager.CheckJorblesInDeck();
            if (jNumber < 1)
            {
                Debug.Log("Not enough jorbles to play!");
                return;
            }
            CGameManager.gameManager.DealNewDeckInstance();
        }
        SceneManager.LoadScene(sceneName);
    }
}
