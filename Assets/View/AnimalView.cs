using UnityEngine;

public class AnimalView : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveDirection = Vector2.zero;
    [SerializeField]
    private float moveSpeed = 2f;


    public Animal animal { get; private set; }

    public void Initialize(Animal animal)
    {
        this.animal = animal;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveDirection = new Vector2(-1f, 1f);
        animator.SetBool("isWalking", true); 
        animator.SetFloat("X", moveDirection.x);
        animator.SetFloat("Y", moveDirection.y);
    }

    private void Update()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    private void Move(Vector2 direction)
    {
        moveDirection = direction;
        animator.SetFloat("X", moveDirection.x);
        animator.SetFloat("Y", moveDirection.y);
        animator.SetBool("isWalking", true);
    }

    private void Stop()
    {
        animator.SetFloat("X", moveDirection.x);
        animator.SetFloat("Y", moveDirection.y);
        moveDirection = Vector2.zero;
        animator.SetBool("isWalking", false);
    }
}
