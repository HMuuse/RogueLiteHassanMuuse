using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public StateMachine machine;
    public Animator playerAnimator;
    public Rigidbody2D playerRigidbody;

    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask wallLayer;
    [SerializeField]
    private float runSpeed = 5f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float wallJumpForce = 15f;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;

    public bool CanDoubleJump { get; set; } = true;

    private void Start()
    {
        machine = new StateMachine();

        // Initialize states and set the initial state
        machine.ChangeState(new IdleState(this));
    }

    private void Update()
    {
        machine.Update();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    public void Move(float horizontalInput)
    {
        playerRigidbody.velocity = new Vector2(horizontalInput * runSpeed, playerRigidbody.velocity.y);
    }

    public void Jump()
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
    }

    public bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
}

