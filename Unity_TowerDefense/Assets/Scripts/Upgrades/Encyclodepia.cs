using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Encyclodepia : MonoBehaviour
{
    [SerializeField] private Image enemyImage;
    [SerializeField] private Image enemyImageShade;
    [SerializeField] private TextMeshProUGUI enemyDescription;
    [SerializeField] private TextMeshProUGUI enemyName;

    [SerializeField] private Button[] enemyButtons;
    private EnemySO _currentEnemyDisplayed;
    private LevelSelectionUi _levelSelectionUi;
    
    private void Start()
    {
        _levelSelectionUi = FindObjectOfType<LevelSelectionUi>();
        enemyImage.enabled = false;
        enemyImageShade.enabled = false;
        enemyDescription.text = null;
        enemyName.text = null;

        foreach (var button in enemyButtons)
        {
            button.onClick.AddListener(()=> ShowDescription(button.GetComponent<EncyclopediaButton>().enemySo));   
        }
    }

    private void ShowDescription(EnemySO enemyData)
    {
        _levelSelectionUi.PlayClickSfx();
        
        enemyImage.enabled = true;
        enemyImageShade.enabled = true;
        
        enemyImage.sprite = enemyData.enemyIcon;
        enemyImageShade.sprite = enemyData.enemyIcon;
        enemyDescription.text = enemyData.enemyDescription;
        enemyName.text = enemyData.enemyName;
    }
}