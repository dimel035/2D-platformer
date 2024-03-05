using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    [Header("Sounds")]
    [SerializeField] private AudioClip onSound;

    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    private Health playerHealth;

    private void Awake()
    {
        anim= GetComponent<Animator>();
        spriteRend= GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerHealth != null && active)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = null;
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
        SoundManager.instance.PlaySound(onSound);

        //trap deactivation
        yield return new WaitForSeconds(activationDelay);
        active= false;
        triggered= false;
        anim.SetBool("activated", false);

    }
}
