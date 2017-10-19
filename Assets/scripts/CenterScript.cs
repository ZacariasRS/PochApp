using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    public bool betTime;
    public int playerToBet;
    public int lastPlayerToBet;
    public bool deleteCards;
    public int playerToPlay;
    public bool botPlay;
    public int playerWonRound;
    public bool botBet;

    public InputField betD;
    public InputField betR;
    public InputField betU;
    public InputField betL;
    //public Text uiMuestra;
    public Text roundsWonD;
    public Text roundsWonR;
    public Text roundsWonU;
    public Text roundsWonL;
    public Text warning;


    



    public void AddPlayers(List<PlayerScript> p)
    {
        players = p;
    }

    public void ReceiveMuestra(CardScript csm)
    {
        muestra = csm.GetPalo();
        /*muestra = csm.GetPalo();
        switch (muestra)
        {
            case 'o':
                uiMuestra.text = "Muestra: " + csm.GetRank() + " Oros";
                break;
            case 'e':
                uiMuestra.text = "Muestra: " + csm.GetRank() + " Espadas";
                break;
            case 'c':
                uiMuestra.text = "Muestra: " + csm.GetRank() + " Copas";
                break;
            case 'b':
                uiMuestra.text = "Muestra: " + csm.GetRank() + " Bastos";
                break;
        }
        */
    }

    public void ReceiveRonda(int r, int nc)
    {
        actualRound = r;
        numCards = nc;
    }
    private void Awake()
    {
        betD.interactable = false;
        betR.interactable = false;
        betU.interactable = false;
        betL.interactable = false;
        playerToBet = 0;
        deleteCards = true;
        botPlay = true;
        botBet = true;
        warning.enabled = true;
    }
    // Use this for initialization
    void Start () {
        paloInicio = 'n';
        revisarJugada = false;
        cards = new List<CardScript>(players.Count);
        pasarTurno = false;
        nextRound = false;
        numTurns = 0;
        betTime = true;
        // UI
    }
	
	// Update is called once per frame
	void Update () {
        if (betTime)
        {
            switch(playerToBet)
            {
                case 0:
                    if (warning.text == "") warning.text = "Introduzca su apuesta\n(pulse 'Enter bet...')" ;
                    players[0].SetAllCardNormal();
                    betD.interactable = true;
                    //EventSystem.current.SetSelectedGameObject(betD.gameObject);
                    if (betD.isFocused/* && Input.GetKeyDown(KeyCode.UpArrow)*/ && betD.text != "")
                    {
                        if (playerToBet == lastPlayerToBet)
                        {
                            if (int.Parse(betD.text) <= numCards)
                            {
                                if (int.Parse(betD.text) + ScoreBoard.GetInstance().SumOfBetsOfRound(actualRound) != numCards)
                                {
                                    warning.text = "";
                                    ScoreBoard.GetInstance().SetBetRoundPlayer(actualRound, playerToBet, int.Parse(betD.text)); // 
                                    betD.interactable = false;
                                    playerToBet = -1;
                                    lastPlayerToBet = -1;
                                    betTime = false;
                                    if (!players[0].myTurn)
                                    {
                                        players[0].SetAllCardGrey();
                                    }
                                }
                                else warning.text = "No puedes apostar eso";
                            } 
                            else warning.text = "Nano no apuestes tanto";
                        } else
                        {
                            if (int.Parse(betD.text) <= numCards)
                            {
                                warning.text = "";
                                ScoreBoard.GetInstance().SetBetRoundPlayer(actualRound, playerToBet, int.Parse(betD.text));
                                playerToBet = (playerToBet + 1) % players.Count;
                                betD.interactable = false;
                                if (!players[0].myTurn)
                                {
                                    players[0].SetAllCardGrey();
                                }
                            }
                            else warning.text = "Nano no apuestes tanto";
                        }
                    }
                    break;
                case 1:
                    /*
                    betR.interactable = true;
                    if (betR.isFocused && Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (playerToBet == lastPlayerToBet)
                        {
                            if (int.Parse(betR.text) <= numCards)
                            {
                                if (int.Parse(betR.text) + ScoreBoard.GetInstance().SumOfBetsOfRound(actualRound) != numCards)
                                {
                                    warning.text = "";
                                    ScoreBoard.GetInstance().SetBetRoundPlayer(actualRound, playerToBet, int.Parse(betR.text)); // TODO: no se puede apostar el mismo numero de cartas repartidas
                                    betR.interactable = false;
                                    playerToBet = -1;
                                    lastPlayerToBet = -1;
                                    betTime = false;
                                }
                                else warning.text = "No puedes apostar eso";
                            }
                            else warning.text = "Nano no apuestes tanto";
                        }
                        else
                        {
                            if (int.Parse(betR.text) <= numCards)
                            {
                                warning.text = "";
                                ScoreBoard.GetInstance().SetBetRoundPlayer(actualRound, playerToBet, int.Parse(betR.text));
                                playerToBet = (playerToBet + 1) % players.Count;
                                betR.interactable = false;
                            }
                            else warning.text = "Nano no apuestes tanto";

                        }
                    }
                    break;
                    */
                    if (botBet) StartCoroutine(BotWaitBet(1f));
                    break;
                case 2:
                    /*betU.interactable = true;
                    if (betU.isFocused && Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (playerToBet == lastPlayerToBet)
                        {
                            if (int.Parse(betU.text) <= numCards)
                            {
                                if (int.Parse(betU.text) + ScoreBoard.GetInstance().SumOfBetsOfRound(actualRound) != numCards)
                                {
                                    warning.text = "";
                                    ScoreBoard.GetInstance().SetBetRoundPlayer(actualRound, playerToBet, int.Parse(betU.text)); // TODO: no se puede apostar el mismo numero de cartas repartidas
                                    betU.interactable = false;
                                    playerToBet = -1;
                                    lastPlayerToBet = -1;
                                    betTime = false;
                                }
                                else warning.text = "No puedes apostar eso";
                            }
                            else warning.text = "Nano no apuestes tanto";
                        }
                        else
                        {
                            if (int.Parse(betU.text) <= numCards)
                            {
                                warning.text = "";
                                ScoreBoard.GetInstance().SetBetRoundPlayer(actualRound, playerToBet, int.Parse(betU.text));
                                playerToBet = (playerToBet + 1) % players.Count;
                                betU.interactable = false;
                            }
                            else warning.text = "Nano no apuestes tanto";

                        }
                    }
                    break;
                    */
                    if (botBet) StartCoroutine(BotWaitBet(1f));
                    break;
                case 3:
                    /*betL.interactable = true;
                    if (betL.isFocused && Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (playerToBet == lastPlayerToBet)
                        {
                            if (int.Parse(betL.text) <= numCards)
                            {
                                if (int.Parse(betL.text) + ScoreBoard.GetInstance().SumOfBetsOfRound(actualRound) != numCards)
                                {
                                    warning.text = "";
                                    ScoreBoard.GetInstance().SetBetRoundPlayer(actualRound, playerToBet, int.Parse(betL.text)); // TODO: no se puede apostar el mismo numero de cartas repartidas
                                    betL.interactable = false;
                                    playerToBet = -1;
                                    lastPlayerToBet = -1;
                                    betTime = false;
                                }
                                else warning.text = "No puedes apostar eso";
                            }
                            else warning.text = "Nano no apuestes tanto";
                        }
                        else
                        {
                            if (int.Parse(betL.text) <= numCards)
                            {
                                warning.text = "";
                                ScoreBoard.GetInstance().SetBetRoundPlayer(actualRound, playerToBet, int.Parse(betL.text));
                                playerToBet = (playerToBet + 1) % players.Count;
                                betL.interactable = false;
                            }
                            else warning.text = "Nano no apuestes tanto";
                        }
                    }
                    break;*/
                    if (botBet) StartCoroutine(BotWaitBet(1f));
                    break;
                default:
                    Debug.Log("betTime nunca llegar aqui");
                    break;
            }
        } else
        {
            //Debug.Log("betTime falsee");
            if (!nextRound)
            {
                if (revisarJugada)
                {
                    //Debug.Log("revisaJugada");
                    if (deleteCards) StartCoroutine(Wait(2));
                    /*if (deleteCards)
                    {
                        int nextPlayer = RevisarJugada(firstPlayer);
                        revisarJugada = false;
                        //Debug.Log("numTurns, numCards" + numTurns + ", " + numCards);
                        if (numTurns != numCards) players[nextPlayer].StartTurn();
                        if (numTurns == numCards)
                        {
                            ScoreBoard.GetInstance().UpdateScoreRound(actualRound);
                            nextRound = true;
                        }
                    }
                    */
                }
            }
            else
            {
                Debug.Log("Ronda over");
            }
        }
        if (!players[playerToPlay].imPlayer && !betTime)
        {
            if(players[playerToPlay].myTurn)
            {
                if (botPlay)
                {
                    StartCoroutine(PlayerWaitPlayCard(0.5f));
                }
            }
        }
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        warning.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!revisarJugada && !betTime) // si no estamos revisando jugada ni apostando
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
                        card.EnterCenter(players[cardPlayer].imPlayer);
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
                                    card.EnterCenter(players[cardPlayer].imPlayer);
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
                                            warning.text = "Tiene una carta mas grande y no hay muestra en mesa";
                                            warning.enabled = true;
                                            card.ReturnToInitialPosition();
                                        } else
                                        {
                                            //Debug.Log("Tiene una carta mas grande pero hay muestra en la mesa");
                                            card.EnterCenter(players[cardPlayer].imPlayer);
                                            pasarTurno = true;
                                            cards.Add(card);
                                        }
                                        
                                    }
                                    else
                                    {
                                        //Debug.Log("No tiene una carta mas grande");
                                        card.EnterCenter(players[cardPlayer].imPlayer);
                                        pasarTurno = true;
                                        cards.Add(card);
                                    }
                                }
                            } else
                            {
                                //Debug.Log("Tiene palo inicio y juega otro palo");
                                warning.text = "Tiene palo inicio y juega otro palo";
                                warning.enabled = true;
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
                                                card.EnterCenter(players[cardPlayer].imPlayer);
                                                pasarTurno = true;
                                                rankMuestra = cardRank;
                                                cards.Add(card);
                                            } else
                                            {
                                                //Debug.Log("Tiene muestra y hay muestra en mesa y tenemos una mas grande y no la jugamos");
                                                warning.text = "Tiene muestra y hay muestra en mesa y tenemos una mas grande y no la jugamos";
                                                warning.enabled = true;
                                                card.ReturnToInitialPosition();
                                            }
                                        } else
                                        {
                                            //Debug.Log("Tiene muestra y hay muestra en mesa y tenemos una mas grande y no juega muestra");
                                            warning.text = "Tiene muestra y hay muestra en mesa y tenemos una mas grande y no juega muestra";
                                            warning.enabled = true;
                                            card.ReturnToInitialPosition();
                                        }
                                    } else
                                    {
                                        //Debug.Log("Tiene muestra y hay muestra en mesa y no tiene una mas grande");
                                        card.EnterCenter(players[cardPlayer].imPlayer);
                                        pasarTurno = true;
                                        cards.Add(card);
                                    }
                                } else // No hay muestra en la mesa
                                {
                                    //Debug.Log("No hay muestra en la mesa y tenemos muestra");
                                    if (cardPalo == muestra)
                                    {
                                        //Debug.Log("Tiene muestra y No hay muestra en la mesa y tenemos muestra y la carta es muestra");
                                        card.EnterCenter(players[cardPlayer].imPlayer);
                                        pasarTurno = true;
                                        hayMuestra = true;
                                        rankMuestra = cardRank;
                                        cards.Add(card);
                                    } else
                                    {
                                        //Debug.Log("No hay muestra en la mesa tenemos muestra y la carta no es muestra");
                                        warning.text = "No hay muestra en la mesa tenemos muestra y la carta no es muestra";
                                        warning.enabled = true;
                                        card.ReturnToInitialPosition();
                                    }
                                }
                            } else // No tiene palo inicio ni muestra, puede echar cualquiera
                            {
                                //Debug.Log("No tiene palo inicio y no tiene muestra");
                                card.EnterCenter(players[cardPlayer].imPlayer);
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
                            playerToPlay = 0;
                        }
                        else
                        {
                            //Debug.Log(auxPlayer);
                            players[auxPlayer].StopTurn();
                            ++auxPlayer;
                            playerToPlay = auxPlayer;
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
            //Debug.Log("index: " + index);
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
            //cards[i].HideCard();
        }
        Debug.Log("PlayerWinner: " + playerWinner);
        int roundsWonByPlayerWinner = ScoreBoard.GetInstance().IncrementRoundsWonRoundPlayer(actualRound, playerWinner); 
        UpdateRoundsWon(playerWinner, roundsWonByPlayerWinner);
        numTurns++;
        // TODO: Maybe meterlo en una funcion
        //cards.Clear();
        hayMuestra = false;
        rankMuestra = 0;
        higherRank = 0;
        //
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
        betD.text = "";
        betR.text = "";
        betU.text = "";
        betL.text = "";
        roundsWonD.text = "Rondas ganadas: 0";
        roundsWonR.text = "Rondas ganadas: 0";
        roundsWonU.text = "Rondas ganadas: 0";
        roundsWonL.text = "Rondas ganadas: 0";
        nextRound = false;
    }

    public bool NeedNextRound()
    {
        return nextRound;
    }

    public IEnumerator Wait(float seconds)
    {
        //Debug.Log(Time.time);
        deleteCards = false;
        int nextPlayer = RevisarJugada(firstPlayer);
        playerToPlay = nextPlayer;
        playerWonRound = nextPlayer;
        paloInicio = 'n';
        yield return new WaitForSeconds(0.5f);
        foreach (CardScript card in cards)
        {
            card.moving = nextPlayer;
        }
        yield return new WaitForSeconds(seconds);
        foreach (CardScript card in cards)
        {
            card.moving = -1;
            card.HideCard();
        }
        cards.Clear();
        revisarJugada = false;
        //Debug.Log("numTurns, numCards" + numTurns + ", " + numCards);
        if (numTurns != numCards) players[nextPlayer].StartTurn();
        if (numTurns == numCards)
        {
            ScoreBoard.GetInstance().UpdateScoreRound(actualRound);
            nextRound = true;
        }
        deleteCards = true;
        //Debug.Log(Time.time);
    }

    public IEnumerator PlayerWaitPlayCard(float seconds)
    {
        botPlay = false;
        yield return new WaitForSeconds(seconds);
        players[playerToPlay].PlayCard(paloInicio, higherRank, hayMuestra, rankMuestra, muestra, ScoreBoard.GetInstance().GetRoundsWon(actualRound, playerToPlay));
        botPlay = true;
    }

    public IEnumerator BotWaitBet(float seconds)
    {
        botBet = false;
        if (playerToBet == lastPlayerToBet)
        {
            warning.text = "";
            int bet = players[playerToBet].Bet(muestra);
            if ((numCards - ScoreBoard.GetInstance().SumOfBetsOfRound(actualRound)) == bet)
            {
                if ((bet - 1) >= 0)
                {
                    bet = 0;
                }
                else bet++;
            }
            ScoreBoard.GetInstance().SetBetRoundPlayer(actualRound, playerToBet, bet); 
            switch (playerToBet)
            {
                case 0:
                    break;
                case 1:
                    betR.text = bet.ToString();
                    betR.interactable = false;
                    break;
                case 2:
                    betU.text = bet.ToString();
                    betU.interactable = false;
                    break;
                case 3:
                    betL.text = bet.ToString();
                    betL.interactable = false;
                    break;
            }
            playerToBet = -1;
            lastPlayerToBet = -1;
            betTime = false;
        }
        else
        {
            warning.text = "";
            int bet = players[playerToBet].Bet(muestra);
            ScoreBoard.GetInstance().SetBetRoundPlayer(actualRound, playerToBet, bet);
            switch (playerToBet)
            {
                case 0:
                    break;
                case 1:
                    betR.text = bet.ToString();
                    betR.interactable = false;
                    break;
                case 2:
                    betU.text = bet.ToString();
                    betU.interactable = false;
                    break;
                case 3:
                    betL.text = bet.ToString();
                    betL.interactable = false;
                    break;
            }
            playerToBet = (playerToBet + 1) % players.Count;
            betR.interactable = false;
        }
        yield return new WaitForSeconds(seconds);
        botBet = true;
    }

    private void UpdateRoundsWon(int p, int rw)
    {
        switch(p)
        {
            case 0:
                roundsWonD.text = "Rondas ganadas: " + rw.ToString();
                break;
            case 1:
                roundsWonR.text = "Rondas ganadas: " + rw.ToString();
                break;
            case 2:
                roundsWonU.text = "Rondas ganadas: " + rw.ToString();
                break;
            case 3:
                roundsWonL.text = "Rondas ganadas: " + rw.ToString();
                break;
        }
    }
}
