using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider efxSlider;

    private void Start()
    {
        if (SoundManager.Instance != null)
        {
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.optionData.BgmOn)
                    bgmSlider.value = 1f;
                else
                    bgmSlider.value = 0f;

                if (GameManager.Instance.optionData.EfxOn)
                    efxSlider.value = 1f;
                else
                    efxSlider.value = 0f;
            }
        }
    }

    public void BgmSliderChange()
    {
        if (bgmSlider.value != 1f)
        {
            SoundManager.Instance.backgroundAudio.volume = 0f;
            GameManager.Instance.optionData.BgmOn = false;
        }
        else
        {
            SoundManager.Instance.backgroundAudio.volume = 0.8f;
            GameManager.Instance.optionData.BgmOn = true;
        }
    }

    public void EfxSliderChange()
    {
        if (efxSlider.value != 1f)
        {
            SoundManager.Instance.effectAudio.volume = 0f;
            GameManager.Instance.optionData.EfxOn = false;
        }
        else
        {
            SoundManager.Instance.effectAudio.volume = 0.4f;
            GameManager.Instance.optionData.EfxOn = true;
        }
    }
}
