using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour {

    public GameObject card;
    public List<GameObject> deck;
    public char muestra;
    public List<PlayerScript> players;
    public List<int> rounds;
    public int actualRound;
    public CenterScript center;

    private Vector3 deckPosition = new Vector3(-3, 3, 0);


    

    void InitializeDeck() // Crear la baraja
    {
        for (int i=1;i<=12;i++) // Bastos
        {
            if (i!=8 && i!=9)
            {
                GameObject auxCard = Instantiate(card, deckPosition, Quaternion.identity);
                string spritePath = "bastos_" + i.ToString();
                auxCard.name = spritePath;
                //Debug.Log(spritePath);
                Sprite auxSprite = Resources.Load<Sprite>(spritePath);
                if (auxSprite == null) Debug.Log("Spriteeee");
                auxCard.GetComponent<CardScript>().CreateCard(auxSprite, i, 'b');
                deck.Add(auxCard);
            }
            
        }

        for (int i = 1; i <= 12; i++) // Copas
        {
            if (i != 8 && i != 9)
            {
                GameObject auxCard = Instantiate(card, deckPosition, Quaternion.identity);
                string spritePath = "copas_" + i.ToString();
                auxCard.name = spritePath;
                //Debug.Log(spritePath);
                Sprite auxSprite = Resources.Load<Sprite>(spritePath);
                if (auxSprite == null) Debug.Log("Spriteeee");
                auxCard.GetComponent<CardScript>().CreateCard(auxSprite, i, 'c');
                deck.Add(auxCard);
            }

        }

        for (int i = 1; i <= 12; i++) // Espadas
        {
            if (i != 8 && i != 9)
            {
                GameObject auxCard = Instantiate(card, deckPosition, Quaternion.identity);
                string spritePath = "espadas_" + i.ToString();
                auxCard.name = spritePath;
                //Debug.Log(spritePath);
                Sprite auxSprite = Resources.Load<Sprite>(spritePath);
                if (auxSprite == null) Debug.Log("Spriteeee");
                auxCard.GetComponent<CardScript>().CreateCard(auxSprite, i, 'e');
                deck.Add(auxCard);
            }

        }

        for (int i = 1; i <= 12; i++) // Oros
        {
            if (i != 8 && i != 9)
            {
                GameObject auxCard = Instantiate(card, deckPosition, Quaternion.identity);
                string spritePath = "oros_" + i.ToString();
                auxCard.name = spritePath;
                //Debug.Log(spritePath);
                Sprite auxSprite = Resources.Load<Sprite>(spritePath);
                if (auxSprite == null) Debug.Log("Spriteeee");
                auxCard.GetComponent<CardScript>().CreateCard(auxSprite, i, 'o');
                deck.Add(auxCard);
            }

        }
    }

    void RepartirRonda()
    {
        List<int> cardsGiven = new List<int>(40);
        GameObject auxCard = null;
        bool aux = true;
        int numCard = 0;

        for (int i=0;i<players.Count;i++)
        {
            //Debug.Log("i = " + i);
            //Debug.Log("Ronda Actual = " + rounds[actualRound]);
            for (int j=0;j<2;j++)
            {
                aux = true;
                while(aux)
                {
                    numCard = Random.Range(0, 40);
                    if (!cardsGiven.Contains(numCard))
                    {
                        aux = false;
                    }
                }
                auxCard = deck[numCard];
                auxCard.GetComponent<CardScript>().SetPlayer(i);
                players[i].AddCard(deck[numCard]);
                cardsGiven.Add(numCard);
            }
        }
        // Poner muestra
        
        if (cardsGiven.Count == 40)
        {
            muestra = deck[cardsGiven[9]].GetComponent<CardScript>().GetPalo();
        }
        else
        {
            while (aux)
            {
                numCard = Random.Range(0, 40);
                if (!cardsGiven.Contains(numCard))
                {
                    aux = false;
                }
            }
            auxCard = deck[numCard];
            muestra = auxCard.GetComponent<CardScript>().GetPalo();
        }
        center.ReceiveMuestra(muestra);
        foreach (PlayerScript player in players) player.OrderCards();
    }


    private void Awake()
    {
        // Players
        PlayerScript[] auxPlayers = GetComponentsInChildren<PlayerScript>();
        for (int i = 0; i < 4; i++) players.Add(auxPlayers[i]);

        //
        // rondas = new int[(40 / players.Count)+(players.Count - 1)];
        for (int i = 0; i < players.Count; i++) rounds.Add(1);
        for (int i = 2; i <= (40/players.Count);i++)
        {
            rounds.Add(i);
        }
        actualRound = 0;

        //
        center = GetComponentInChildren<CenterScript>();
        center.AddPlayers(players);

    }
    // Use this for initialization
    void Start () {
        InitializeDeck();
        RepartirRonda();
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
