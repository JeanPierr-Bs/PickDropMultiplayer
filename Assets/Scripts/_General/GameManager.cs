using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPun

{
    public static GameManager Instance;

    public GameObject winPanel;

    private bool isPlaying = true;

    void Awake()
    {
        // Instance = this;
        // winPanel.SetActive(false);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Time.timeScale = 1f;

        if (winPanel != null)
            winPanel.SetActive(false);
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }

    [PunRPC]
    public void RPC_EndGame()
    {
        isPlaying = false;

        Time.timeScale = 0f; // 🔒 Congela TODO
        winPanel.SetActive(true);

        Debug.Log("🏆 Juego terminado");
    }

    public void EndGame()
    {
        photonView.RPC("RPC_EndGame", RpcTarget.All);
    }

    public void ResetGame()
    {
        Debug.Log("🔁 ResetGame llamado");

        if(!PhotonNetwork.IsMasterClient)
            return;

        photonView.RPC(
            nameof(RPC_ResetGame),
            RpcTarget.All
        );
    }

    [PunRPC]
    void RPC_ResetGame()
    {
        Debug.Log("♻️ Recargando escena en cliente");

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // public void loadNextLevel()
    // {
    //     Time.timeScale = 1f;
    //     // SceneManager.LoadScene("NextLevel"); // cámbialo por tu escena
    // }
}
