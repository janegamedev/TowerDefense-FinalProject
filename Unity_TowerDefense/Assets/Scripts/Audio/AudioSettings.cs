using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private float _musicValue;
    private float _sfxValue;
    private void Start()
    {
        _musicValue = AudioManager.Instance.musicAudioSource.volume;
        musicSlider.value = _musicValue * 100;
        
        _sfxValue = AudioManager.Instance.sfxAudioSource.volume;
        sfxSlider.value = _sfxValue * 100;
    }

    private void Update()
    {
        if (Math.Abs(musicSlider.value - _musicValue*100) > 1f)
        {
            _musicValue = musicSlider.value / 100;
            AudioManager.Instance.SetMusicVolume(_musicValue);
        }

        if (Math.Abs(sfxSlider.value - _sfxValue*100) > 1f)
        {
            _sfxValue = sfxSlider.value / 100;
            AudioManager.Instance.SetSxfVolume(_sfxValue);
        }
    }
}
