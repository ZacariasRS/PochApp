using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard
{
    private static ScoreBoard instance;

    public class ScoreBoardContent
    {
        public int bet;
        public int roundsWon;
        public int score;

        public ScoreBoardContent()
        {
            bet = 0;
            roundsWon = 0;
            score = 0;
        }
    }

    private ScoreBoardContent[,] scoreBoardContent; // filas son numero de rondas, columnas numero de jugadores.

    private ScoreBoard()
    {

    }

    public static ScoreBoard GetInstance()
    {
        if (instance == null)
        {
            instance = new ScoreBoard();
        }
        return instance;
    }

    public void InitiateScoreBoard(int rounds, int players) 
    {
        scoreBoardContent = new ScoreBoardContent[rounds,players];
        for (int i = 0; i < scoreBoardContent.GetLength(0); i++)
        {
            for (int j = 0; j < scoreBoardContent.GetLength(1); j++)
            {
                scoreBoardContent[i, j] = new ScoreBoardContent();
            }
        }
    }

    public void SetBetRoundPlayer(int r, int p, int b)
    {
        scoreBoardContent[r, p].bet = b;
    }

    public void SetRoundsWonRoundPlayer(int r, int p, int rw)
    {
        scoreBoardContent[r, p].roundsWon = rw;
    }

    public void IncrementRoundsWonRoundPlayer(int r, int p)
    {
        scoreBoardContent[r, p].roundsWon+=1;
    }

    public void CalculateScore(int r, int p)
    {
        if (scoreBoardContent[r,p].bet == scoreBoardContent[r,p].roundsWon)
        {
            scoreBoardContent[r, p].score = 10 + (5 * scoreBoardContent[r, p].roundsWon); // 10 por acertar + 5*rondas ganadas
        } else
        {
            scoreBoardContent[r, p].score = (-5 * (Mathf.Abs(scoreBoardContent[r, p].bet - scoreBoardContent[r, p].roundsWon))); // -5 * diferencia entre apuesta y ganado
        }
    }

    public string ScoreSize()
    {
        return "Rondas, Jugadores: " + scoreBoardContent.GetLength(0) + ", " + scoreBoardContent.GetLength(1);
    }


}