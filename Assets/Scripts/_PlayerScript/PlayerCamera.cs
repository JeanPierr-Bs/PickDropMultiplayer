using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviourPun
{
    [Header("Camera Settings")]
    public Camera playerCamera;
    public Transform cameraPivot;

    [Header("Rotation")]
    public float mouseSensibility = 150f;
    public float minY = -40f;
    public float maxY = 70f;

    [Header("Camera Follow")]
    public Vector3 cameraOffset = new Vector3(0f, 2f, -4f);
    public float followSmooth = 10f;
    
    float xRotation;
    float currentYaw;

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

    void LateUpdate()
    {
        if(!photonView.IsMine) return;
        if(GameManager.Instance != null && !GameManager.Instance.IsPlaying()) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensibility * Time.deltaTime;

        //Rotacion de la camara vertical
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minY, maxY);

        //Rotacion del personaje horizontal\
        currentYaw += mouseX;
        transform.rotation = Quaternion.Euler(0f, currentYaw, 0f);

        //Pivote Rota verticalmente
        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //Posicion de la camara
        Vector3 desiredPos = cameraPivot.TransformPoint(cameraOffset);
        playerCamera.transform.position = Vector3.Lerp(
            playerCamera.transform.position,
            desiredPos,
            followSmooth * Time.deltaTime
        );

        //Mirar al pivot
        playerCamera.transform.LookAt(cameraPivot);
    }
}
