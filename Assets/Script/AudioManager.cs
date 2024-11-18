using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public List<AudioClip> playlist;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource soundAudioSource;
    const string MUSIC_VOLUME = "MusicVolume";
    const string MASTER_VOLUME = "MasterVolume";
    bool music;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        PlayMusic("start");
        music = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicAudioSource.isPlaying && music)
        {
            PlayMusic("next");
        }
    }

    void OnEnable()
    {
        
    }

    void PlayMusic(string music)
    {
        if (music == "next")
        {
            for (int i = 0; i <= playlist.Count - 1; i++)
            {
                if (musicAudioSource.clip == playlist[i])
                {
                    if (i == playlist.Count - 1)
                    {
                        int nextIndex = 0;
                        EventManager.MusicChange(playlist[nextIndex + 1].name, true);
                        EventManager.MusicSlider(playlist[nextIndex].length);
                        musicAudioSource.clip = playlist[nextIndex];
                        musicAudioSource.Play();
                        break;
                    }
                    else
                    {
                        int nextIndex = i + 1;
                        if (nextIndex == playlist.Count - 1)
                        {
                            EventManager.MusicChange(playlist[0].name, true);
                        }
                        else
                        {
                            EventManager.MusicChange(playlist[nextIndex + 1].name, true);
                        }
                        EventManager.MusicSlider(playlist[nextIndex].length);
                        musicAudioSource.clip = playlist[nextIndex];
                        musicAudioSource.Play();
                        break;
                    }
                }
            }
        }
        else if (music == "previous")
        {
            for (int i = 0; i <= playlist.Count - 1; i++)
            {
                if (musicAudioSource.clip == playlist[i])
                {
                    if (i == 0)
                    {
                        int nextIndex = playlist.Count - 1;
                        EventManager.MusicChange(playlist[nextIndex - 1].name, false);
                        EventManager.MusicSlider(playlist[nextIndex].length);
                        musicAudioSource.clip = playlist[nextIndex];
                        musicAudioSource.Play();
                        break;
                    }
                    else
                    {
                        int nextIndex = i - 1;
                        if (nextIndex == 0)
                        {
                            EventManager.MusicChange(playlist[playlist.Count - 1].name, false);
                        }
                        else
                        {
                            EventManager.MusicChange(playlist[nextIndex - 1].name, false);
                        }
                        EventManager.MusicSlider(playlist[nextIndex].length);
                        musicAudioSource.clip = playlist[nextIndex];
                        musicAudioSource.Play();
                        break;
                    }
                }
            }
        }
        else
        {
            EventManager.MusicChange(playlist[0].name, true);
            EventManager.MusicSlider(playlist[0].length);
            musicAudioSource.clip = playlist[0];
            musicAudioSource.Play();
        }
    }

    public void PausePlayMusic(Button button)
    {
        if (button.name == "PlayButton")
        {
            musicAudioSource.Play();
            music = true;
        }
        else
        {
            musicAudioSource.Pause();
            music = false;
        }
    }

    public void ChangeMusic(Button button)
    {
        button.interactable = false;
        if (button.name == "NextButton")
        {
            PlayMusic("next");
        }
        else
        {
            PlayMusic("previous");
        }
        button.interactable = true;
    }

    public void MasterSliderValue(float value)
    {
        audioMixer.SetFloat(MASTER_VOLUME, Mathf.Log10(value) * 20);
    }

    public void MusicSliderValue(float value)
    {
        audioMixer.SetFloat(MUSIC_VOLUME, Mathf.Log10(value) * 20);
    }
}