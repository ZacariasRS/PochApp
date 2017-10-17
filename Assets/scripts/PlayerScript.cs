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

    public void OrderCards() // TODO: Usar PlayerPref("OrderCards") para ordenar por palos
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
                card.GetComponent<CardScript>().SaveInitialPosition();
                auxXY += 1.0f;
                auxZ -= 0.1f;
            }
        }
    }

    public void StopTurn()
    {
        //Debug.Log(this.name + " StopTurn");
        foreach (GameObject card in cards)
        {
            card.GetComponent<BoxCollider2D>().enabled = false;
            card.GetComponent<CardScript>().SetGreyMaterial();
        }
        myTurn = false;
    }

    public void StartTurn()
    {
        //Debug.Log(this.name + " Start Turn");
        foreach (GameObject card in cards)
        {
            card.GetComponent<BoxCollider2D>().enabled = true; // TODO: hacerlo en Stop/StartTurn??
            card.GetComponent<CardScript>().SetDefMaterial();
        }
        myTurn = true;
    }

    public bool HasPalo(char p)
    {
        bool res = false;
        //Debug.Log("Cards count: " + cards.Count);
        for (int i = 0; i < cards.Count; i++) // TODO: parar bucle antes
        {
            //Debug.Log("i: " + i);
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

    public void PlayCard(char pi, int hr, bool hm, int rm, char m)
    {
        if (!imPlayer && myTurn)
        {
            GameObject cardToPlay = null;
            if (cards.Count > 0)
            {
                if (HasPalo(pi)) // Si tengo del palo inicial
                {
                    for (int i = 0; i < cards.Count; i++)
                    {
                        if (cards[i].GetComponent<CardScript>().GetPalo() == pi) // busco en las cartas
                        {
                            if (cardToPlay == null) // si aun no he elegido ninguna, la primera del palo
                            {
                                cardToPlay = cards[i];
                            }
                            else // ya habia escogido una
                            {
                                if (cardToPlay.GetComponent<CardScript>().GetRank() < cards[i].GetComponent<CardScript>().GetRank()) // si es mas grande, actualiza
                                {
                                    cardToPlay = cards[i];
                                }
                            }
                        }
                    }
                }
                else // no tengo del palo inicial
                {
                    if (HasPalo(m)) // si tengo muestra, tiro la mas grande
                    {
                        for (int i = 0; i < cards.Count; i++)
                        {
                            if (cards[i].GetComponent<CardScript>().GetPalo() == m) // busco en las cartas
                            {
                                if (cardToPlay == null) // si aun no he elegido ninguna, la primera del palo
                                {
                                    cardToPlay = cards[i];
                                }
                                else // ya habia escogido una
                                {
                                    if (cardToPlay.GetComponent<CardScript>().GetRank() < cards[i].GetComponent<CardScript>().GetRank()) // si es mas grande, actualiza
                                    {
                                        cardToPlay = cards[i];
                                    }
                                }
                            }
                        }
                    }
                    else // si no tengo inicial ni muestra, tiro la primera
                    {
                        cardToPlay = cards[0];
                    }
                }
            }
            if (cardToPlay == null)
            {
                Debug.Log("El bot la ha liado");
            }
            else
            {
                cardToPlay.transform.position = new Vector3(0f, 0f, -3f);
            }
        }
    }

    public int Bet(char m)
    {
        int bet = 0;
        if (cards.Count == 1 && myTurn)
        {
            return 1;
        }
        if (cards.Count == 1 && !myTurn)
        {
            if (cards[0].GetComponent<CardScript>().GetPalo() == m) return 1;
            else return 0;
        }
        if (cards.Count > 1)
        {
            foreach (GameObject card in cards)
            {
                if (card.GetComponent<CardScript>().GetPalo() == m)
                {
                    bet++;
                }
            }
        }
        return bet;
    }

    public void SetAllCardGrey()
    {
        foreach (GameObject card in cards)
        {
            card.GetComponent<CardScript>().SetGreyMaterial();
        }
    }

    public void SetAllCardNormal()
    {
        foreach (GameObject card in cards)
        {
            card.GetComponent<CardScript>().SetDefMaterial();
        }
    }
}
