using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectLobbie : MonoBehaviourPunCallbacks
{
    public static ConnectLobbie Instance;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void StartGame()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("⛔ Solo el Host puede iniciar el juego");
            return;
        }

        Debug.Log("🎬 Host inicia Gameplay");

        PhotonNetwork.IsMessageQueueRunning = false;

        photonView.RPC(
            nameof(RPC_LoadGameplay),
            RpcTarget.All
        );
        // Debug.Log("🌍 Cargando Gameplay");
        // SceneManager.LoadScene("GamePlay");
    }

    [PunRPC]
    void RPC_LoadGameplay()
    {
        Debug.Log("🌍 Cargando Gameplay");
        SceneManager.LoadScene("GamePlay");
    }
}
