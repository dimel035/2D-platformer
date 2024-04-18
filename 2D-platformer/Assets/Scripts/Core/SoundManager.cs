using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;
    private AudioSource musicSource;

    private void Awake()
    {
        source= GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        //keep this sound object in every level
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance !=null && instance != this)
        {
            Destroy(gameObject);
        }

        changeVolume(0);
        changeMusicVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

    public void changeVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, source);
    }
    public void changeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource);
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        //Get initial value of volume and change it
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        //Check if we reached the maximum or minimum value
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //Assign final value
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        //Save final value to player prefs
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}
