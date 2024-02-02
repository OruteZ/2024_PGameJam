using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility.Manager;

public class Option : MonoBehaviour
{
    private Slider sfxVolumeSlider;
    private Slider bgmVolumeSlider;
    
    [SerializeField] private GameObject sfxVolumeSliderObj;
    [SerializeField] private GameObject bgmVolumeSliderObj;
    
    private void Awake()
    { 
        sfxVolumeSlider = sfxVolumeSliderObj.GetComponent<Slider>();
        bgmVolumeSlider = bgmVolumeSliderObj.GetComponent<Slider>();
        
        sfxVolumeSlider.value = SoundManager.Instance.SfxVolume;
        bgmVolumeSlider.value = SoundManager.Instance.BgmVolume;
    }
    
    public void SetSfxVolume()
    {
        SoundManager.Instance.SetSFXVolume(sfxVolumeSlider.value);
    }
    
    public void SetBgmVolume()
    {
        SoundManager.Instance.SetBGMVolume(bgmVolumeSlider.value);
    }
}
