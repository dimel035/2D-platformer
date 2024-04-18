using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeText : MonoBehaviour
{
    [SerializeField] private string sourceName;
    [SerializeField] private string textStart;
    private Text txt;

    private void Awake()
    {
        txt = GetComponent<Text>();
    }

    private void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        float volume = PlayerPrefs.GetFloat(sourceName) * 100;
        txt.text = textStart + volume.ToString();
    }
}
