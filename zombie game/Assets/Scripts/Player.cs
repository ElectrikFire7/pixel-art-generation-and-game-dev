using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float jumpForce = 11f;
    //public float maxVelocity = 22f;

    private float movementX;

    private Rigidbody2D myBody;

    private Animator anim;

    private SpriteRenderer sr;

    private string WALK_ANIMATION = "Walk";

    private bool isGrounded;

    private string GROUND_TAG = "Ground";

    // Start is called before the first frame update
    private void Awake()
    {
        myBody = GetComponent < Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
    }

    private void FixedUpdate()
    {
        PlayerJump();
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;

        AnimatePlayer();
    }

    void AnimatePlayer()
    {

        if (movementX > 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }
        else if (movementX < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else
            anim.SetBool(WALK_ANIMATION, false);

    }

    void PlayerJump ()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }

     

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Collector"))
        {
            Destroy(gameObject);
        }
    }
}
