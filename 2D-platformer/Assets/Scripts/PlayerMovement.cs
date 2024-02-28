using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask wallLayer;
    [SerializeField]
    private Transform wallCheck;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    //private bool grounded;


    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private void Awake()
    {
        //Get refrerences from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        WallSlide();

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //if (wallJumpCooldown>0.2f) 
        //{


        //    body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //    if (onWall() && !isGrounded())
        //    {
        //        body.gravityScale = 0;
        //        body.velocity = Vector2.zero;
        //    }
        //    else
        //    {
        //        body.gravityScale = 7;
        //    }

        //    if (Input.GetKey(KeyCode.Space))
        //    {
        //        Jump();
        //    }
        //}
        //else
        //{
        //    wallJumpCooldown += Time.deltaTime;
        //}
    }
    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        //else if (onWall() && !isGrounded())
        //{
        //    if(horizontalInput == 0)
        //    {
        //        body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 6);
        //        transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), transform.localScale.y);    
        //    }
        //    else
        //    {
        //        body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 5, 6);
        //    }
        //    wallJumpCooldown = 0;
            
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private bool isGrounded()
    {
        /*        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,0,Vector2.down, 0.1f, groundLayer);
                return raycastHit.collider!=null;*/
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (isWalled() && !isGrounded() && horizontalInput!=0f)
        {
            isWallSliding = true;
            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    //private bool onWall()
    //{
    //    RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
    //    return raycastHit.collider != null;
    //}
}
