using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerConect : MonoBehaviourPunCallbacks
{
    // void Start()
    // {
    //     // Conectar al servidor de Photon
    //     PhotonNetwork.ConnectUsingSettings();
    // }

    public void StartGame()
    {
        // Crear o unirse a una sala
        //PhotonNetwork.JoinOrCreateRoom("DefaultRoom", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("✅ Conectado al Master Server");
        PhotonNetwork.JoinLobby(); // opcional, si quieres mostrar lista de salas
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("🎮 Entraste al Lobby");
        SceneManager.LoadScene("Lobby"); // aquí mostrarías UI de salas
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("🚀 Entraste a la sala, cargando escena de juego...");
        SceneManager.LoadScene("SampleScene");
    }
}