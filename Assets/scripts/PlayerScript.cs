using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public List<GameObject> cards; // las cartas que posee el jugador
    public bool imPlayer; // soy un jugador, sprite de carta o back
    public bool myTurn; // indica si es su turno
    public int betActualRound; // la apuesta este turno
    public int numCardsActualRound; // el numero de cartas de este turno

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

    public void OrderCards(char m) // TODO: Usar PlayerPref("OrderCards") para ordenar por palos
    {
        if (cards.Count > 0)
        {
            numCardsActualRound = cards.Count;
            float auxXY = -(cards.Count / 2.0f);
            float auxZ = 1;
            char[] palos = null;
            List<List<GameObject>> listaAux = new List<List<GameObject>>();
            switch (m)
            {
                case 'b':
                    palos = new char[] { 'b', 'c', 'e', 'o' };
                    break;
                case 'c':
                    palos = new char[] { 'c', 'b', 'e', 'o' };
                    break;
                case 'e':
                    palos = new char[] { 'e', 'b', 'c', 'o' };
                    break;
                case 'o':
                    palos = new char[] { 'o', 'b', 'c', 'e' };
                    break;
                default:
                    Debug.Log("OrderCards(m): nunca llegar aqui");
                    break;
            }
            if (palos != null)
            {
                for (int i = 0; i < palos.Length; i++)
                {
                    listaAux.Add(new List<GameObject>());
                    foreach (GameObject card in cards)
                    {
                        if (card.GetComponent<CardScript>().GetPalo() == palos[i])
                        {
                            listaAux[i].Add(card);
                        }
                    }
                } // tenemos aislados los palos
                //Debug.Log("Tenemos aislados los palos");
                foreach (List<GameObject> listaPalo in listaAux)
                {
                    for (int i = 0; i < listaPalo.Count; i++) // hasta listaPalo.Count - 1?
                    {
                        for (int j = 0; j < (listaPalo.Count - 1); j++)
                        {
                            if (listaPalo[j].GetComponent<CardScript>().GetRank() < listaPalo[j + 1].GetComponent<CardScript>().GetRank())
                            {
                                GameObject temp = listaPalo[j];
                                listaPalo[j] = listaPalo[j + 1];
                                listaPalo[j + 1] = temp;
                            }
                        }
                    }
                } // los palos aislados estan ordenados
                //Debug.Log("Los palos aislados estan ordenados");
                List<GameObject> final = new List<GameObject>();
                for (int i = 0; i < listaAux.Count; i++)
                {
                    for (int j = 0; j < listaAux[i].Count; j++)
                    {
                        final.Add(listaAux[i][j]);
                    }
                }
                cards = final;
                //Debug.Log("Metidos en la lista final");
                // a dibujar
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
            else Debug.Log("OrderCards(m): Palos es null");
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

    /* pi = palo inicial
     * hr = higher rank
     * hm = hay muestra
     * rm = rank muestra
     * m = muestra
     * rw = roundsWon
     * De la clase a tener en cuenta
     * betActualRound = la apuesta en esta ronda
     * numCardsActualRound = el numero de cartas de la ronda
     */
    public void PlayCard(char pi, int hr, bool hm, int rm, char m, int rw)
    {
        if (!imPlayer && myTurn)
        {
            GameObject cardToPlay = null;
            if (cards.Count > 0)
            {
                if (pi != 'n')
                {
                    if (HasPalo(pi)) // Si tengo del palo inicial
                    {
                        if (HasHigherPaloRank(pi, hr)) // Si tengo carta mas grande, la mas grande de las que tenga
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
                        else // no tengo mas grande, la mas pequeña
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
                                        if (cardToPlay.GetComponent<CardScript>().GetRank() > cards[i].GetComponent<CardScript>().GetRank()) // si es mas grande, actualiza
                                        {
                                            cardToPlay = cards[i];
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else // no tengo del palo inicial
                    {
                        if (HasPalo(m)) // si tengo muestra, tiro la mas grande
                        {
                            if (hm) // si hay muestra, busco si tengo una mas grande
                            {
                                if (HasHigherPaloRank(m, rm)) // si tengo una mas grande, tiro la mas grande que tenga
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
                                else // no tengo muestra mas grande, tiro la mas pequeña de cualquier palo QUE NO SEA MUESTRA
                                {
                                    for (int i = 0; i < cards.Count; i++)
                                    {
                                        if (cardToPlay == null) // si aun no he elegido ninguna, la primera que no sea muestra
                                        {
                                            if (cards[i].GetComponent<CardScript>().GetPalo() != m) cardToPlay = cards[i];
                                        }
                                        else // ya habia escogido una
                                        {
                                            if (cards[i].GetComponent<CardScript>().GetPalo() != m && cardToPlay.GetComponent<CardScript>().GetRank() > cards[i].GetComponent<CardScript>().GetRank()) // si es mas grande, actualiza
                                            {
                                                cardToPlay = cards[i];
                                            }
                                        }
                                    }

                                    if (cardToPlay == null) // Tienes TODO muestras, pero ninguna mas grande...
                                    {
                                        for (int i = 0; i < cards.Count; i++)
                                        {
                                            if (cardToPlay == null) // si aun no he elegido ninguna, la primera 
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
                            }
                            else
                            {
                                for (int i = 0; i < cards.Count; i++)
                                {
                                    if (cardToPlay == null)
                                    {
                                        if (cards[i].GetComponent<CardScript>().GetPalo() == m) cardToPlay = cards[i];
                                    }
                                    else
                                    {
                                        if (cards[i].GetComponent<CardScript>().GetPalo() == m && cardToPlay.GetComponent<CardScript>().GetRank() < cards[i].GetComponent<CardScript>().GetRank())
                                        {
                                            cardToPlay = cards[i];
                                        }
                                    }
                                }
                            }
                        }
                        else // si no tengo inicial ni muestra, tiro la mas pequeña de cualquier palo
                        {
                            for (int i = 0; i < cards.Count; i++)
                            {
                                if (cardToPlay == null) // si aun no he elegido ninguna, la primera 
                                {
                                    cardToPlay = cards[i];
                                }
                                else // ya habia escogido una
                                {
                                    if (cardToPlay.GetComponent<CardScript>().GetRank() > cards[i].GetComponent<CardScript>().GetRank()) // si es mas grande, actualiza
                                    {
                                        cardToPlay = cards[i];
                                    }
                                }
                            }
                        }
                    }
                } else // soy el primero en jugar
                {
                    if (cards.Count == 1) // si solo tengo una carta...
                    {
                        cardToPlay = cards[0];
                    }
                    else // mas de 1 carta, la mas grande QUE NO SEA MUESTRA (en principio)
                    {
                        for (int i = 0; i < cards.Count; i++)
                        {
                            if (cardToPlay == null)
                            {
                                if (cards[i].GetComponent<CardScript>().GetPalo() != m) cardToPlay = cards[i];
                            }
                            else
                            {
                                if (cards[i].GetComponent<CardScript>().GetPalo() != m && cardToPlay.GetComponent<CardScript>().GetRank() < cards[i].GetComponent<CardScript>().GetRank())
                                {
                                    cardToPlay = cards[i];
                                }
                            }
                        }

                        if (cardToPlay == null) // tienes todo muestras...
                        {
                            if (rw < betActualRound) // aun tengo que ganar
                            {
                                for (int i = 0; i < cards.Count; i++)
                                {
                                    if (cardToPlay == null) // si aun no he elegido ninguna, la primera 
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
                            } else
                            {
                                for (int i = 0; i < cards.Count; i++)
                                {
                                    if (cardToPlay == null) // si aun no he elegido ninguna, la primera 
                                    {
                                        cardToPlay = cards[i];
                                    }
                                    else // ya habia escogido una
                                    {
                                        if (cardToPlay.GetComponent<CardScript>().GetRank() > cards[i].GetComponent<CardScript>().GetRank()) // si es mas pequeña, actualiza
                                        {
                                            cardToPlay = cards[i];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (cardToPlay == null)
            {
                Debug.Log("PlayCard: El bot la ha liado");
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
        if (cards.Count == 1 && myTurn) // 1 sola carta y empiezo
        {
            if (cards[0].GetComponent<CardScript>().GetRank() < 5) 
            {
                if (Random.Range(0, 99) < 90) // factor wixifu
                {
                    return 1;
                }
                else return 0;
            }
            else return 1;
        }
        if (cards.Count == 1 && !myTurn) // 1 sola carta y no empiezo
        {
            if (cards[0].GetComponent<CardScript>().GetPalo() == m) return 1;
            else return 0;
        }
        if (cards.Count > 1) // mas de 1 carta
        {
            foreach (GameObject card in cards)
            {
                if (card.GetComponent<CardScript>().GetPalo() == m) // sumo 1 por cada muestra
                {
                    bet++;
                }
                if (cards.Count > 6 && card.GetComponent<CardScript>().GetRank() == 10 && card.GetComponent<CardScript>().GetPalo()!= m) // en las bazas de mas de 6, sumo por cada 1 de palo
                {
                    bet++;
                }
            }
        }
        betActualRound = bet;
        Debug.Log(this.name + " apuesta: " + bet);
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
