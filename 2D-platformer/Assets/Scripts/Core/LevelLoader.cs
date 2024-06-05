using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public void LoadNextLevel(int scene)
    {
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(int scene)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(scene);
        
    }
}
