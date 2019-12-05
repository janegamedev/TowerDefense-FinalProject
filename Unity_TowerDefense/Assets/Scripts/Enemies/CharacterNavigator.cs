using UnityEngine;

public class CharacterNavigator : MonoBehaviour
{
    private CharacterNavigationController _controller;
    private RoadTile _currentTile;
    public RoadTile CurrentTile
    {
        get => _currentTile;
        set => _currentTile = value;
    }

    public event System.Action OnDestroy;

    private void Awake()
    {
        _controller = GetComponent<CharacterNavigationController>();
    }

    private void Update()
    {
        if (_controller.reachedDestination)
        {
            if (_currentTile.nextTile != null)
            {
                _currentTile = _currentTile.nextTile;
                _controller.SetDestination(_currentTile.transform.position);
            }
            else
            {
                Die();
            }
        }
    }

    void Die()
    {
        OnDestroy?.Invoke();
        PlayerStats.Instance.ChangeLives(1);
        Destroy(gameObject);
    }
}
