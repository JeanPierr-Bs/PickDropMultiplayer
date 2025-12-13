using Photon.Pun;
using UnityEngine;

public class PlayerScore : MonoBehaviourPun
{
    public int score = 0;

    public void AddPoint()
    {
        score++;
    }
}
