using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAnimSmoke : MonoBehaviour
{
    private Color sAlpha;
    private float lifetime;
    private float spawnTime;
    private CGameManager gmScript;


	// Use this for initialization
	void Start ()
    {
        gmScript = CGameManager.gameManager;
        spawnTime = 1f;
        lifetime = 1.4f;
        //Debug.Log(lifetime);
        sAlpha = gameObject.GetComponent<SpriteRenderer>().color;
        sAlpha.a = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = sAlpha;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (spawnTime <= 0f)
        {
            lifetime -= Time.deltaTime;
            if (lifetime > 1f)
            {
                sAlpha.a = 1f;
            }
            if (lifetime <= 1f)
            {
                sAlpha.a = lifetime;
            }
            gameObject.GetComponent<SpriteRenderer>().color = sAlpha;

            if (lifetime <= 0.1f)
            {
                //Debug.Log("MAKE BOOM");
                Destroy(this.gameObject);
            }
        }
        if (spawnTime > 0f)
        {
            spawnTime -= Time.deltaTime;
        }
    }

    
}
