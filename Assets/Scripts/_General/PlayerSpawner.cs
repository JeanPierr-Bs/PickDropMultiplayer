using System.Collections;
using Photon.Pun; 
using UnityEngine; 
public class PlayerSpawner : MonoBehaviourPunCallbacks
{ 
    public GameObject playerPrefab; 
    public Transform[] spawnPoints; 

    private bool hasSpawned = false;
    
    void Start() 
    { 
        if(!PhotonNetwork.InRoom) 
        { 
            Debug.LogError("❌ No conectado a Photon"); return; 
        } 
            // Verificar configuración de sala
        Debug.Log($"🔹 AutoCleanUp: {PhotonNetwork.AutomaticallySyncScene}");
        Debug.Log($"🔹 IsMasterClient: {PhotonNetwork.IsMasterClient}");
        Debug.Log($"🔹 Jugadores en sala: {PhotonNetwork.CurrentRoom.PlayerCount}");

        if (hasSpawned) return;
        
        SpawnPlayer(); 
        hasSpawned = true;
    } 

    // public override void OnJoinedRoom()
    // {
    //     SpawnPlayer();
    // }
    // public override void OnJoinedRoom()
    // {
    //     if (hasSpawned) return;

    //     SpawnPlayer();
    //     hasSpawned = true;
    // }

    void SpawnPlayer() 
    { 
        if (!PhotonNetwork.InRoom)
        {
            Debug.LogError("❌ No estás en una sala");
            return;
        }

        int index = PhotonNetwork.LocalPlayer.ActorNumber - 1; 
        index = Mathf.Clamp(index, 0, spawnPoints.Length - 1); 

        Vector3 spawnPos = spawnPoints[index].position; 

        PhotonNetwork.Instantiate( 
            playerPrefab.name, 
            spawnPos, 
            Quaternion.identity 
        ); 
        
        Debug.Log($"🧍 Player {PhotonNetwork.LocalPlayer.ActorNumber} spawneado en punto {index}");
    } 
}


