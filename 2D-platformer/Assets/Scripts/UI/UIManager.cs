using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject gameOverScreen;
   [SerializeField] private AudioClip gameoverSound;

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameoverSound);
    }
}
