using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject optionsMenuScreen;
    [SerializeField] public LevelLoader ll;

    SaveManager sm;
    private void Awake()
    {
        mainMenuScreen.SetActive(true);
        optionsMenuScreen.SetActive(false);
        sm = new SaveManager();
    }

    //main menu
    public void PlayGame()
    {
        PlayerPrefs.SetInt("GameState", 0); // 0 for new game
        PlayerPrefs.Save();
        ll.LoadNextLevel(1);
    }

    public void Resume()
    {
        PlayerPrefs.SetInt("GameState", 1); // 1 for resume
        PlayerPrefs.Save();
        StuffToSave myStuff = sm.LoadStuff();
        ll.LoadNextLevel(myStuff.level);
        Debug.Log("levelinmenu:"+myStuff.level.ToString());
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
