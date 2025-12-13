using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviourPun
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;

    private CharacterController controller;
    public Animator anim;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        if (anim == null)
        Debug.LogError("[PlayerMovement] Animator NO encontrado");
    }

    void Update()
    {
        if(GameManager.Instance == null)
            return;
        
        if(!GameManager.Instance.IsPlaying())
            return;

        if (!photonView.IsMine) 
            return;

        if(anim == null) 
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        //Movimiento
        controller.Move(move * moveSpeed * Time.deltaTime);

        //Animacion
        bool isMoving = move.magnitude > 0.1f;
        anim.SetFloat("Blend", isMoving ? 1f : 0f);

        Debug.Log($"Playing: {GameManager.Instance.IsPlaying()}");

        // Gravedad básica
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
