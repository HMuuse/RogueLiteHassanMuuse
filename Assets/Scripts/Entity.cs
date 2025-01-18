using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public enum EntityType
    {
        Player,
        AngryPig,
        BlueBird
    }

    public EntityType entityType;

    public float moveSpeed = 5f;
    public float wallJumpForce = 2f;
    public float jumpForce = 10f;
    public float detectionRadius = 2f;

    public StateMachine machine;
    public Rigidbody2D rb;
    public Animator animator;


    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform wallCheck;
    public LayerMask wallLayer;
    public LayerMask enemyLayer;

    public bool CanDoubleJump { get; set; } = true;
    public Transform target;

    // Abstract methods
    public abstract bool IsGrounded();
    public abstract bool IsWalled();
    public abstract void Move(float horizontalInput = 0f);

    public abstract void Die();

    public virtual void Jump() { }
    public virtual void Chase(Transform destination) { }

    public virtual bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    public void Flip(float direction)
    {
        Debug.Log("Flipping");
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
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
