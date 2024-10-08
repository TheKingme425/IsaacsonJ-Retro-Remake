using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Camera camera;
    private Rigidbody2D rigidbody;

    private Vector2 velocity;
    private float inputAxis;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;

    public float JumpForce => (2f * maxJumpHeight) / (maxJumpTime /2f); 
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;  
    }

    private void Update()
    {
        HorizontalMovement();

        grounded = rigidbody.Raycast(Vector2.down);

        if (grounded)
        {
            GroundMovement();
        }

        ApplyGravity();
    }

    private void GroundMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0);
        jumping = velocity.y < 0;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = JumpForce;
            jumping = true;
        }
         if (Input.GetButton("Jump"))
        {
            velocity.y = JumpForce;
            jumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0 || !Input.GetButton("Jump");
        float multiplyer = falling ? 2f : 1f;

        velocity.y += gravity * multiplyer * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }
    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        if (rigidbody.Raycast(Vector2.right * velocity.x)) 
        {
            velocity.x = 0f;
        }

        if (velocity.x > 0f) 
        {
            transform.eulerAngles = Vector3.zero;
        } 
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
       
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 LeftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 RightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, LeftEdge.x + 0.3f, RightEdge.x);

        rigidbody.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = JumpForce / 2f;
                jumping = true;
            }
        }
        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }

        }
    }
}   