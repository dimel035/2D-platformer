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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
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
