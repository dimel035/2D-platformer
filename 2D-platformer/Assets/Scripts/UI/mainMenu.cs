using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject optionsMenuScreen;
    private void Awake()
    {
        mainMenuScreen.SetActive(true);
        optionsMenuScreen.SetActive(false);
    }

    //main menu
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        if (mainMenuScreen.activeSelf)
        {
            mainMenuScreen.SetActive(false);
            optionsMenuScreen.SetActive(true);
        }
        else
        {
            mainMenuScreen.SetActive(true);
            optionsMenuScreen.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

//options
    public void volume()
    {
        SoundManager.instance.changeVolume(0.2f);
    }
    public void music()
    {
        SoundManager.instance.changeMusicVolume(0.2f);
    }
}
