using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public Transform spawnPoint;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.JoinRandomOrCreateRoom(
            null, // Custom room properties (none in this case)
            2, // MaxPlayers
            MatchmakingMode.FillRoom, // Matching type
            null, // TypedLobby (use default)
            null, // SqlLobbyFilter (none)
            null, // ExpectedUsers (none)
            new RoomOptions { MaxPlayers = 2 } // RoomOptions
        );
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");

        if (PhotonNetwork.IsConnectedAndReady)
        {
            GameObject playerPrefab = PhotonNetwork.LocalPlayer.ActorNumber == 1 ? playerPrefab1 : playerPrefab2;
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a random room. Creating a new room.");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }
}
