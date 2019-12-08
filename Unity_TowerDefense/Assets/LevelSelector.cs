using UnityEngine;

public class LevelSelector: MonoBehaviour
{
    [SerializeField] private LayerMask cellLayerMask;
    private Camera _camera;
    
    private Level _currentLevelSelected;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, cellLayerMask))
        {
            Level level = hit.collider.GetComponent<Level>();

            if (level.levelState == LevelState.UNLOCKED)
            {
                _currentLevelSelected = level;
                GameManager.Instance.LoadLevel(_currentLevelSelected.levelData);
            }
        }
    }
}