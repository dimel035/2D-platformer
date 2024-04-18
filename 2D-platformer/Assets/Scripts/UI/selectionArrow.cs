using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip movingSound;
    [SerializeField] private AudioClip interactSound;

    private RectTransform rect;
    private int cPosition;


    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) )
        {
            changePosition(-1);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            changePosition(1);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            OptionInteract();
        }
    }

    private void OptionInteract()
    {
        SoundManager.instance.PlaySound(interactSound);

        options[cPosition].GetComponent<Button>().onClick.Invoke();

    }

    private void changePosition(int _change)
    {
        cPosition += _change;
        if (_change != 0) 
        {
            SoundManager.instance.PlaySound(movingSound);
        }

        if(cPosition < 0)
        {
            cPosition = options.Length - 1;
        }
        else if(cPosition > options.Length - 1)
        {
            cPosition = 0;
        }

        rect.position = new Vector3(rect.position.x, options[cPosition].position.y, 0);
    }


}
