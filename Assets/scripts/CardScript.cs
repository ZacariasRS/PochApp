using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour {

    int rank;
    char palo;
    SpriteRenderer mySpriteRenderer;

	// Use this for initialization
	void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateCard(Sprite s, int r, char p)
    {
        if (mySpriteRenderer != null)
        {
            mySpriteRenderer.sprite = s;
        }
        rank = r;
        palo = p;
    }
}
