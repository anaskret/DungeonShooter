using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    private GameObject audio;

    private void Start()
    {
        audio = GameObject.FindGameObjectWithTag("Music");
    }

    public void SetLevel(float sliderValue)
    {
        audio.GetComponent<AudioSource>().volume = sliderValue;
    }
}
