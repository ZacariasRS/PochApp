using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterScript : MonoBehaviour {


    public List<CardScript> cards; // cartas del centro
    public List<PlayerScript> players; // los jugadores
    public char paloInicio; // variable que guarda el palo con el q se empieza la jugada
    public char muestra; // valor de la muestra
    public int actualRound; // valor de la ronda actual
    public int numCards;
    public int numTurns;
    public bool revisarJugada; // controlar si el centro debe ser revisado
    public int auxCards; // cartas jugadas
    public bool pasarTurno; // variable para saber si pasa turno al siguiente jugador
    public int higherRank; // valor de rango mas alto en mesa
    public bool hayMuestra; // si hay muestra en mesa
    public int rankMuestra; // el rango de la muestra en mesa
    public int firstPlayer; // el index del jugador que empieza la jugada
    public bool nextRound; // se pide a board que reparta la siguiente ronda

    public void AddPlayers(List<PlayerScript> p)
    {
        players = p;
    }

    public void ReceiveMuestra(char m)
    {
        muestra = m;
    }

    public void ReceiveRonda(int r, int nc)
    {
        actualRound = r;
        numCards = nc;
    }
    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start () {
        paloInicio = 'n';
        revisarJugada = false;
        cards = new List<CardScript>(players.Count);
        pasarTurno = false;
        nextRound = false;
        numTurns = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (!nextRound)
        {
            if (revisarJugada)
            {
                //Debug.Log("revisaJugada");
                int nextPlayer = RevisarJugada(firstPlayer);
                revisarJugada = false;
                //Debug.Log("numTurns, numCards" + numTurns + ", " + numCards);
                if (numTurns != numCards) players[nextPlayer].StartTurn();
                if (numTurns == numCards) nextRound = true;
            }
        } else
        {
            Debug.Log("Ronda over");
        }
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!revisarJugada)
        {
            if (auxCards < players.Count)
            {
                //Debug.Log("Collision!");
                CardScript card = collision.gameObject.GetComponent<CardScript>();
                int cardPlayer = card.GetPlayer();
                int cardRank = card.GetRank();
                char cardPalo = card.GetPalo();
                //card.EnterCenter(); // Carta entra al centro

                // No entran 2 cartas a la vez, declaramos paloInicio
                if (!cards.Contains(card))
                {
                    if (cards.Count == 0) // Es la primera carta, se puede jugar lo que sea
                    {
                        Debug.Log("Primera carta");
                        paloInicio = cardPalo;
                        higherRank = cardRank;
                        //if (card.GetPalo() == muestra) hayMuestra = true;
                        firstPlayer = cardPlayer;
                        card.EnterCenter();
                        pasarTurno = true;
                        cards.Add(card);
                    }
                    else // No es la primera carta, introducir logica de juego de la pocha
                    {
                        //Debug.Log("No primera carta");
                        if (players[cardPlayer].HasPalo(paloInicio)) // Si tienes del palo de inicio
                        {
                            //Debug.Log("Tiene palo inicio");
                            if (cardPalo == paloInicio)
                            {
                                //Debug.Log("Tiene palo inicio y lo juega");
                                if (cardRank > higherRank) // si la carta es de mayor rango que todas
                                {
                                    //Debug.Log("Tiene palo inicio y la carta es mayor que la actual o hay muestra");
                                    card.EnterCenter();
                                    higherRank = cardRank;
                                    pasarTurno = true;
                                    cards.Add(card);
                                }
                                else // si la carta es menor 
                                {
                                    //Debug.Log("Tiene palo inicio y la carta es menor que la actual");
                                    if (players[cardPlayer].HasHigherPaloRank(cardPalo, higherRank)) // si tiene una que podria ser mayor
                                    {
                                        if (!hayMuestra)
                                        {
                                            //Debug.Log("Tiene una carta mas grande y no hay muestra en mesa");
                                            card.ReturnToInitialPosition();
                                        } else
                                        {
                                            //Debug.Log("Tiene una carta mas grande pero hay muestra en la mesa");
                                            card.EnterCenter();
                                            pasarTurno = true;
                                            cards.Add(card);
                                        }
                                        
                                    }
                                    else
                                    {
                                        //Debug.Log("No tiene una carta mas grande");
                                        card.EnterCenter();
                                        pasarTurno = true;
                                        cards.Add(card);
                                    }
                                }
                            } else
                            {
                                //Debug.Log("Tiene palo inicio y juega otro palo");
                                card.ReturnToInitialPosition();
                            }
                        } else // No tiene palo de inicio
                        {
                            //Debug.Log("No tiene palo inicio");
                            if (players[cardPlayer].HasPalo(muestra)) // si tiene muestra
                            {
                                //Debug.Log("Tiene muestra");
                                if (hayMuestra) // Si hay muestra ya en la mesa
                                {
                                    if (players[cardPlayer].HasHigherPaloRank(muestra, rankMuestra))
                                    {
                                        //Debug.Log("Tiene muestra y hay muestra en mesa y tenemos una mas grande que la actual");
                                        if (cardPalo == muestra)
                                        {
                                            if (cardRank > rankMuestra)
                                            {
                                                //Debug.Log("Tiene muestra y hay muestra en mesa y tenemos una mas grande y la jugamos");
                                                card.EnterCenter();
                                                pasarTurno = true;
                                                rankMuestra = cardRank;
                                                cards.Add(card);
                                            } else
                                            {
                                                //Debug.Log("Tiene muestra y hay muestra en mesa y tenemos una mas grande y no la jugamos");
                                                card.ReturnToInitialPosition();
                                            }
                                        } else
                                        {
                                            //Debug.Log("Tiene muestra y hay muestra en mesa y tenemos una mas grande y no juega muestra");
                                            card.ReturnToInitialPosition();
                                        }
                                    } else
                                    {
                                        //Debug.Log("Tiene muestra y hay muestra en mesa y no tiene una mas grande");
                                        card.EnterCenter();
                                        pasarTurno = true;
                                        cards.Add(card);
                                    }
                                } else // No hay muestra en la mesa
                                {
                                    //Debug.Log("No hay muestra en la mesa y tenemos muestra");
                                    if (cardPalo == muestra)
                                    {
                                        //Debug.Log("Tiene muestra y No hay muestra en la mesa y tenemos muestra y la carta es muestra");
                                        card.EnterCenter();
                                        pasarTurno = true;
                                        hayMuestra = true;
                                        rankMuestra = cardRank;
                                        cards.Add(card);
                                    } else
                                    {
                                        //Debug.Log("No hay muestra en la mesa tenemos muestra y la carta no es muestra");
                                        card.ReturnToInitialPosition();
                                    }
                                }
                            } else // No tiene palo inicio ni muestra, puede echar cualquiera
                            {
                                //Debug.Log("No tiene palo inicio y no tiene muestra");
                                card.EnterCenter();
                                pasarTurno = true;
                                cards.Add(card);
                            }
                        }
                    }
                }
                if (pasarTurno)
                {
                    // Quitamos la carta al jugador       
                    players[card.GetPlayer()].RemoveCard(collision.gameObject);

                    // Terminamos turno y mandamos al siguiente si quedan cartas por jugar
                    auxCards++;
                    if (auxCards == players.Count)
                    {
                        players[card.GetPlayer()].StopTurn();
                        auxCards = 0;
                        revisarJugada = true;
                    }
                    else
                    {
                        int auxPlayer = card.GetPlayer();
                        if (auxPlayer == (players.Count - 1))
                        {
                            players[auxPlayer].StopTurn();
                            players[0].StartTurn();
                        }
                        else
                        {
                            //Debug.Log(auxPlayer);
                            players[auxPlayer].StopTurn();
                            ++auxPlayer;
                            //Debug.Log(auxPlayer);
                            players[auxPlayer].StartTurn();
                        }
                    }
                    pasarTurno = false;
                }   
            }
        }
    }

    public int RevisarJugada(int fp) 
    {
        GetComponent<BoxCollider2D>().enabled = false;
        int playerWinner = 0;
        int bestRank = 0;
        bool hasMuestra = false;
        for (int i = 0; i<cards.Capacity; i++)
        {
            int index = (fp + i) % 4; // index del jugador apropiado
            Debug.Log("index: " + index);
            int auxRank = cards[i].GetRank();
            if (hasMuestra)
            {
                if (cards[i].GetPalo() == muestra && auxRank > bestRank)
                {
                    bestRank = auxRank;
                    playerWinner = index;
                }
            } else
            {
                if (cards[i].GetPalo() == muestra)
                {
                    playerWinner = index;
                    bestRank = auxRank;
                    hasMuestra = true;
                } else
                {
                    if (auxRank > bestRank && cards[i].GetPalo() == paloInicio)
                    {
                        bestRank = auxRank;
                        playerWinner = index;
                    }
                }
            }
            cards[i].HideCard();
        }
        Debug.Log("PlayerWinner: " + playerWinner);
        ScoreBoard.GetInstance().IncrementRoundsWonRoundPlayer(actualRound, playerWinner); // TODO: Usar esto para hacer el score
        numTurns++;
        cards.Clear();
        GetComponent<BoxCollider2D>().enabled = true;
        return playerWinner;
    }

    public void ResetValues()
    {
        cards.Clear();
        paloInicio = 'n'; // TODO: ¿Es necesario?
        revisarJugada = false;
        auxCards = 0;
        higherRank = 0; // TODO: ¿Es necesario?
        hayMuestra = false; // TODO: ¿Es necesario?
        rankMuestra = 0; // TODO: ¿Es necesario?
        numTurns = 0; // TODO: ¿Es necesario?
        nextRound = false;
    }

    public bool NeedNextRound()
    {
        return nextRound;
    }
}
