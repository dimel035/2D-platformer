using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] private AudioClip endSound;
    [SerializeField] private GameObject endScreen;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Time.timeScale = 0;
            endScreen.SetActive(true);
            SoundManager.instance.PlaySound(endSound);
            anim.SetTrigger("endgame");


        }
    }

    }
