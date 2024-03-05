using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip pickUpSound;
    private bool used;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            used = collision.GetComponent<Health>().AddHealth(healthValue);
            if (used)
            {
                SoundManager.instance.PlaySound(pickUpSound);
                gameObject.SetActive(false);
            }
        }
    }
}
