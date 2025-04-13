using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Musik : MonoBehaviour
{
    public AudioMixer audioMixer; // Drag deinen Mixer hier rein im Inspector
    public Slider volumeSlider;   // Dein UI-Slider
    const string Mixer_Music = "Master";

    private void Start()
    {
        // Initialwert setzen
        float volume;
        audioMixer.GetFloat("Master", out volume);
        volumeSlider.value = Mathf.Pow(10, volume / 20f); // Umwandlung von dB zu linear
    }

    public void SetVolume(float value)
    {
        // Lautstärke im Mixer setzen (linear → dB)
        float volumeInDb = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("Master", volumeInDb);
    }
}

