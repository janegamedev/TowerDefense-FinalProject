using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionPanel : MonoBehaviour
{
    public Button[] buttons;
    public TextMeshProUGUI[] costs;
    
    private TowerTile _tile;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        for (int i = 0; i < costs.Length; i++)
        {
            int.TryParse(costs[i].text, out int result);

            if (PlayerStats.Instance.Coins < result)
            {
                buttons[i].interactable = false;
            }
            else
            {
                buttons[i].interactable = true;
            }
        }
    }

    public void Init(TowerTile tile)
    {
        _tile = tile;
        transform.position = Camera.main.WorldToScreenPoint(_tile.transform.position);
    }

    private void OnDisable()
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
