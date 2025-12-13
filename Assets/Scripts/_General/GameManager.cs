using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPun

{
    public static GameManager Instance;

    public enum GameState { Playing, Finished }
    public GameState currentState = GameState.Playing;

    [Header("UI")]
    public GameObject winPanel;

    private void Awake()
    {
        Debug.Log($"[GameManager] Awake en {gameObject.name}");

        if (Instance == null)
        {
            Instance = this;
            Debug.Log("[GameManager] Instance asignada");
        }
        else
        {
            Debug.LogError("[GameManager] DUPLICADO DETECTADO, DESTRUYENDO");
            Destroy(gameObject);
        }
    }

    public void EndGame()
    {
        Debug.Log("[GameManager] EndGame() llamado");

        photonView.RPC(nameof(RPC_EndGame), RpcTarget.All);
    }

    [PunRPC]
    void RPC_EndGame()
    {
        Debug.Log("[GameManager] RPC_EndGame ejecutado");
        currentState = GameState.Finished;

        if (winPanel != null)
            winPanel.SetActive(true);

        Debug.Log("🏆 WIN PANEL ACTIVADO"); 
    }

    public bool IsPlaying()
    {
        return currentState == GameState.Playing;
    }

    public void loadNextLevel()
    {
        Time.timeScale = 1f;
        // SceneManager.LoadScene("NextLevel"); // cámbialo por tu escena
    }
}
