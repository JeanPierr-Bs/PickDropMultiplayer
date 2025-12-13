using Photon.Pun; 
using UnityEngine; 
public class PlayerSpawner : MonoBehaviourPunCallbacks
{ 
    public GameObject playerPrefab; 
    public Transform[] spawnPoints; 
    void Start() 
    { 
        if(!PhotonNetwork.InRoom) 
        { 
            Debug.LogError("❌ No conectado a Photon"); return; 
        } 
        SpawnPlayer(); 
    } 
    // public override void OnJoinedRoom()
    // {
    //     SpawnPlayer();
    // }
    void SpawnPlayer() 
    { 
        int index = PhotonNetwork.LocalPlayer.ActorNumber - 1; 

        index = Mathf.Clamp(index, 0, spawnPoints.Length - 1); 

        Vector3 spawnPos = spawnPoints[index].position; 

        PhotonNetwork.Instantiate( 
            playerPrefab.name, 
            spawnPos, 
            Quaternion.identity 
        ); 
        
        Debug.Log($"🧍 Player spawneado en punto {index}"); 
    } 
}


