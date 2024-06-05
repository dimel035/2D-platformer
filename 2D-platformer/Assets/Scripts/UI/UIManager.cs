using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   [Header ("Game Over")]
   [SerializeField] private GameObject gameOverScreen;
   [SerializeField] private AudioClip gameoverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Help")]
    [SerializeField] private GameObject helpScreen;

    [SerializeField] private GameObject endScreen;

    [SerializeField] public LevelLoader ll;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        helpScreen.SetActive(false);
        endScreen.SetActive(false);

    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameoverSound);
    }

    public void Restart()
    {
        ll.LoadNextLevel(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying= false;
        #endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !helpScreen.activeInHierarchy)
        {
            if (pauseScreen.activeInHierarchy)
            {
                pauseGame(false);
            }
            else
            {
                pauseGame(true);
            }

        }
        if(Input.GetKeyDown(KeyCode.F1) && !pauseScreen.activeInHierarchy)
        {
            if (helpScreen.activeInHierarchy)
            {
                showControls(false);
            }
            else
            {
                showControls(true);
            }
        }
    }

    public void showControls(bool status)
    {
        helpScreen.SetActive(status);

        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void pauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void volume()
    {
        SoundManager.instance.changeVolume(0.2f);
    }
    public void music()
    {
        SoundManager.instance.changeMusicVolume(0.2f);
    }
}
