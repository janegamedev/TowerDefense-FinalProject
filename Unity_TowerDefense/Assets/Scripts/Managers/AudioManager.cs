using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip levelSelectionMusic;
    [SerializeField] private AudioClip levelMusic;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;
    
    private AudioSource _musicAudioSource;
    private AudioSource _sfxAudioSource;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _musicAudioSource = GetComponent<AudioSource>();
        HandleGameStateChanged(GameState.MENU, GameState.MENU);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }
    
    private void HandleGameStateChanged(GameState previousState, GameState currentState)
    {
        if (currentState == GameState.MENU)
        {
            ChangeMusic(menuMusic);
        }
        else if( currentState == GameState.SELECTION)
        {
            ChangeMusic(levelSelectionMusic);
        }
        else if(currentState == GameState.RUNNING)
        {
            ChangeMusic(levelMusic);
        }
        else if(currentState == GameState.END)
        {
            _musicAudioSource.Stop();
            _sfxAudioSource.Stop();
        }

        if (currentState == GameState.PAUSED)
        {
            _musicAudioSource.Pause();
            _sfxAudioSource.Pause();
        }
        else if(previousState == GameState.PAUSED)
        {
            _musicAudioSource.Play();
            _sfxAudioSource.Play();
        }
    }

    private void ChangeMusic(AudioClip clip)
    {
        _musicAudioSource.Stop();
        _musicAudioSource.clip = clip;
        _musicAudioSource.Play();
    }
    
    public void SetMusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
    
    public void SetSxfVolume(float sliderValue)
    {
        musicMixer.SetFloat("SfxVol", Mathf.Log10(sliderValue) * 20);
    }
}
