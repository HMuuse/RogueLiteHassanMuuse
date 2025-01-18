using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : Entity
{
    public EventHandler OnPickUpPowerUp;

    private float horizontalInput;
    private int gravityScale = 1;
    public bool isRotated = false;

    public override void Start()
    {
        base.Start();

        entityType = EntityType.Player;

        // Initialize states and set the initial state
        machine.ChangeState(new IdleState(this));
    }

    public override void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0 && !IsFacingRight())
        {
            Flip(horizontalInput);
        }
        else if (horizontalInput < 0 && IsFacingRight())
        {
            Flip(horizontalInput);
        }

        if (isRotated)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, -180, 180);
        }

        if (!isRotated)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        machine.Update();
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
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    public float GetHorizontalInput()
    {
        return horizontalInput;
    }

    public override void Jump()
    {
        isRotated = !isRotated;
        gravityScale = -gravityScale;
        rb.gravityScale = gravityScale;
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            OnPickUpPowerUp?.Invoke(this, EventArgs.Empty);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }
}