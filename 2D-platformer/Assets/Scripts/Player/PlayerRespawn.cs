using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform lastCheckpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth= GetComponent<Health>();

    }

    public void Respawn()
    {
        transform.position = lastCheckpoint.position;

        playerHealth.Respawn();

        /*Camera.main.GetComponent<CameraController>().MoveToNewRoom(lastCheckpoint.parent);*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            lastCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("open");
        }
    }
}
