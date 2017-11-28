using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUNManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Pun connect");
        bool cn = PhotonNetwork.ConnectUsingSettings("0.1");
        Debug.Log(cn);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnFailedToConnectToPhoton()
    {

    }

    public void OnConnectionFail()
    {

    }

    public void OnJoinedLobby()
    {
        Debug.Log("Pun joined lobby");
        PhotonNetwork.JoinOrCreateRoom("room1", new RoomOptions() { MaxPlayers = 4, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }

    public void OnJoinedRoom()
    {
        Debug.Log("Pun joined room");
        Debug.Log("ID: " + PhotonNetwork.player.ID);
    }
}
