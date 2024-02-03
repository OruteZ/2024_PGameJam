using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility.Manager;

public class Option : MonoBehaviour
{
    private Slider _sfxVolumeSlider;
    private Slider _bgmVolumeSlider;
    
    [SerializeField] private GameObject sfxVolumeSliderObj;
    [SerializeField] private GameObject bgmVolumeSliderObj;
    
    private void Awake()
    { 
        _sfxVolumeSlider = sfxVolumeSliderObj.GetComponent<Slider>();
        _bgmVolumeSlider = bgmVolumeSliderObj.GetComponent<Slider>();
        
        _sfxVolumeSlider.value = SoundManager.Instance.SfxVolume;
        _bgmVolumeSlider.value = SoundManager.Instance.BgmVolume;
    }
    
    public void SetSfxVolume()
    {
        SoundManager.Instance.SetSFXVolume(_sfxVolumeSlider.value);
    }
    
    public void SetBgmVolume()
    {
        SoundManager.Instance.SetBGMVolume(_bgmVolumeSlider.value);
    }
}
