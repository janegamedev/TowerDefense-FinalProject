using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDescription : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image shadeImage;

    [SerializeField] private TextMeshProUGUI upgradeDescription;

    private Vector3 startPosition;
    public Vector3 toPosition;

    private int moveInId;
    private int moveOutId;
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
    

    public void ToggleAnimationIn()
    {
        LeanTween.reset();
        moveInId = LeanTween.moveLocal(gameObject, toPosition, 0.25f).setEase(LeanTweenType.easeInQuad).id;
    }

    public void ToggleAnimationOut()
    {
        LeanTween.reset();
        moveOutId = LeanTween.moveLocal(gameObject, startPosition, 1f).setEase(LeanTweenType.easeInQuad).id;
    }
}
