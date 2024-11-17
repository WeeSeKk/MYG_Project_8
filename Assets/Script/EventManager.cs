using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public static event Action<string, bool> musicChange;
    public static event Action<float> musicSlider;

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

    public static void MusicChange(string musicName, bool next)
    {
        musicChange?.Invoke(musicName, next);
    }
    public static void MusicSlider(float musicTime)
    {
        musicSlider?.Invoke(musicTime);
    }
}
