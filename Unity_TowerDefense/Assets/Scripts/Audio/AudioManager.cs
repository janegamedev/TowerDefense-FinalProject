using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip levelSelectionMusic;
    [SerializeField] private AudioClip levelMusic;

    [SerializeField] public AudioSource musicAudioSource;
    [SerializeField] public AudioSource sfxAudioSource;

    public AudioClip waveStartSfx;
    public AudioClip liveDecreaseSfx;
    public AudioClip bountySfx;

    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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
            musicAudioSource.Stop();
            sfxAudioSource.Stop();
        }

        if (currentState == GameState.PAUSED)
        {
            musicAudioSource.Pause();
            sfxAudioSource.Pause();
        }
        else if(previousState == GameState.PAUSED)
        {
            musicAudioSource.Play();
            sfxAudioSource.Play();
        }
    }

    private void ChangeMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }
    
    public void SetMusicVolume(float sliderValue)
    {
        musicAudioSource.volume = sliderValue;
        /*musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);*/
    }
    
    public void SetSxfVolume(float sliderValue)
    {
        sfxAudioSource.volume = sliderValue;
        /*musicMixer.SetFloat("SfxVol", Mathf.Log10(sliderValue) * 20);*/
    }

    public void PlayerSfx(AudioClip sfx)
    {
        sfxAudioSource.Stop();
        sfxAudioSource.clip = sfx;
        sfxAudioSource.Play();
    }
}
