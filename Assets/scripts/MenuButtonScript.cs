using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour {

    public Button localPlay;
    public Button onlineMultiplayer;
    public Button options;
    public Button exit;

    public Toggle orderCards;
    public Button exitOptions;
    public GameObject whiteCube;
    public bool optionsOut;

	// Use this for initialization
	void Start () {
        optionsOut = true;
        orderCards.transform.position = new Vector3(300, 300, 0);
        exitOptions.transform.position = new Vector3(300, 300, 0);
        whiteCube.transform.position = new Vector3(300, 300, 0);
        Debug.Log(PlayerPrefs.GetInt("OrderCards", 0));
        if (PlayerPrefs.GetInt("OrderCards") == 1)
        {
            PlayerPrefs.SetInt("OrderCards", 0);
            orderCards.isOn = true;
        }
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
            orderCards.transform.position = new Vector3(0, 0, 0);
            exitOptions.transform.position = new Vector3(0, -40, 0);
            whiteCube.transform.position = new Vector3(0, -30, 0);
            optionsOut = false;
        } else
        {
            orderCards.transform.position = new Vector3(300, 300, 0);
            exitOptions.transform.position = new Vector3(300, 300, 0);
            whiteCube.transform.position = new Vector3(300, 300, 0);
            optionsOut = true;
        }
        
    }
}
