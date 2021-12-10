using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//ADD PUN THINGS

public class Launcher : MonoBehaviour//MonoBehaviourPunCallBacks
{
    public GameObject lobbyPanel;
    public GameObject roomPanel;

    public InputField createInput;
    public InputField joinInput;

    public Text roomName;
    public Text playerCount;

    public GameObject playerListing;
    public Transform playerListContent;

    public Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
    }

    public void CreateRoom() { }

    public void JoinRoom() { }

    //public override void OnJoinedRoom() { }

    public void OnClickLeaveRoom() { }

    //public override void OnLeftRoom() { }

    //public override void OnPlayerEnteredRoom(Player newPlayer) { }

    public void OnClickStartGame() { }
}