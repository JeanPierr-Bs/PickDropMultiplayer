using UnityEngine;

using Photon.Pun;

public class PlayerSkin : MonoBehaviour
{
    public GameObject[] skins;

    void Start()
    {
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        for(int i = 0; i < skins.Length; i++)
            skins[i].SetActive(false);

        int index = (actorNumber - 1) % skins.Length;
        skins[index].SetActive(true);

        Debug.Log($"Skin asignada; {index}");
    }
}
