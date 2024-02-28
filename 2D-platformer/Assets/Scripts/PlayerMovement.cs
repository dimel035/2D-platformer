using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField]
    private float speed;
    private Animator anim;

    private void Awake()
    {
        //Get refrerences from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        body.velocity  =  new Vector2(horizontalInput * speed, body.velocity.y);

        //flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f )
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            body.velocity = new Vector2(body.velocity.x, speed);
        }

        //animator parameters
        anim.SetBool("run", horizontalInput != 0);
    }
}
