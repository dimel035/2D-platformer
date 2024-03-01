using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    private void Awake()
    {
        anim= GetComponent<Animator>();
        spriteRend= GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(!triggered)
            {
                StartCoroutine(ActivateFiretrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
        
    }

    private IEnumerator ActivateFiretrap()
    {
        //sprite turns red, notifies player
        triggered= true;
        spriteRend.color = Color.red;

        //delay, trap activation, animation on, color back to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated",true);

        //trap deactivation
        yield return new WaitForSeconds(activationDelay);
        active= false;
        triggered= false;
        anim.SetBool("activated", false);

    }
}
