using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UpgradeSO _upgradeSo;
    
    public UpgradeState upgradeState;
    
    [HideInInspector] public int level;
    [HideInInspector] public int cost;
    [HideInInspector] public string description;
    [HideInInspector] public Sprite upgradeImage;

    private UpgradeDescription upgradeDescription;

    [SerializeField] private TextMeshProUGUI costTmp;
    [SerializeField] private Image icon;
    [SerializeField] private Image icon_shade;

    private Button _button;
    
    private void Start()
    {
        _button = GetComponent<Button>();
        upgradeDescription = FindObjectOfType<UpgradeDescription>();

        level = _upgradeSo.level;
        cost = _upgradeSo.cost;
        description = _upgradeSo.description;
        upgradeImage = _upgradeSo.upgradeImage;

        costTmp.text = cost.ToString();
        icon.sprite = upgradeImage;
        icon_shade.sprite = upgradeImage;
    }

    private void Update()
    {
        if (upgradeState == UpgradeState.UNLOCKED)
        {
            if (Game.Instance.stars >= cost)
            {
                _button.interactable = true;
            }
            else
            {
                _button.interactable = false;
            }
        }
        else
        {
            if (upgradeState == UpgradeState.BOUGHT && costTmp.transform.parent.gameObject.activeSelf)
            {
                costTmp.transform.parent.gameObject.SetActive(false);
            }
            
            _button.interactable = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        upgradeDescription.Init(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        upgradeDescription.ToggleAnimationOut();
    }
}