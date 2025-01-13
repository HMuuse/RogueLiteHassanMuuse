using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float wallJumpForce = 2f;
    public float jumpForce = 10f;

    public StateMachine machine; // Reference to the state machine
    public Rigidbody2D rb;
    public Animator animator;


    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform wallCheck;
    public LayerMask wallLayer;
    public bool CanDoubleJump { get; set; } = true;

    // Abstract methods
    public abstract bool IsGrounded();
    public abstract bool IsWalled();
    public abstract void Move(float horizontalInput = 0f);

    public abstract void Die();

    public virtual void Jump() { }
    public virtual bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    public virtual void Start()
    {
        machine = new StateMachine();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        machine.Update();
    }
}
