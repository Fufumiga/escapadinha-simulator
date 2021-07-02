using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class DoggoController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private MQTTConnector connector;

    private Vector2 changeInPos;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        changeInPos.x = Input.GetAxisRaw("Horizontal");
        changeInPos.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        UpdateAnimation();
        MoveDoggo();

        SendToBroker();
    }

    private void MoveDoggo()
    {
        rigidBody.MovePosition(rigidBody.position + changeInPos * speed * Time.fixedDeltaTime);
    }

    private void UpdateAnimation()
    {
        bool isMoving = changeInPos != Vector2.zero;

        animator.SetBool("moving", isMoving);

        spriteRenderer.flipX = changeInPos.x < 0;
    }

    private void SendToBroker()
    {
        if(connector is null) { return; }
        connector.SendPositionToBroker(transform.localPosition);
    }
}
