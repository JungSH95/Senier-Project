using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource backgroundAudio;
    public AudioSource effectAudio;

    public AudioClip[] BGMSounds;
    public AudioClip[] EFXSounds;

    public void BackGoundPlay(AudioClip clip)
    {
        if (clip == null)
            return;

        backgroundAudio.clip = clip;
        backgroundAudio.Play();
    }
    public void EffectPlay(AudioClip clip)
    {
        if (clip == null)
            return;

        effectAudio.clip = clip;
        effectAudio.Play();
    }

    public void BgmSoundOnOff()
    {
        if (backgroundAudio == null)
            return;

        if (backgroundAudio.volume == 0.0f)
        {
            backgroundAudio.volume = 1.0f;
            BackGoundPlay(backgroundAudio.clip);
        }
        else
        {
            backgroundAudio.Stop();
            backgroundAudio.volume = 0.0f;
        }
    }

    public void EfxSoundOnOff()
    {
        if (effectAudio == null)
            return;

        if (effectAudio.volume == 0.0f)
            effectAudio.volume = 1.0f;
        else
            effectAudio.volume = 0.0f;
    }
}
