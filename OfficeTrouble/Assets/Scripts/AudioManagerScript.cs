using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    #region Singleton

    

    
    private static AudioManagerScript _instance;

    public static AudioManagerScript Instance
    {
        get {
            return _instance;
        }
    }
    #endregion

    private AudioPlayScript.MusicClip _currentMusic = AudioPlayScript.MusicClip.Medium;
    private AudioSource _track1, _track2;
    private bool _track1Playing = true;
    
    public SoundAudioClip[] soundAudioClipArray;
    
    public MusicAudioClip[] musicAudioClipArray;

    [SerializeField] private bool ModifyPitch;
    [SerializeField] private bool gameLost;
    [SerializeField] private bool gameStart;
        
    [Range(0,1.0f)]public float panicLevel;
    private IValueChanged valueChangedEvent;
    
    [System.Serializable]
    public class SoundAudioClip
    {
        public AudioPlayScript.SoundClip sound;
        public AudioClip audioClip;
    }
    
    [System.Serializable]
    public class MusicAudioClip
    {
        public AudioPlayScript.MusicClip music;
        public AudioClip audioClip;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        _track1 = gameObject.AddComponent<AudioSource>();
        _track1.loop = true;
        _track2 = gameObject.AddComponent<AudioSource>();
        _track2.loop = true;
        _track1Playing = true;
        //AudioSource.PlayClipAtPoint(AudioPlayScript.GetMusicClip(AudioPlayScript.MusicClip.TuneUp), Camera.main.transform.position);

    }
    public void Update()
    {
        if (gameLost)
        {
            StartCoroutine(LostMusic());
            gameLost = false;
            return;
        }

        if (gameStart)
        {
            StartMusic();
            gameStart = false;
            return;
        }
        if (panicLevel < 0.30f)
        {
            PlayMusic(AudioPlayScript.MusicClip.Calm);
        } 
        else if (panicLevel > 0.40f && panicLevel < 0.60f)
        {
            PlayMusic(AudioPlayScript.MusicClip.Medium);
        }
        else if (panicLevel > 0.70f && panicLevel < 1.0f)
        {
            PlayMusic(AudioPlayScript.MusicClip.Panic);
        }

        if (ModifyPitch)
        {
            float pitch = 0.0f;
            switch (_currentMusic)
            {
                case AudioPlayScript.MusicClip.Calm:
                    pitch = panicLevel * 2.5f + 0.5f;
                    break;
                case AudioPlayScript.MusicClip.Medium:
                    pitch = (panicLevel-0.3f) * 2.5f + 0.5f;
                    break;
                case AudioPlayScript.MusicClip.Panic:
                    pitch = (panicLevel-0.6f) * 2.5f + 0.5f;
                    break;
            }
            //float pitch = ((panicLevel % 0.335f) * 3) + 0.5f;
            _track1.pitch = pitch;
            _track2.pitch = pitch;
        }
    }

    private void PlayMusic(AudioPlayScript.MusicClip music)
    {
        if (_currentMusic == music) return;
        _currentMusic = music;
        PlayMusicClip(_currentMusic);
    }

    private void PlayMusicClip(AudioPlayScript.MusicClip music)
    {
        StopAllCoroutines();

        StartCoroutine(FadeTrack(music));
        //_track1Playing = !_track1Playing;
    }

    private IEnumerator FadeTrack(AudioPlayScript.MusicClip music)
    {
        float timeToFade = 1.25f;
        float timeElapsed = 0;
        if (_track1Playing)
        {
            _track2.clip = AudioPlayScript.GetMusicClip(music);
            _track2.Play();

            while (timeElapsed < timeToFade)
            {
                _track2.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                _track1.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            _track1.Stop();
        }
        else
        {
            _track1.clip = AudioPlayScript.GetMusicClip(music);
            _track1.Play();
            
            while (timeElapsed < timeToFade)
            {
                _track1.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                _track2.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            
            _track2.Stop();
        }
        _track1Playing = !_track1Playing;
    }

    private IEnumerator LostMusic()
    {
        float timeToFade = 2.0f;
        float timeElapsed = 0;
        while (timeElapsed < timeToFade)
        {
            if (_track1Playing)
            {
                _track1.pitch = (timeToFade - timeElapsed) / timeToFade;
            }
            else
            {
                _track2.pitch = (timeToFade - timeElapsed) / timeToFade;
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (_track1Playing)
        {
            _track1.Stop();
        }
        else
        {
            _track2.Stop();
        }
}

    public void StartMusic()
    {
        _track1.pitch = 1;
        _track2.pitch = 1;
        
    }

    public void GameIsLost()
    {
        gameLost = true;
    }

    public void PlaySound(AudioPlayScript.SoundClip sound)
    {
        AudioPlayScript.PlaySound(sound);
    }
    
    public void Register(IValueChanged value)
    {
        valueChangedEvent = value;
        valueChangedEvent.ValueChanged += ChangePanicLevel;
    }

    private void ChangePanicLevel(float newValue)
    {
        panicLevel = newValue;
    }
    
    public void OnDestroy()
    {
        if (valueChangedEvent != null)
        {
            valueChangedEvent.ValueChanged -= ChangePanicLevel;
        }

    }
    
}
