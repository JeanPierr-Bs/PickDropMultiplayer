using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviourPun
{
    [Header("Camera Settings")]
    public Camera playerCamera;
    public float mouseSensibility = 150f;
    public float minY = -60f;
    public float maxY = 60f;
    
    float xRotation = 0f;

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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        if(!photonView.IsMine) return;
        if(GameManager.Instance != null && !GameManager.Instance.IsPlaying()) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensibility * Time.deltaTime;

        //Rotacion de la camara
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minY, maxY);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //Rotacion horizontal
        transform.Rotate(Vector3.up * mouseX);
    }
}
