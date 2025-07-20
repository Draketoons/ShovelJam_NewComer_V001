using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip currentMusic;
    public AudioClip loopSound;
    public AudioClip startSound;
    public bool playStartSound;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (playStartSound)
        {
            PlayStartSound();
        }
        else
        {
            StartMusic();
        }
    }

    private void Update()
    {
        if (source.clip == startSound && !source.isPlaying)
        {
            StartMusic();
        }
    }

    public AudioSource GetAudioSource()
    {
        return source;
    }

    public void StartMusic()
    {
        source.loop = true;
        source.clip = currentMusic;
        source.Play();
    }

    public void PlayLoopSound()
    {
        source.loop = false;
        source.clip = loopSound;
        source.Play();
    }

    public void PlayStartSound()
    {
        source.loop = false;
        source.clip = startSound;
        source.Play();
    }
}
