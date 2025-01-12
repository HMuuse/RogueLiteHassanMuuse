using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : Entity
{
    public EventHandler OnGrounded;
    public EventHandler OnWallSlide;
    public EventHandler OnDoubleJump;

    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask wallLayer;
    [SerializeField]
    private float runSpeed = 5f;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;

    private float horizontalInput;
    private bool isFacingRight = true;

    public override void Start()
    {
        base.Start();

        // Initialize states and set the initial state
        machine.ChangeState(new IdleState(this));
    }

    public override void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (rb.velocity.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && isFacingRight)
        {
            Flip();
        }

        machine.Update();
        Debug.Log(IsGrounded());
    }

    private void Flip()
    {
        // Toggle the facing direction
        isFacingRight = !isFacingRight;

        // Flip the player by scaling
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    public override bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public override bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    public override void Move(float horizontalInput)
    {
        rb.velocity = new Vector2(horizontalInput * runSpeed, rb.velocity.y);
    }

    public override float GetHorizontalInput()
    {
        return horizontalInput;
    }

    public override void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}

