using Photon.Pun;
using UnityEngine;
using TMPro;
public class Room : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI roomName;

    public void JoinRoom()
    {
        // CreateAndJoin createAndJoin = GameObject.Find("CreateAndJoin").GetComponent<CreateAndJoin>();
        // createAndJoin.JoinRoomInList(roomName.text);
        CreateAndJoin caj = FindFirstObjectByType<CreateAndJoin>();
        caj.JoinRoomInList(roomName.text);
    }
}
