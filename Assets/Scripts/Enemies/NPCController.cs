using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : Entity
{
    public override bool IsGrounded()
    {
        // Check if the NPC is grounded
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public override bool IsWalled()
    {
        // Check if the NPC is near a wall
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    public override void Move(float horizontalInput)
    {
        // Move the NPC horizontally and apply movement speed
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Flip the NPC based on movement direction
        if (horizontalInput > 0 && !IsFacingRight())
        {
            Flip(horizontalInput);
        }
        else if (horizontalInput < 0 && IsFacingRight())
        {
            Flip(horizontalInput);
        }
    }

    public override void Chase(Transform destination)
    {
        // Calculate direction towards the target
        Vector2 direction = (destination.position - transform.position).normalized;

        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }


    public override bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    public override void Die()
    {

    }

    public override void Start()
    {
        base.Start();
        machine.ChangeState(new IdleState(this));
    }

    public override void Update()
    {
        base.Update();


        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, 5f, enemyLayer);

        target = collider != null ? collider.transform : null;
    }
}
