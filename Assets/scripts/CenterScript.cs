using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterScript : MonoBehaviour {


    public List<GameObject> cards;
    public List<PlayerScript> players;
    public char paloInicio;
    public char muestra;


    public void AddPlayers(List<PlayerScript> p)
    {
        players = p;
    }

    public void ReceiveMuestra(char m)
    {
        muestra = m;
    }

    private void Awake()
    {
        cards = new List<GameObject>(4);
        for (int i = 0; i < 4; i++) cards.Add(null);
    }
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		if(cards.Count == 4)
        {

        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision!");
        collision.gameObject.GetComponent<CardScript>().EnterCenter();
        if (!cards.Contains(collision.gameObject))
        {
            if (cards.Count == 0) paloInicio = collision.gameObject.GetComponent<CardScript>().getPalo();
            cards[collision.gameObject.GetComponent<CardScript>().GetPlayer()] = collision.gameObject;
        }
        foreach (PlayerScript player in players)
        {
            player.RemoveCard(collision.gameObject);
        }
        //Destroy(collision.gameObject);
    }

}
