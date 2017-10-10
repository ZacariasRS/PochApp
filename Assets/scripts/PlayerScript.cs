using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public List<GameObject> cards; // las cartas que posee el jugador
    public bool imPlayer; // soy un jugador, sprite de carta o back
    public bool myTurn; // indica si es su turno

	// Use this for initialization
	void Start () {
        //myTurn = false;
	}
	
	// Update is called once per frame
	void Update () {

        //OrderCards();
		if (!myTurn)
        {
            foreach (GameObject card in cards)
            {
                card.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            foreach (GameObject card in cards)
            {
                card.GetComponent<BoxCollider2D>().enabled = true; // TODO: hacerlo en Stop/StartTurn??
            }
        }
	}

    public void AddCard(GameObject card)
    {
        cards.Add(card);
        card.transform.position = this.transform.position;
        card.transform.rotation = this.transform.rotation;
        card.GetComponent<CardScript>().ActivateCard(imPlayer);
    }

    public void RemoveCard(GameObject card)
    {
        cards.Remove(card);
    }

    public void OrderCards()
    {
        if (cards.Count > 0)
        {
            float auxXY = -(cards.Count / 2.0f);
            float auxZ = 1;
            foreach (GameObject card in cards)
            {
                if (card.transform.position.x == 0)
                {
                    Vector3 v = new Vector3(auxXY, 0, auxZ);
                    card.transform.position += v;
                }
                else if (card.transform.position.y == 0)
                {
                    Vector3 v = new Vector3(0, auxXY, auxZ);
                    card.transform.position += v;
                }
                auxXY += 1.0f;
                auxZ -= 0.1f;
            }
        }
    }

    public void StopTurn()
    {
        Debug.Log(this.name + " StopTurn");
        myTurn = false;
    }

    public void StartTurn()
    {
        Debug.Log(this.name + " Start Turn");
        myTurn = true;
    }

    public bool HasPalo(char p)
    {
        bool res = false;
        Debug.Log("Cards count: " + cards.Count);
        for (int i = 0; i < cards.Count; i++) // TODO: parar bucle antes
        {
            Debug.Log("i: " + i);
            CardScript auxCard = cards[i].GetComponent<CardScript>();
            if (auxCard.GetPalo() == p)
            {
                res = true;
            }
        }
        return res;
    }

    public bool HasHigherPaloRank(char p, int r)
    {
        bool res = false;
        for (int i = 0; i < cards.Count; i++) // TODO: parar bucle antes
        {
            CardScript auxCard = cards[i].GetComponent<CardScript>();
            if(auxCard.GetPalo() == p && auxCard.GetRank() > r)
            {
                res = true;
            }
        }
        return res;
    }


}
