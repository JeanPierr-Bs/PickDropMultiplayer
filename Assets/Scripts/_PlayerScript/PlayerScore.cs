using Photon.Pun;
using UnityEngine;

public class PlayerScore : MonoBehaviourPun
{
    public int score = 0;
    public int pointsToWin = 1;

    public void AddPoint()
    {
        if(!photonView.IsMine) return;

        score++;
        Debug.Log($"[Score] Puntos: {score}");

        if(score>= pointsToWin)
        {
            GameManager.Instance.EndGame();
        }
    }
}
