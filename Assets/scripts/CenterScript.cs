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
        for (int i = 0; i < players.Count; i++) cards.Add(null);
        auxCards = 0;
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
                Debug.Log("Collision!");
                CardScript card = collision.gameObject.GetComponent<CardScript>();
                card.EnterCenter(); // Carta entra al centro

                // No entran 2 cartas a la vez, declaramos paloInicio, las cartas entran en la posicion del vector del jugador
                if (!cards.Contains(card))
                {
                    if (cards.Count == 0)
                    {
                        paloInicio = card.getPalo();
                        cards[card.GetPlayer()] = card;
                    }
                }

                // Quitamos la carta al jugador       
                players[card.GetPlayer()].RemoveCard(collision.gameObject);

                // Terminamos turno y mandamos al siguiente si quedan cartas por jugar
                auxCards++;
                if (auxCards == players.Count)
                {
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
                        Debug.Log(auxPlayer);
                        players[auxPlayer].StopTurn();
                        ++auxPlayer;
                        Debug.Log(auxPlayer);
                        players[auxPlayer].StartTurn();
                    }
                }
            }
        }
        
    }
}
