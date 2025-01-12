using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    public float wallJumpForce = 2f;
    public float jumpForce = 10f;

    public StateMachine machine; // Reference to the state machine
    public Rigidbody2D rb;
    public Animator animator;
    public bool CanDoubleJump { get; set; } = true;

    // Abstract methods
    public abstract bool IsGrounded();
    public abstract bool IsWalled();
    public abstract void Move(float horizontalInput);

    // Implement this method to handle input in specific classes
    public virtual float GetHorizontalInput()
    {
        return 0f; // Default implementation for NPCs (if needed)
    }

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
