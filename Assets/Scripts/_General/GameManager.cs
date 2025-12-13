using Mono.Cecil;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;
// using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPun

{
    public static GameManager Instance;

    public GameObject winPanel;

    private bool isPlaying = true;

    void Awake()
    {
        Instance = this;
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

    // public void loadNextLevel()
    // {
    //     Time.timeScale = 1f;
    //     // SceneManager.LoadScene("NextLevel"); // cámbialo por tu escena
    // }
}
