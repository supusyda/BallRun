using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource musicAudio;
    [SerializeField] AudioSource SFXAudio;
    public AudioClip background;
    public AudioClip death;
    public AudioClip portalIn;
    public AudioClip portalOut;
    public AudioClip walk;
    public AudioClip checkPoint;
    public VolumSetting volumSetting;

    // public AudioClip background;'
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {

        if (PlayerPrefs.HasKey("musicValue") || PlayerPrefs.HasKey("SFXValue"))
        {
            this.volumSetting.LoadVolum();

        }
        else
        {
            this.volumSetting.SetVolum();
            volumSetting.SetSFXVolum();
        }
        if (musicAudio != null)
        {
            musicAudio.clip = this.background;
            musicAudio.Play();
        }
    }
    public void PlaySFX(AudioClip audioClip)
    {
        this.SFXAudio.PlayOneShot(audioClip);
    }




}
