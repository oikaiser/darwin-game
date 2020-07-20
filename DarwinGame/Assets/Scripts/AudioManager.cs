using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager obj;
    public AudioClip coin;
    private bool isMuted;
    private AudioSource _audioSource;

    void Awake()
    {
        obj = this;
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isMuted = PlayerPrefs.GetInt("MUTED") == 1;
        AudioListener.pause = isMuted;
    }

    public void Mute() 
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        PlayerPrefs.GetInt("MUTED", isMuted ? 1 : 0);
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void PlayCoin() { PlaySound(coin); }

    void OnDestroy()
    {
        obj = null;
    }
}
