using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class NextLevelPortal : MonoBehaviour
{
    
    [SerializeField] public LevelLoader ll;
    [SerializeField] private AudioClip NextLevelSound;
    [SerializeField] private Transform glow;
    [SerializeField] public Animator anim;

    [SerializeField] private Health playerHealth;
    public StuffToSave myStuff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {

            SaveManager sm = gameObject.AddComponent<SaveManager>();
            playerHealth = (Health)collision.GetComponent<Health>();
            myStuff = playerHealth.myStuff;
            myStuff.level = SceneManager.GetActiveScene().buildIndex;
            myStuff.currentHealth = playerHealth.currentHealth;
            myStuff.lastCheckpoint = "";
            Debug.Log("checkpoint:" + myStuff.lastCheckpoint.ToString());
            sm.SaveStuff(myStuff);
            Debug.Log("lastCheckpointinrespawn:" + myStuff.lastCheckpoint.ToString());
            Debug.Log("healthrespawn:" + myStuff.currentHealth.ToString());
            Debug.Log("levelinrespawn:" + myStuff.level.ToString());

            anim.SetTrigger("portalOpen");



            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        SoundManager.instance.PlaySound(NextLevelSound);

        yield return new WaitForSeconds(1);

        ll.LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
