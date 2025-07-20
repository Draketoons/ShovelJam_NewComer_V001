using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip currentMusic;
    public AudioClip loopSound;
    public AudioClip startSound;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        PlayStartSound();
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
