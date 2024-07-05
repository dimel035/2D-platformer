using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    SaveManager sm = new SaveManager();

    [SerializeField] private AudioClip checkpointSound;
    private Transform lastCheckpoint;
    private Health playerHealth;
    private UIManager uimngr;

    public StuffToSave myStuff;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uimngr = FindObjectOfType<UIManager>();
        myStuff = playerHealth.myStuff;
        myStuff.lastCheckpoint = "";
        Debug.Log("lastCheckpointinrespawn:" + myStuff.lastCheckpoint.ToString());
        Debug.Log("healthrespawn:" + myStuff.currentHealth.ToString());
        Debug.Log("levelinrespawn:" + myStuff.level.ToString());
        
    }

    public void CheckRespawn()
    {
        if (myStuff.lastCheckpoint == "")
        {
            //Game Over
            uimngr.GameOver();
            return;
        }

        transform.position = lastCheckpoint.position;

        playerHealth.Respawn();

        /*Camera.main.GetComponent<CameraController>().MoveToNewRoom(lastCheckpoint.parent);*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            lastCheckpoint = collision.transform;
            myStuff.level = SceneManager.GetActiveScene().buildIndex;
            myStuff.currentHealth = playerHealth.currentHealth;
            myStuff.lastCheckpoint = collision.transform.name;
            Debug.Log("checkpoint:" + myStuff.lastCheckpoint.ToString());
            sm.SaveStuff(myStuff);
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("open");
        }
    }
}
