using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip levelSelectionMusic;
    [SerializeField] private AudioClip levelMusic;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;
    
    private AudioSource _audioSource;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
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

        if (currentState == GameState.PAUSED)
        {
            _audioSource.Pause();
        }
        else if(previousState == GameState.PAUSED)
        {
            _audioSource.Play();
        }
    }

    private void ChangeMusic(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
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
