using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioPlayScript
{
    private static AudioSource _audioSource;
    [SerializeField] private static AudioClip calmMusic; 
    [SerializeField] private static AudioClip mediumMusic; 
    [SerializeField] private static AudioClip panicMusic;

    [Range(0,1.0f)]public static float panicLevel;
    private static int _currentMusicLevel = 0;

    public enum SoundClip
    {
        Test1,
        Test2,
        Test3,
    }
    
    public enum MusicClip
    {
        Calm,
        Medium,
        Panic,
    }

    public static void PlaySound(SoundClip sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }
    
    /*public static void PlayMusic(MusicClip music)
    {
        _audioSource.clip = 
        GameObject musicGameObject = new GameObject("Music");
        AudioSource audioSource = musicGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetMusicClip(music));
    }*/

    
    private static AudioClip GetAudioClip(SoundClip sound)
    {
        foreach (AudioManagerScript.SoundAudioClip soundAudioClip in AudioManagerScript.Instance.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
    
    public static AudioClip GetMusicClip(MusicClip music)
    {
        foreach (AudioManagerScript.MusicAudioClip musicAudioClip in AudioManagerScript.Instance.musicAudioClipArray)
        {
            if (musicAudioClip.music == music)
            {
                return musicAudioClip.audioClip;
            }
        }
        Debug.LogError("Music " + music + " not found!");
        return null;
    }
    /*
    private void CurrentMusic(int level)
    {
        if (_currentMusicLevel == level) return;
        _currentMusicLevel = level;
        StartMusic(level);
    }

    private void StartMusic(int level)
    {
        if (level == 0)
        {
            _audioSource.Play();
        }
    }
    */
}