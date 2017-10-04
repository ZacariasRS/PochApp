using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour {

    List<GameObject> deck;
    char muestra;
    List<PlayerScript> players;

    void InitializeDeck()
    {
        for (int i=1;i<12;i++)
        {
            var auxCard = Resources.Load<GameObject>("prefabs/Card");
            string spritePath = "sprites/bastos_" + i.ToString();
            auxCard.GetComponent<CardScript>().CreateCard(Resources.Load<Sprite>(spritePath), i, 'b');
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
