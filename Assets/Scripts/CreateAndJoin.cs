using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;  

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_InputField inputCreate;
    public TMP_InputField inputJoin;
    
    public void CreateRoom()
    {
        // PhotonNetwork.CreateRoom(inputCreate.text, new RoomOptions { MaxPlayers = 4 });
        if(string.IsNullOrEmpty(inputCreate.text))
            return;

        PhotonNetwork.CreateRoom(
            inputCreate.text,
            new RoomOptions { MaxPlayers = 4 }
        );
    }
    public void JoinRoom()
    {
        // PhotonNetwork.JoinRoom(inputJoin.text);
        if(string.IsNullOrEmpty(inputJoin.text))
            return;

        PhotonNetwork.JoinRoom(inputJoin.text);
    }
    public void JoinRoomInList(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("🚀 Entraste a la sala, cargando escena de juego...");
        // if(PhotonNetwork.IsMasterClient)
        // {
        //     PhotonNetwork.LoadLevel("GamePlay");
        // }
        // SceneManager.LoadScene("GamePlay");
    }
}
