using Photon.Pun;
using UnityEngine;

public class PlayerTeam : MonoBehaviourPun
{
    public int teamId;

    void Start()
    {
        teamId = photonView.Owner.ActorNumber;

        GetComponentInChildren<Renderer>().material.color =
        teamId == 1 ? Color.blue : Color.red;
    }
}
