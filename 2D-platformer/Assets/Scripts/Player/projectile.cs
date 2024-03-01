using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
   [SerializeField] private float speed;

    private float direction;
    private float lifespan;
    private bool hit;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        boxCollider= GetComponent<BoxCollider2D>();
        anim= GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed*Time.deltaTime*direction;
        transform.Translate(movementSpeed,0,0);
        lifespan += Time.deltaTime;
        if(lifespan>5)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled= false;
        anim.SetTrigger("explode");

    }

    public void SetDirection(float _direction)
    {
        lifespan = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit= false;
        boxCollider.enabled = true;

        float localScaleX=transform.localScale.x;
        if(Mathf.Sign(localScaleX)!=_direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector2(localScaleX,transform.localScale.y);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
