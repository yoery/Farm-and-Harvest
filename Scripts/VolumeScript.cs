using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        // Initialize the volume slider to the current volume of the audio source
        volumeSlider.value = audioSource.volume;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
