using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardScript : MonoBehaviour {

    public bool isMultiPlayer;
    public bool isTutorial;

    public GameObject card; // prefab usado para carta
    public List<GameObject> deck; // la baraja entera
    public CardScript muestra; // la muestra
    public List<PlayerScript> players; // los jugadores
    public List<int> rounds; // cuantas cartas se reparten, numero de rondas
    public int actualRound; // la ronda actual
    public CenterScript center; // referencia al centro de mesa
    public int startPlayer;

    public SpriteRenderer muestraSprite;

    public Text scoreD;
    public Text scoreR;
    public Text scoreU;
    public Text scoreL;
    public Text roundsPlayed;
    public Text cardsDealed;
    public Text winnerText;

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
                if (i == 1)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, 10, 'b');
                } else if (i == 3)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, 9, 'b');
                } else if (i == 2)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, 1, 'b');
                } else if (i >=4 && i<=7)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, i-2, 'b');
                } else if (i >=10 && i<=12)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, i-4, 'b');
                }
                if (isMultiPlayer) auxCard.GetComponent<CardScript>().isMultiPlayer = true;
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
                if (i == 1)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, 10, 'c');
                }
                else if (i == 3)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, 9, 'c');
                }
                else if (i == 2)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, 1, 'c');
                }
                else if (i >= 4 && i <= 7)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, i - 2, 'c');
                }
                else if (i >= 10 && i <= 12)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, i - 4, 'c');
                }
                if (isMultiPlayer) auxCard.GetComponent<CardScript>().isMultiPlayer = true;
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
                if (i == 1)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, 10, 'e');
                }
                else if (i == 3)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, 9, 'e');
                }
                else if (i == 2)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, 1, 'e');
                }
                else if (i >= 4 && i <= 7)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, i - 2, 'e');
                }
                else if (i >= 10 && i <= 12)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, i - 4, 'e');
                }
                if (isMultiPlayer) auxCard.GetComponent<CardScript>().isMultiPlayer = true;
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
                if (i == 1)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, 10, 'o');
                }
                else if (i == 3)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, 9, 'o');
                }
                else if (i == 2)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, 1, 'o');
                }
                else if (i >= 4 && i <= 7)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, i - 2, 'o');
                }
                else if (i >= 10 && i <= 12)
                {
                    auxCard.GetComponent<CardScript>().CreateCard(auxSprite, i - 4, 'o');
                }
                if (isMultiPlayer) auxCard.GetComponent<CardScript>().isMultiPlayer = true;
                deck.Add(auxCard);
            }

        }
    }

    void RepartirRonda()
    {
        if (!isTutorial)
        {
            Debug.Log("Repartimos ronda (num, numCards): " + actualRound + ", " + rounds[actualRound]);
            List<int> cardsGiven = new List<int>(40);
            GameObject auxCard = null;
            bool aux = true;
            int numCard = 0;
            UpdateRoundsPlayed();
            UpdateCardsDealed();
            for (int i = 0; i < players.Count; i++)
            {
                //Debug.Log("i = " + i);
                //Debug.Log("Ronda Actual = " + rounds[actualRound]);
                for (int j = 0; j < rounds[actualRound]; j++)
                {
                    aux = true;
                    while (aux)
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
                int playerMuestra;
                if (startPlayer == 0)
                {
                    playerMuestra = 3;
                }
                else
                {
                    playerMuestra = startPlayer - 1;
                }
                muestra = deck[cardsGiven[playerMuestra * 10 + 9]].GetComponent<CardScript>(); // TODO: La muestra es del que reparte, no el que empieza (es decir, el anterior al startPlayer)
            }
            else
            {
                aux = true;
                while (aux)
                {
                    numCard = Random.Range(0, 40);
                    if (!cardsGiven.Contains(numCard))
                    {
                        aux = false;
                    }
                }
                auxCard = deck[numCard];
                muestra = auxCard.GetComponent<CardScript>();
            }
            muestraSprite.sprite = muestra.GetComponent<CardScript>().GetSprite();
            center.ReceiveMuestra(muestra);
            center.ReceiveRonda(actualRound, rounds[actualRound]);
            center.playerToBet = startPlayer;
            center.playerToPlay = startPlayer;
            if (startPlayer == 0)
            {
                center.lastPlayerToBet = 3;
            }
            else
            {
                center.lastPlayerToBet = (startPlayer - 1) % players.Count;
            }
            center.betTime = true;
            actualRound++;
            if (PlayerPrefs.GetInt("OrderCards") == 1)
            {
                foreach (PlayerScript player in players) player.OrderCards(muestra.GetPalo()); // TODO: usar muestra
            }
            else
            {
                foreach (PlayerScript player in players) player.OrderCards();
            }

            for (int i = 0; i < players.Count; i++)
            {
                if (i == startPlayer) players[startPlayer].StartTurn();
                else players[i].StopTurn();
            }

            startPlayer = (startPlayer + 1) % players.Count;
        } else // TODO: tutorial
        {
            UpdateRoundsPlayed();
            UpdateCardsDealed();
            switch(actualRound)
            {
                case 0: // ganar por palo inicio
                    // carta a jugador
                    deck[9].GetComponent<CardScript>().SetPlayer(0);
                    players[0].AddCard(deck[9]);

                    // cartas a bots
                    deck[14].GetComponent<CardScript>().SetPlayer(1);
                    players[1].AddCard(deck[14]);

                    deck[4].GetComponent<CardScript>().SetPlayer(2);
                    players[2].AddCard(deck[4]);

                    deck[20].GetComponent<CardScript>().SetPlayer(3);
                    players[3].AddCard(deck[20]);
                    muestra = deck[36].GetComponent<CardScript>();
                    break;
                case 1: // no puede apostar 0... pero tiene muestra!
                    deck[19].GetComponent<CardScript>().SetPlayer(0);
                    players[0].AddCard(deck[19]);
                    
                    // cartas a bots
                    deck[8].GetComponent<CardScript>().SetPlayer(1);
                    players[1].AddCard(deck[8]);

                    deck[24].GetComponent<CardScript>().SetPlayer(2);
                    players[2].AddCard(deck[24]);

                    deck[32].GetComponent<CardScript>().SetPlayer(3);
                    players[3].AddCard(deck[32]);

                    muestra = deck[18].GetComponent<CardScript>();
                    break;
                case 2:
                    deck[31].GetComponent<CardScript>().SetPlayer(0);
                    players[0].AddCard(deck[31]);

                    // cartas a bots
                    deck[5].GetComponent<CardScript>().SetPlayer(1);
                    players[1].AddCard(deck[5]);

                    deck[29].GetComponent<CardScript>().SetPlayer(2);
                    players[2].AddCard(deck[29]);

                    deck[38].GetComponent<CardScript>().SetPlayer(3);
                    players[3].AddCard(deck[38]);

                    muestra = deck[6].GetComponent<CardScript>();
                    break;
                case 3:
                    deck[28].GetComponent<CardScript>().SetPlayer(0);
                    players[0].AddCard(deck[28]);

                    // cartas a bots
                    deck[25].GetComponent<CardScript>().SetPlayer(1);
                    players[1].AddCard(deck[25]);

                    deck[23].GetComponent<CardScript>().SetPlayer(2);
                    players[2].AddCard(deck[23]);

                    deck[8].GetComponent<CardScript>().SetPlayer(3);
                    players[3].AddCard(deck[8]);

                    muestra = deck[21].GetComponent<CardScript>();
                    break;
                case 4:
                    deck[0].GetComponent<CardScript>().SetPlayer(0);
                    players[0].AddCard(deck[0]);
                    deck[18].GetComponent<CardScript>().SetPlayer(0);
                    players[0].AddCard(deck[18]);
                    // cartas a bots
                    deck[11].GetComponent<CardScript>().SetPlayer(1);
                    players[1].AddCard(deck[11]);
                    deck[21].GetComponent<CardScript>().SetPlayer(1);
                    players[1].AddCard(deck[21]);

                    deck[23].GetComponent<CardScript>().SetPlayer(2);
                    players[2].AddCard(deck[23]);
                    deck[33].GetComponent<CardScript>().SetPlayer(2);
                    players[2].AddCard(deck[33]);

                    deck[19].GetComponent<CardScript>().SetPlayer(3);
                    players[3].AddCard(deck[19]);
                    deck[22].GetComponent<CardScript>().SetPlayer(3);
                    players[3].AddCard(deck[22]);

                    muestra = deck[1].GetComponent<CardScript>();
                    break;
                case 5: // random
                    List<int> cardsGiven = new List<int>(40);
                    GameObject auxCard;
                    int numCard = 0;
                    bool aux;
                    for (int i = 0; i < players.Count; i++)
                    {
                        //Debug.Log("i = " + i);
                        //Debug.Log("Ronda Actual = " + rounds[actualRound]);
                        for (int j = 0; j < rounds[actualRound]; j++)
                        {
                            aux = true;
                            while (aux)
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
                    aux = true;
                    while (aux)
                    {
                        numCard = Random.Range(0, 40);
                        if (!cardsGiven.Contains(numCard))
                        {
                            aux = false;
                        }
                    }
                    auxCard = deck[numCard];
                    muestra = auxCard.GetComponent<CardScript>();
                    break;
            }
            muestraSprite.sprite = muestra.GetComponent<CardScript>().GetSprite();
            center.ReceiveMuestra(muestra);
            center.ReceiveRonda(actualRound, rounds[actualRound]);
            center.playerToBet = startPlayer;
            center.playerToPlay = startPlayer;
            if (startPlayer == 0)
            {
                center.lastPlayerToBet = 3;
            }
            else
            {
                center.lastPlayerToBet = (startPlayer - 1) % players.Count;
            }
            center.betTime = true;
            actualRound++;
            if (PlayerPrefs.GetInt("OrderCards") == 1)
            {
                foreach (PlayerScript player in players) player.OrderCards(muestra.GetPalo()); // TODO: usar muestra
            }
            else
            {
                foreach (PlayerScript player in players) player.OrderCards();
            }

            for (int i = 0; i < players.Count; i++)
            {
                if (i == startPlayer) players[startPlayer].StartTurn();
                else players[i].StopTurn();
            }

            startPlayer = (startPlayer + 1) % players.Count;
        }
        
    }


    private void Awake()
    {
        // Players
        PlayerScript[] auxPlayers = GetComponentsInChildren<PlayerScript>();
        for (int i = 0; i < auxPlayers.Length; i++) players.Add(auxPlayers[i]);
        startPlayer = 0; // TODO: Dejarlo a 0
                            //
                            // rondas = new int[(40 / players.Count)+(players.Count - 1)];
        if (!isTutorial)
        {
            for (int i = 0; i < players.Count; i++) rounds.Add(1); // agregar tantas de 1 como jugadores
            for (int i = 2; i <= (40 / players.Count); i++) // agregar rondas intermedias
            {
                rounds.Add(i);
            }
            for (int i = 0; i < (players.Count - 1); i++) // agregar faltantes de la ultima ronda
            {
                rounds.Add(40 / players.Count);
            }
        }
        else // TODO: tutorial
        {
            for (int i = 0; i < players.Count; i++) rounds.Add(1);
            rounds.Add(2);
            rounds.Add(5);
        }

        actualRound = 0; // TODO: Dejarlo a 0
        ScoreBoard.GetInstance().InitiateScoreBoard(rounds.Count, players.Count); // inicializamos el scoreBoard
        Debug.Log(ScoreBoard.GetInstance().ScoreSize());
        //
        center = GetComponentInChildren<CenterScript>();
        center.AddPlayers(players);
        //
    }
    // Use this for initialization
    void Start () {
        Debug.Log(PlayerPrefs.GetInt("OrderCards"));
        InitializeDeck();
        //RepartirRonda();
	}
	
	// Update is called once per frame
	void Update () {
        if (isMultiPlayer)
        {
            if (PhotonNetwork.room != null)
            {
                Debug.Log("Room Player Count: " + PhotonNetwork.room.PlayerCount);
                if (PhotonNetwork.room.PlayerCount == 4)
                {
                    if (center.NeedNextRound())
                    {
                        center.nextRound = false;
                        center.ResetValues();
                        Debug.Log(ScoreBoard.GetInstance().WriteConsoleBets());
                        Debug.Log(ScoreBoard.GetInstance().WriteConsoleRoundsWon());
                        Debug.Log(ScoreBoard.GetInstance().WriteConsoleScore());
                        //ScoreBoard.GetInstance().UpdateScoreRound(actualRound);
                        UpdateScore();
                        if (actualRound < rounds.Count)
                        {
                            RepartirRonda();
                        }
                        else
                        {
                            Debug.Log("ITS OVER");
                            int winner = ScoreBoard.GetInstance().Winner();
                            if (winner == 0)
                            {
                                winnerText.text = "¡Has ganado!";
                            }
                            else
                            {
                                winnerText.text = "La siguiente vez será";
                            }
                            //UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
                        }
                    }
                }
            }
        } else
        {
            if (center.NeedNextRound())
            {
                center.nextRound = false;
                center.ResetValues();
                Debug.Log(ScoreBoard.GetInstance().WriteConsoleBets());
                Debug.Log(ScoreBoard.GetInstance().WriteConsoleRoundsWon());
                Debug.Log(ScoreBoard.GetInstance().WriteConsoleScore());
                //ScoreBoard.GetInstance().UpdateScoreRound(actualRound);
                UpdateScore();
                if (actualRound < rounds.Count)
                {
                    RepartirRonda();
                }
                else
                {
                    Debug.Log("ITS OVER");
                    int winner = ScoreBoard.GetInstance().Winner();
                    if (winner == 0)
                    {
                        winnerText.text = "¡Has ganado!";
                    }
                    else
                    {
                        winnerText.text = "La siguiente vez será";
                    }
                    //UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
                }
            }
        }
    }

    private void UpdateScore()
    {
        scoreD.text = "Puntos: " + ScoreBoard.GetInstance().ScorePlayer(0).ToString();
        scoreR.text = "Puntos: " + ScoreBoard.GetInstance().ScorePlayer(1).ToString();
        scoreU.text = "Puntos: " + ScoreBoard.GetInstance().ScorePlayer(2).ToString();
        scoreL.text = "Puntos: " + ScoreBoard.GetInstance().ScorePlayer(3).ToString();
    }

    public void UpdateRoundsPlayed()
    {
        roundsPlayed.text = "Rondas: " + (actualRound + 1) + "/" + rounds.Count;
    }
    
    public void UpdateCardsDealed()
    {
        cardsDealed.text = "Repartido " + rounds[actualRound] + " cartas.";
    }
}
