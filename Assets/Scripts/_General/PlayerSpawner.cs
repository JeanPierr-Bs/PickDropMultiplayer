using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;

    public override void OnJoinedRoom()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        int index = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        index = index % spawnPoints.Length;

        Vector3 spawnPos = spawnPoints[index].position;

        PhotonNetwork.Instantiate(
            playerPrefab.name,
            spawnPos,
            Quaternion.identity
        );

        Debug.Log($"🧍 Player spawneado en punto {index}");
    }
}
