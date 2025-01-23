using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : Entity
{
    public static PlayerController Instance { get; private set; }

    public EventHandler OnPickUpPowerUp;
    public EventHandler OnPlayerDeath;

    private float horizontalInput;
    private int gravityScale = 1;
    private bool isRotated = false;

    [SerializeField]
    private GameObject shield;
    private bool isShielded;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

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
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    public void EnableShield()
    {
        Debug.Log("Shield enabled!");
        shield.SetActive(true);
        isShielded = true;

        StopCoroutine(nameof(DisableShieldAfterDelay));

        StartCoroutine(DisableShieldAfterDelay(5f));
    }

    private IEnumerator DisableShieldAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Disable the shield if it is still active
        if (isShielded)
        {
            DisableShield();
        }
    }

    public void DisableShield()
    {
        shield.SetActive(false);
        isShielded = false;
    }

    public void SlowDownTime(float timeScale, float duration)
    {
        Time.timeScale = timeScale;

        StopCoroutine(nameof(ResetTimeScaleAfterDelay));

        StartCoroutine(ResetTimeScaleAfterDelay(duration));
    }

    private IEnumerator ResetTimeScaleAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        Debug.Log("Time scale reset.");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            OnPickUpPowerUp?.Invoke(this, EventArgs.Empty);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isShielded)
            {
                Die();
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}