using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviourPun
{
    public Camera playerCamera;

    void Awake()
    {
        if(!photonView.IsMine)
        {
            playerCamera.enabled = false;
            AudioListener audio = playerCamera.GetComponent<AudioListener>();
            if(audio != null) audio.enabled = false;
        }
        else
        {
            playerCamera.enabled = true;
        }
    }
}
