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
        _musicValue = musicSlider.value;
        _sfxValue = sfxSlider.value;
    }

    private void Update()
    {
        if (Math.Abs(musicSlider.value - _musicValue) > 0.01f)
        {
            _musicValue = musicSlider.value;
            AudioManager.Instance.SetMusicVolume(_musicValue);
        }

        if (Math.Abs(sfxSlider.value - _sfxValue) > 0.01f)
        {
            _sfxValue = sfxSlider.value;
            AudioManager.Instance.SetSxfVolume(_sfxValue);
        }
    }
}
