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

    public static AudioManager I() {
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
    public List<float> AudioSettingList;
    
    void Start() {
        audioList = new List<AudioClip>();
        for (int i = 0; i < AudioListInspector.Count; i++) audioList.Add(null);
        for (int i = 0; i < AudioListInspector.Count; i++) {
            audioList[(int)AudioListInspector[i].audioType] = AudioListInspector[i].audioClip;
        }
    }

    void Update() {
        PlaySound(AudioType.BGM, AudioSetting.Music);
    }

    public void StopSound(AudioType audio) {
        if (audio == AudioType.Null) return;
        for (int i = 0; i < AudioSources.Count; i++) {
            if (AudioSources[i].clip == audioList[(int)audio]) {
                AudioSources[i].Stop();
                AudioSources[i].clip = null;
            }
        }
    }

    public void PlaySound(AudioType audio, AudioSetting audioSetting = AudioSetting.SFX, AudioPlayType audioPlayType = AudioPlayType.Yield) {
        if (audio == AudioType.Null) return;
        switch (audioPlayType) {
            case AudioPlayType.Override:
                StopSound(audio);
                break;
            case AudioPlayType.Yield:
                if (checkPlaying(audio)) return;
                break;
            case AudioPlayType.Overlap:
                break;
        }
        playFirst(audio, audioSetting);
    }


    bool checkPlaying(AudioType audio) {
        for (int i = 0; i < AudioSources.Count; i++) {
            if (!AudioSources[i].isPlaying) continue;
            if (AudioSources[i].clip == audioList[(int)audio]) return true;
        }
        return false;
    }

    void playFirst(AudioType audio, AudioSetting audioSetting) {
        Debug.Log("try playfirst");
        for (int i = 0; i < AudioSources.Count; i++) {
            if (!AudioSources[i].isPlaying) {
                AudioSources[i].volume = AudioSettingList[(int)audioSetting];
                AudioSources[i].clip = audioList[(int)audio];
                AudioSources[i].Play();
                Debug.Log("play??");
                return;
            }
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