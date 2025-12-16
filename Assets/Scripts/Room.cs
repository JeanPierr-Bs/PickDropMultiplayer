using Photon.Pun;
using UnityEngine;
using TMPro;
public class Room : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI Name;

    public void JoinRoom()
    {
        // CreateAndJoin caj = FindFirstObjectByType<CreateAndJoin>();
        // caj.JoinRoomInList(Name.text);
        GameObject.Find("CreateAndJoin").GetComponent<CreateAndJoin>().JoinRoomInList(Name.text);
    }
}
