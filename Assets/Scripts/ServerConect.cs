using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerConect : MonoBehaviourPunCallbacks
{
    public void StartGame()
    {
        if(PhotonNetwork.IsConnected)
            return;
        // Crear o unirse a una sala
        //PhotonNetwork.JoinOrCreateRoom("DefaultRoom", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
        PhotonNetwork.ConnectUsingSettings();
    }

    // void Start()
    // {
    //     if(!PhotonNetwork.IsConnected)
    //     {
    //         Debug.Log("🔌 Conectando a Photon...");
    //         PhotonNetwork.ConnectUsingSettings();
    //     }
    // }

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

    // public override void OnJoinedRoom()
    // {
    //     Debug.Log("🚀 Entraste a la sala, cargando escena de juego...");
    //     SceneManager.LoadScene("GamePlay");
    // }
}