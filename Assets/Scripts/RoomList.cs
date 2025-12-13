using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class RoomList : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject roomPrefabBtn;
    public GameObject[] Allrooms;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (GameObject room in Allrooms)
        {
            Destroy(room);
        }
        Allrooms = new GameObject[roomList.Count];
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            if (roomList[i].PlayerCount >=1 && roomList[i].IsOpen && roomList[i].IsVisible)
            {
                GameObject roomBtn = Instantiate(roomPrefabBtn);
                roomBtn.transform.SetParent(this.transform);
                roomBtn.transform.localScale = Vector3.one;
                Room roomScript = roomBtn.GetComponent<Room>();
                roomScript.roomName.text = roomList[i].Name;
                Allrooms[i] = roomBtn;
                
            }
                    
        }
    }  
}
