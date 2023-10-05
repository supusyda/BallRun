using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;



public class VolumSetting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] TMP_Text text;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXslider;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("musicValue") || PlayerPrefs.HasKey("SFXValue"))
        {
            this.LoadVolum();

        }
        else
        {
            this.SetVolum();
            SetSFXVolum();
        }
    }
    public void SetVolum()
    {
        this.audioMixer.SetFloat("music", Mathf.Log10(this.musicSlider.value) * 20);
        PlayerPrefs.SetFloat("musicValue", this.musicSlider.value);
    }
    public void SetSFXVolum()
    {
        this.audioMixer.SetFloat("SFX", Mathf.Log10(this.SFXslider.value) * 20);
        PlayerPrefs.SetFloat("SFXValue", this.SFXslider.value);
    }
    public void LoadVolum()
    {
        this.musicSlider.value = PlayerPrefs.GetFloat("musicValue");
        this.SFXslider.value = PlayerPrefs.GetFloat("SFXValue");

        this.SetVolum();
        this.SetSFXVolum();

    }


}
