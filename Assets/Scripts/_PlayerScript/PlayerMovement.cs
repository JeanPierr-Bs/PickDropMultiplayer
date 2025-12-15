using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviourPun
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Jump Settings")]
    public float jumpForce = 6f;
    public LayerMask groundLayer;
    public float gravity = -20f;


    private bool isGrounded;
    private CharacterController controller;
    private Animator anim;
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
        if (GameManager.Instance == null) return;
        if (!GameManager.Instance.IsPlaying()) return;
        if (!photonView.IsMine) return;
        if (anim == null) return;

        isGrounded = CheckGround();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Animación
        anim.SetFloat("Blend", move.magnitude > 0.1f ? 1f : 0f);

        // Salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
        }

        // Gravedad
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // bool IsGrounded()
    // {
    //     return Physics.Raycast(
    //         transform.position + Vector3.up * 0.1f,
    //         Vector3.down,
    //         0.2f
    //     );
    // }

    bool CheckGround()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        float distance = 0.3f;

        return Physics.Raycast(
            origin,
            Vector3.down,
            distance,
            groundLayer
        );
    }

    
}
