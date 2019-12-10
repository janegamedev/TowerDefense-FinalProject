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
    
    private void Start()
    {
        upgradeDescription = FindObjectOfType<UpgradeDescription>();

        level = _upgradeSo.level;
        cost = _upgradeSo.cost;
        description = _upgradeSo.description;
        upgradeImage = _upgradeSo.upgradeImage;

        costTmp.text = cost.ToString();
        icon.sprite = upgradeImage;
        icon_shade.sprite = upgradeImage;
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