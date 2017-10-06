using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public List<GameObject> cards;
    public bool imPlayer;
    public bool myTurn;

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
            float auxZ = 0;
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
                auxZ += 0.1f;
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


}
