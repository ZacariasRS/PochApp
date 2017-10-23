using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour {

    public Canvas menuCanvas;
    public Button localPlay;
    public Button onlineMultiplayer;
    public Button options;
    public Button exit;
    public Button rules;

    public Toggle orderCards;
    public GameObject whiteCube;
    public bool optionsOut;

    // RulesCanvas
    public Canvas rulesCanvas;
    public Button rulesNextButton;
    public Button rulesBeforeButton;
    public Text rulesText;
    public Text indexText;
    public GameObject rulesCube;
    public int rulesSize;
    public int rulesIndex;
    public string[] rulesInText;

	// Use this for initialization
	void Start () {
        optionsOut = true;
        orderCards.transform.position = new Vector3(300, 300, 0);
        whiteCube.transform.position = new Vector3(300, 300, 0);
        rulesCube.transform.position = new Vector3(300, 300, 0);
        Debug.Log(PlayerPrefs.GetInt("OrderCards", 1));
        rulesCanvas.enabled = false;
        if (PlayerPrefs.GetInt("OrderCards") == 1)
        {
            PlayerPrefs.SetInt("OrderCards", 0);
            orderCards.isOn = true;
        }
        rulesSize = 16;
        rulesIndex = 0;
        rulesInText = new string[rulesSize];
        TextRules();
        Debug.Log(rulesInText.Length);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LocalPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("pocha");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void OrderCardsToggle()
    {
        if (PlayerPrefs.GetInt("OrderCards", 0) == 0)
        {
            PlayerPrefs.SetInt("OrderCards", 1);
            Debug.Log(PlayerPrefs.GetInt("OrderCards"));
        }
        else
        {
            PlayerPrefs.SetInt("OrderCards", 0);
            Debug.Log(PlayerPrefs.GetInt("OrderCards"));
        }
    }

    public void OptionsButton()
    {
        if (optionsOut)
        { 
            orderCards.transform.position = new Vector3(0, 0, -2);
            //exitOptions.transform.position = new Vector3(0, -40, 0);
            whiteCube.transform.position = new Vector3(0, 0, -1);
            optionsOut = false;
        } else
        {
            orderCards.transform.position = new Vector3(300, 300, 0);
            //exitOptions.transform.position = new Vector3(300, 300, 0);
            whiteCube.transform.position = new Vector3(300, 300, 0);
            optionsOut = true;
        }
    }

    public void RulesButton()
    {
        if (rulesCanvas.enabled)
        {
            rulesCube.transform.position = new Vector3(300, 300, 0);
            rulesCanvas.enabled = false;
            menuCanvas.enabled = true;
        } else
        {
            rulesIndex = 0;
            rulesText.text = "Reglas del juego de cartas 'La Pocha')";
            indexText.text = "0/16";
            rulesCube.transform.position = new Vector3(0, 0, -1);
            menuCanvas.enabled = false;
            rulesCanvas.enabled = true;
        }
    }

    public void RulesNextButton()
    {
        if (rulesIndex < 16)
        {
            rulesIndex++;
            rulesText.text = rulesInText[rulesIndex];
            indexText.text = (rulesIndex+1).ToString() + "/" + rulesSize;
        }
    }

    public void RulesBeforeButton()
    {
        if (rulesIndex != 0)
        {
            rulesIndex--;
            rulesText.text = rulesInText[rulesIndex];
            indexText.text = (rulesIndex+1).ToString() + "/" + rulesSize;
        }
    }

    public void TextRules()
    {
        rulesInText[0] = "Se emplea la baraja española de 40 cartas. El orden de las cartas ordenadas de mayor a menor valor es: As, tres, rey, caballo, sota, siete, seis, cinco, cuatro y dos. Cada carta de esta serie del mismo palo gana a todas las que estén a su derecha, y pierde frente a todas las que tiene a su izquierda, por ejemplo, el rey gana a todas las cartas excepto al As y al tres.";
        rulesInText[1] = "Cualquier carta del palo de la muestra, es superior a cualquier carta de un palo que no sea muestra (Es decir, si muestra espadas, el dos de espadas es más valioso que el as de oros, de copas y de bastos).";
        rulesInText[2] = "La pocha es un juego de apuestas. Según el número de jugadores existe un número determinado de rondas y en cada ronda se reparte un número determinado de cartas. El objetivo es acertar las bazas que uno va a llevarse con las cartas que le han tocado en cada ronda, para sumar puntos. El ganador será el jugador que más puntos sume al finalizar la partida.";
        rulesInText[3] = "El sentido para fijar puestos, para dar las cartas y jugarlas es siempre el contrario al de las agujas del reloj. Tras decidir quien comienza a barajar se reparten las cartas. El número de cartas a repartir y el de rondas a jugar depende del número de jugadores.";
        rulesInText[4] = "Primero se juegan tantas rondas de una carta como número de jugadores haya.\n Después se va aumentando el número de cartas por baza progresivamente de una en una, hasta llegar al máximo de cartas que se pueden repartir y con ese número de cartas se juegan tantas rondas como jugadores haya";
        rulesInText[5] = "Fase 1.\n Previamente a jugar las cartas, en cada una de las manos, cada jugador tiene que decir, en función de las cartas que tiene, cuantas bazas cree que se va a llevar, comenzando por el jugador sentado a la derecha del que reparte y siguiendo en sentido antihorario.";
        rulesInText[6] = "El jugador que ha repartido, siempre será el último en anunciar el número de bazas que cree que se llevará e irá con 'apuesta obligada'.Nunca podrá decir que se llevará un número de bazas tal, que sumado a las apuestas de sus rivales, sumen el número de cartas repartidas a cada jugador.Es decir, si hay 3 jugadores y se han repartido 8 cartas y el 1º jugador apuesta 4 bazas y el 2º jugador 3 bazas, el 3º jugador no podrá apostar 1 baza, ya que sumarían 8.Tendrán que ser 2 bazas o ninguna.";
        rulesInText[7] = "Fase 2.\n Inicia el juego el jugador que se encuentra a la derecha del que ha repartido, echando una carta que dejará descubierta sobre la mesa. El jugador que sale puede echar la carta que quiera, mientras que el resto de jugadores deben seguir las siguientes reglas.";
        rulesInText[8] = "A) Si el primer jugador ha salido con muestra, se dice comúnmente que ha arrastrado. El resto de los jugadores están obligados a echar muestra, y ganando si es posible. En caso de no tener ningún muestra se echará cualquier otra carta.";
        rulesInText[9] = "B) Si el primer jugador no sale con muestra entonces:\n - Todos los jugadores deben seguir el palo de inicio, y superando a la carta más alta que haya de ese palo sobre la mesa, si es posible. \n";
        rulesInText[10] = "- Si un jugador no tiene ninguna carta del palo de inicio, debe echar muestra (lo que comúnmente se conoce como fallar).\n - En caso de no tener ninguna carta ni del palo de inicio ni de muestra, se puede echar cualquier carta de cualquiera de los dos palos restantes.";
        rulesInText[11] = "- Nota adicional: Si un jugador ha fallado previamente, los demás tienen que seguir el palo de inicio aunque ya no es preciso que superen ninguna carta ya que no optan a ganar la baza.\n - Nota adicional: Si un jugador ha fallado previamente y nosotros tampoco tenemos ninguna carta del palo de inicio, estamos obligados a fallar, solo si tenemos un muestra superior al más alto que haya sobre la mesa. En caso contrario, no se está obligado a desperdiciar el muestra (aunque se podría hacer si al jugador le interesara).";
        rulesInText[12] = "Gana la baza, la mayor carta jugada del palo de muestra y en su defecto, la carta más alta del palo de salida. El jugador que gana la baza recoge las cartas que la forman, dejándolas junto a sí. \n Inicia la baza siguiente el jugador que ganó la anterior, que jugará una carta cualquiera, continuando los demás por orden riguroso de izquierda a derecha en la forma ya explicada, hasta acabar todas las cartas.";
        rulesInText[13] = "Fase 3.\n Una vez que se han completado todas las bazas de una mano y los jugadores se han quedado sin cartas para jugar, se procederá al recuento de los puntos en función de los aciertos. \n Cada jugador ganará 10 puntos si adivinó el número exacto de bazas que se iba a llevar. Además, los jugadores que acierten su apuesta se llevarán 5 puntos por cada baza ganada en esa mano.";
        rulesInText[14] = "Los jugadores que no hubieran acertado el número de bazas que iban a llevarse perderán 5 puntos por cada baza de diferencia (tanto por exceso como por defecto) entre lo apostado y lo conseguido.";
        rulesInText[15] = "Final del Juego.\n Cuando se hayan repartido todas las rondas el jugador con más puntuación gana.";
    }
}
