using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://mentum.tistory.com/221
public class SoundManager : Singleton<SoundManager>
{
    public AudioSource backgroundAudio;
    public AudioSource effectAudio;

    public AudioClip[] BGMSounds;

    public AudioClip[] PlayerEFXSounds;
    public AudioClip MonsterEFXSounds;

    public AudioClip[] UiEFXSounds;

    private void Start()
    {
        if(GameManager.Instance != null)
        {
            if (GameManager.Instance.optionData.BgmOn)
                backgroundAudio.volume = 0.6f;
            else
                backgroundAudio.volume = 0f;

            if (GameManager.Instance.optionData.EfxOn)
                effectAudio.volume = 0.3f;
            else
                effectAudio.volume = 0f;
        }
    }

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
            backgroundAudio.volume = 0.6f;
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
            effectAudio.volume = 0.3f;
        else
            effectAudio.volume = 0.0f;
    }
}
