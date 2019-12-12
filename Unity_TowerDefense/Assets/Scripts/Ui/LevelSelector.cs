using UnityEngine;

public class LevelSelector: MonoBehaviour
{
    [SerializeField] private LayerMask cellLayerMask;
    private Camera _camera;
    
    private Level _currentLevelSelected;
    private LevelSelectionUi _levelSelectionUi;

    private void Awake()
    {
        _camera = Camera.main;
        _levelSelectionUi = FindObjectOfType<LevelSelectionUi>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, cellLayerMask))
            {
                Level level = hit.collider.GetComponent<Level>();

                if (level.levelState == LevelState.UNLOCKED || level.levelState == LevelState.FINISHED)
                {
                    _currentLevelSelected = level;
                    _levelSelectionUi.ToggleLevel(_currentLevelSelected.levelData);
                }
            }
        }
    }
}