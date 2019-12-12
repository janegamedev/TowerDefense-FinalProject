using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressSceneLoader : MonoBehaviour
{
    public Events.SceneLoadCompleted OnSceneLoadedCompleted; 
    private AsyncOperation _asyncOperation;
    public Canvas _canvas;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        UpdateProgress(0);
        _canvas.gameObject.SetActive(true);

        StartCoroutine(BeginLoad(sceneName));
    }

    private IEnumerator BeginLoad(string sceneName)
    {
        _asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!_asyncOperation.isDone)
        {
            UpdateProgress(_asyncOperation.progress);
            yield return null;
        }
        
        UpdateProgress(_asyncOperation.progress);
        _asyncOperation = null;
        _canvas.gameObject.SetActive(false);
        OnSceneLoadedCompleted.Invoke();
    }

    private void UpdateProgress(float progress)
    {
        _slider.value = progress;
    }
}
