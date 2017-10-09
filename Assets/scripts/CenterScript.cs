using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterScript : MonoBehaviour {


    public List<CardScript> cards;
    public List<PlayerScript> players;
    public char paloInicio;
    public char muestra;
    public bool revisarJugada;
    public int auxCards;
    public bool pasarTurno;
    public int higherRank;
    public bool hayMuestra;
    public int rankMuestra;


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
        
    }
    // Use this for initialization
    void Start () {
        paloInicio = 'n';
        revisarJugada = false;
        cards = new List<CardScript>(players.Count);
        pasarTurno = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(revisarJugada)
        {
            Debug.Log("revisaJugada");
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
                        card.EnterCenter();
                        pasarTurno = true;
                        cards.Add(card);
                    }
                    else // No es la primera carta, introducir logica de juego de la pocha
                    {
                        Debug.Log("No primera carta");
                        if (players[cardPlayer].HasPalo(paloInicio)) // Si tienes del palo de inicio
                        {
                            Debug.Log("Tiene palo inicio");
                            if (cardPalo == paloInicio)
                            {
                                Debug.Log("Tiene palo inicio y lo juega");
                                if (cardRank > higherRank) // si la carta es de mayor rango que todas
                                {
                                    Debug.Log("Tiene palo inicio y la carta es mayor que la actual");
                                    card.EnterCenter();
                                    higherRank = cardRank;
                                    pasarTurno = true;
                                    cards.Add(card);
                                }
                                else // si la carta es menor 
                                {
                                    Debug.Log("Tiene palo inicio y la carta es menor que la actual");
                                    if (players[cardPlayer].HasHigherPaloRank(cardPalo, higherRank)) // si tiene una que podria ser mayor
                                    {
                                        Debug.Log("Tiene una carta mas grande");
                                        card.ReturnToInitialPosition();
                                    }
                                    else
                                    {
                                        Debug.Log("No tiene una carta mas grande");
                                        card.EnterCenter();
                                        pasarTurno = true;
                                        cards.Add(card);
                                    }
                                }
                            } else
                            {
                                Debug.Log("Tiene palo inicio y juega otro palo");
                                card.ReturnToInitialPosition();
                            }
                        } else // No tiene palo de inicio
                        {
                            Debug.Log("No tiene palo inicio");
                            if (players[cardPlayer].HasPalo(muestra)) // si tiene muestra
                            {
                                Debug.Log("Tiene muestra");
                                if (hayMuestra) // Si hay muestra ya en la mesa
                                {
                                    if (players[cardPlayer].HasHigherPaloRank(muestra, rankMuestra))
                                    {
                                        Debug.Log("Tiene muestra y hay muestra en mesa y tenemos una mas grande que la actual");
                                        if (cardPalo == muestra)
                                        {
                                            if (cardRank > rankMuestra)
                                            {
                                                Debug.Log("Tiene muestra y hay muestra en mesa y tenemos una mas grande y la jugamos");
                                                card.EnterCenter();
                                                pasarTurno = true;
                                                rankMuestra = cardRank;
                                                cards.Add(card);
                                            } else
                                            {
                                                Debug.Log("Tiene muestra y hay muestra en mesa y tenemos una mas grande y no la jugamos");
                                                card.ReturnToInitialPosition();
                                            }
                                        } else
                                        {
                                            Debug.Log("Tiene muestra y hay muestra en mesa y tenemos una mas grande y no juega muestra");
                                            card.ReturnToInitialPosition();
                                        }
                                    } else
                                    {
                                        Debug.Log("Tiene muestra y hay muestra en mesa y no tiene una mas grande");
                                        card.EnterCenter();
                                        pasarTurno = true;
                                        cards.Add(card);
                                    }
                                } else // No hay muestra en la mesa
                                {
                                    Debug.Log("No hay muestra en la mesa y tenemos muestra");
                                    if (cardPalo == muestra)
                                    {
                                        Debug.Log("Tiene muestra y No hay muestra en la mesa y tenemos muestra y la carta es muestra");
                                        card.EnterCenter();
                                        pasarTurno = true;
                                        hayMuestra = true;
                                        rankMuestra = cardRank;
                                        cards.Add(card);
                                    } else
                                    {
                                        Debug.Log("No hay muestra en la mesa tenemos muestra y la carta no es muestra");
                                        card.ReturnToInitialPosition();
                                    }
                                }
                            } else // No tiene palo inicio ni muestra, puede echar cualquiera
                            {
                                Debug.Log("No tiene palo inicio y no tiene muestra");
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

    public void RevisarJugada() 
    {
        /*int cartaGanadora;
        int mejorRango;
        bool esMuestra;*/
        for (int i = 0; i<cards.Capacity; i++)
        {

        }
    }
}
