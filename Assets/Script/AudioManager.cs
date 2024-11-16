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
    int musicCount = 0;
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

    }

    // Update is called once per frame
    void Update()
    {
        if (!musicAudioSource.isPlaying && music)
        {
            PlayMusic("next");
            music = false;
        }
    }

    void OnEnable()
    {
        music = true;
        //StartCoroutine(PlayMusic());
    }

    void PlayMusic(string music)
    {
        if (music == "next")
        {
            Debug.Log("next");
            if (musicCount == playlist.Count - 1)
            {
                EventManager.MusicChange(playlist[0].name, true);
                EventManager.MusicSlider(playlist[1].length);
                musicAudioSource.clip = playlist[musicCount];
                musicAudioSource.Play();
                musicCount = 0;
            }
            else
            {
                EventManager.MusicChange(playlist[musicCount + 1].name, true);
                EventManager.MusicSlider(playlist[musicCount].length);
                musicAudioSource.clip = playlist[musicCount];
                musicAudioSource.Play();
                musicCount++;
            }
        }
        else if (music == "previous")
        {
            Debug.Log("previous");
            if (musicCount < 1)
            {
                musicCount = playlist.Count - 1;
                EventManager.MusicChange(playlist[musicCount].name, false);
                //EventManager.MusicSlider(playlist[musicCount].length);
                musicAudioSource.clip = playlist[musicCount];
                musicAudioSource.Play();
            }
            else
            {
                EventManager.MusicChange(playlist[musicCount - 1].name, false);
                //EventManager.MusicSlider(playlist[musicCount].length);
                musicAudioSource.clip = playlist[musicCount];
                musicAudioSource.Play();
            }
            musicCount--;
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
        Debug.Log("testtttstststst");
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
}