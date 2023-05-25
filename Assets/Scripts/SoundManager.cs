using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [SerializeField]
    private AudioSource soundsFX;
    [SerializeField]
    private AudioSource soundsBG;

    public Sound[] sounds;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Play(Sounds _soundType)
    {
        Sound s = Array.Find(sounds, i => i.soundType == _soundType);
        AudioClip clip = s.soundClip;
        float _volume = s.volume;
        soundsFX.clip = clip;
        soundsFX.volume = _volume;
        soundsFX.Play();
    }

    public void PlayBG(Sounds _soundType)
    {
        Sound s = Array.Find(sounds, i => i.soundType == _soundType);
        AudioClip clip = s.soundClip;
        float _volume = s.volume;
        soundsBG.clip = clip;
        soundsBG.volume = _volume;
        soundsBG.Play();
    }

    [Serializable]
    public class Sound
    {
        public Sounds soundType;
        public AudioClip soundClip;
        [Range(0f, 1f)]
        public float volume;
    }

    public enum Sounds
    {
        ButtonHover,
        ButtonPressed,
        LobbyMusic,
        GameplayMusic,
        Pickup,
        Death,
        Spawn,
        BadPickup
    }
}
