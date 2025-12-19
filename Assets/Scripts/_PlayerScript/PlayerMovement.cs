using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviourPun
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 8f;
    public float gravity = -30f;
    public float fallMultiplier = 2.5f;
    public LayerMask groundLayer;

    private CharacterController controller;
    private Animator anim;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!photonView.IsMine) return;
        if (GameManager.Instance != null && !GameManager.Instance.IsPlaying()) return;
        if (anim == null) return;

        isGrounded = CheckGround();
        anim.SetBool("IsGrounded", isGrounded);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 🎭 Blend SIEMPRE depende del input, no del estado previo
        float blendValue = move.magnitude > 0.05f ? 1f : 0f;
        anim.SetFloat("Blend", blendValue);

        // 🦘 Salto
        if (isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = -2f;

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpForce;
                anim.SetTrigger("Jump");
            }
        }

        // 🌍 Gravedad con carácter
        if (velocity.y < 0)
            velocity.y += gravity * fallMultiplier * Time.deltaTime;
        else
            velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    bool CheckGround()
    {
        Vector3 origin = transform.position + Vector3.up * 0.4f;
        return Physics.Raycast(origin, Vector3.down, 0.6f, groundLayer);
    }
}
