using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionPanel : MonoBehaviour
{
    public Button[] buttons;
    public TextMeshProUGUI[] costs;
    public Transform cell;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if(cell!=null)
            transform.position = _camera.WorldToScreenPoint(cell.position);
        
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
}
