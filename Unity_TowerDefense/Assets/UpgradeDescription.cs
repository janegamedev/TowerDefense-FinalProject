using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradeDescription : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image shadeImage;

    [SerializeField] private TextMeshProUGUI upgradeDescription;

    private Vector3 startPosition;
    public Vector3 toPosition;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    public void Init(Upgrade upgrade)
    {
        image.sprite = upgrade.upgradeImage;
        shadeImage.sprite = upgrade.upgradeImage;

        upgradeDescription.text = upgrade.description;
        
        ToggleAnimationIn();
    }

    private void ToggleAnimationIn()
    {
        LeanTween.moveLocal(gameObject, toPosition, 0.25f).setEase(LeanTweenType.easeInQuad);
    }

    public void ToggleAnimationOut()
    {
        LeanTween.moveLocal(gameObject, startPosition, 0.25f).setEase(LeanTweenType.easeInQuad);
    }
}
