using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager obj;
    public AudioClip jump, damage, pickUp, walk;
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

    public void PlayPickUp() { PlaySound(pickUp); }
    public void PlayJump() { PlaySound(jump); }
    public void PlayDamage() { PlaySound(damage); }
    public void PlayWalk() { PlaySound(walk); }

    void OnDestroy()
    {
        obj = null;
    }
}
