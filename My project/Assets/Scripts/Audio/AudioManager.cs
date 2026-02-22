using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    // Singleton

    private static AudioManager _instance;

    private AudioManager() {
        _instance = this;
    }

    public static AudioManager instance() {
        if (_instance == null) {
            AudioManager instance = new AudioManager();
            _instance = instance;
        }
        return _instance;
    }

    // for the inspector
    [Serializable]
    public struct AudioInspectorStruct {
        public AudioType audioType;
        public AudioClip audioClip;
    }

    // inspector fields
    public List<AudioInspectorStruct> AudioListInspector;
    private List<AudioClip> audioList;

    // use first source for background music only
    public List<AudioSource> AudioSources;

    // player settings
    public float BGVolume;
    public float SFXVolume;
    
    void Start() {
        audioList = new List<AudioClip>(AudioListInspector.Count);
        for (int i = 0; i < AudioListInspector.Count; i++) {
            audioList[(int)AudioListInspector[i].audioType] = AudioListInspector[i].audioClip;
        }
    }

    void Update() {
        /*if (musicSource.volume != musicTarget)
        {
            if (musicSource.volume < musicTarget)
            {
                musicSource.volume += Mathf.Min(Time.deltaTime / 10f, musicTarget - musicSource.volume);
            }
            else musicSource.volume -= Mathf.Min(Time.deltaTime / 10f, musicSource.volume - musicTarget);
        }*/
    }


    bool checkPlaying(AudioType audio) {
        for (int i = 0; i < AudioSources.Count; i++) {
            if (!AudioSources[i].isPlaying) continue;
            if (AudioSources[i].clip == audioList[(int)audio]) return true;
        }
        return false;
    }

    void playFirst(AudioType audio) {
        for (int i = 0; i < AudioSources.Count; i++) {
            if (!AudioSources[i].isPlaying)
            {
                AudioSources[i].volume = SFXVolume;
                AudioSources[i].clip = audioList[(int)audio];
                AudioSources[i].Play();
                return;
            }
        }
    }

    public void StopSound(AudioType audio) {
        if (audio == AudioType.Null) return;
        for (int i = 0; i < AudioSources.Count; i++) {
            if (AudioSources[i].clip == audioList[(int)audio]) {
                AudioSources[i].Stop();
            }
        }
    }

    public void PlaySound(AudioType audio, AudioPlayType audioPlayType = AudioPlayType.Override) {
        if (audio == AudioType.Null) return;
        switch (audioPlayType) {
            case AudioPlayType.Override:
                break;
            case AudioPlayType.Yield:
            case AudioPlayType.Overlap:
                break;
        }
        if (!checkPlaying(audio)) {
            playFirst(audio);
        }
    }

    /*public void PlayBackground(BackgroundMusic audio, float volume = 0.5f)
    {
        if (!musicSource.isPlaying)
        {
            musicSource.clip = backgroundList[(int)audio];
            musicSource.Play();
        }
        else
        {
            if (musicSource.clip != backgroundList[(int)audio])
            {
                musicSource.clip = backgroundList[(int)audio];
                musicSource.Play();
            }
        }
    }

    public void BackgroundVolume(float volume)
    {
        musicTarget = volume;
    }*/
}