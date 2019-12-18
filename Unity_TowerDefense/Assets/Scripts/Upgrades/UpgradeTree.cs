using System;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeState
{
    LOCKED,
    UNLOCKED,
    BOUGHT
}
public class UpgradeTree : MonoBehaviour
{
    public Upgrade[] archerUpgradeStates = new Upgrade[3];
    public Upgrade[] mageUpgradeStates = new Upgrade[3];
    public Upgrade[] slowdownUpgradeStates = new Upgrade[3];
    public Upgrade[] bombUpgradeStates = new Upgrade[3];
    public Upgrade[] meteorUpgradeStates = new Upgrade[3];
    
    //archer modifiers
    private float _archerRangeIncrease;
    private float _archerDamageIncrease;
    
    //mage modifiers
    private float _mageRangeIncrease;
    private float _mageDamageIncrease;
    
    //slowdown modifiers
    private float _slowdownRangeIncrease;
    private float _slowdownIncrease;
    
    //bomb modifiers
    private float _bombDamageIncrease;
    private float _bombRangeIncrease;
    private float _bombCostDecrease;
    
    //meteor modifiers
    private float _meteorDamageIncrease;
    private float _meteorRangeIncrease;
    private float _meteorCountDownDecrease;

    private Button[] _archerUpgrades = new Button[3];
    private Button[] _mageUpgrades = new Button[3];
    private Button[] _bombUpgrades = new Button[3];
    private Button[] _slowdownUpgrades = new Button[3];
    private Button[] _meteorUpgrades = new Button[3];

    private LevelSelectionUi _levelSelectionUi;

    private void Start()
    {
        _levelSelectionUi = FindObjectOfType<LevelSelectionUi>();
        Game.Instance.OnGameUpdateCompleted.AddListener(Init);
        
        for (int i = 0; i < _archerUpgrades.Length; i++)
        {
            var index = i;
            _archerUpgrades[i] = archerUpgradeStates[i].GetComponent<Button>();
            _archerUpgrades[i].onClick.AddListener(()=> ArcherUpgrade(archerUpgradeStates[index].level));
        }
        
        for (int i = 0; i < _mageUpgrades.Length; i++)
        {
            var index = i;
            _mageUpgrades[i] = mageUpgradeStates[i].GetComponent<Button>();
            _mageUpgrades[i].onClick.AddListener(()=> MageUpgrade(mageUpgradeStates[index].level));
        }
        
        for (int i = 0; i < _bombUpgrades.Length; i++)
        {
            var index = i;
            _bombUpgrades[i] = bombUpgradeStates[i].GetComponent<Button>();
            _bombUpgrades[i].onClick.AddListener(()=> BombUpgrade(bombUpgradeStates[index].level));
        }
        
        for (int i = 0; i < _slowdownUpgrades.Length; i++)
        {
            var index = i;
            _slowdownUpgrades[i] = slowdownUpgradeStates[i].GetComponent<Button>();
            _slowdownUpgrades[i].onClick.AddListener(()=> SlowdownUpgrade(slowdownUpgradeStates[index].level));
        }
        
        for (int i = 0; i < _meteorUpgrades.Length; i++)
        {
            var index = i;
            _meteorUpgrades[i] = meteorUpgradeStates[i].GetComponent<Button>();
            _meteorUpgrades[i].onClick.AddListener(()=> MeteorUpgrade(meteorUpgradeStates[index].level));
        }
    }

    void Init()
    {

        if (GameManager.Instance.CurrentGameState == GameState.SELECTION)
        {
            for (int i = 0; i < archerUpgradeStates.Length; i++)
            {
                archerUpgradeStates[i].upgradeState = Game.Instance.archerUpgradeStates[i];
            }
        
            for (int i = 0; i < mageUpgradeStates.Length; i++)
            {
                mageUpgradeStates[i].upgradeState = Game.Instance.mageUpgradeStates[i];
            }
        
            for (int i = 0; i < bombUpgradeStates.Length; i++)
            {
                bombUpgradeStates[i].upgradeState = Game.Instance.bombUpgradeStates[i];
            }
        
            for (int i = 0; i < slowdownUpgradeStates.Length; i++)
            {
                slowdownUpgradeStates[i].upgradeState = Game.Instance.slowdownUpgradeStates[i];
            }
        
            for (int i = 0; i < meteorUpgradeStates.Length; i++)
            {
                meteorUpgradeStates[i].upgradeState = Game.Instance.meteorUpgradeStates[i];
            }

           
            _archerRangeIncrease = Game.Instance._archerRangeIncrease;
            _archerDamageIncrease = Game.Instance._archerDamageIncrease;

            _mageRangeIncrease = Game.Instance._mageRangeIncrease;
            _mageDamageIncrease = Game.Instance._mageDamageIncrease;

            _slowdownIncrease = Game.Instance._slowdownIncrease;
            _slowdownRangeIncrease = Game.Instance._slowdownRangeIncrease;

            _bombDamageIncrease = Game.Instance._bombDamageIncrease;
            _bombRangeIncrease = Game.Instance._bombRangeIncrease;
            _bombCostDecrease = Game.Instance._bombCostDecrease;

            _meteorDamageIncrease = Game.Instance._meteorDamageIncrease;
            _meteorRangeIncrease = Game.Instance._meteorRangeIncrease;
            _meteorCountDownDecrease = Game.Instance._meteorCountDownDecrease;

            FindObjectOfType<LevelSelectionUi>().UpdateStars();
        }
    }

    private void ArcherUpgrade(int upgradeLevel)
    {
        _levelSelectionUi.PlayUpgrade();
        Game.Instance.stars -= archerUpgradeStates[upgradeLevel - 1].cost;
        
        if (upgradeLevel < 3)
        {
            archerUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            archerUpgradeStates[upgradeLevel].upgradeState = UpgradeState.UNLOCKED;
            
            _archerUpgrades[upgradeLevel-1].interactable = false;
            _archerUpgrades[upgradeLevel].interactable = true;
        }
        else
        {
            archerUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            _archerUpgrades[upgradeLevel-1].interactable = false;
        }
        
        switch (upgradeLevel)
        {
            case 3:
                _archerRangeIncrease += 0.1f;
                break;
            
            case 2:
                _archerDamageIncrease += 0.1f;
                break;
            
            case 1 :
                _archerRangeIncrease += 0.1f;
                break;
        }
        
        SaveUpgrades();
    }
    
    private void MageUpgrade(int upgradeLevel)
    {
        _levelSelectionUi.PlayUpgrade();
        Game.Instance.stars -= mageUpgradeStates[upgradeLevel - 1].cost;
        
        if (upgradeLevel < 3)
        {
            mageUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            mageUpgradeStates[upgradeLevel].upgradeState = UpgradeState.UNLOCKED;
            
            _mageUpgrades[upgradeLevel-1].interactable = false;
            _mageUpgrades[upgradeLevel].interactable = true;
        }
        else
        {
            mageUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            _mageUpgrades[upgradeLevel-1].interactable = false;
        }
        
        switch (upgradeLevel)
        {
            case 3:
                _mageRangeIncrease += 0.1f;
                break;
            
            case 2:
                _mageDamageIncrease += 0.15f;
                break;
            
            case 1 :
                _mageRangeIncrease += 0.1f;
                break;
        }
        
        SaveUpgrades();
    }
    
    private void BombUpgrade(int upgradeLevel)
    {
        _levelSelectionUi.PlayUpgrade();
        Game.Instance.stars -= bombUpgradeStates[upgradeLevel - 1].cost;
        
        if (upgradeLevel < 3)
        {
            bombUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            bombUpgradeStates[upgradeLevel].upgradeState = UpgradeState.UNLOCKED;
            
            _bombUpgrades[upgradeLevel-1].interactable = false;
            _bombUpgrades[upgradeLevel].interactable = true;
        }
        else
        {
            bombUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            _bombUpgrades[upgradeLevel-1].interactable = false;
        }
        
        switch (upgradeLevel)
        {
            case 3:
                _bombCostDecrease += 0.1f;
                break;
            
            case 2:
                _bombRangeIncrease += 0.1f;
                break;
            
            case 1 :
                _bombDamageIncrease += 0.1f;
                break;
        }
        
        SaveUpgrades();
    }
    
    private void SlowdownUpgrade(int upgradeLevel)
    {
        _levelSelectionUi.PlayUpgrade();
        Game.Instance.stars -= slowdownUpgradeStates[upgradeLevel - 1].cost;
        
        if (upgradeLevel < 3)
        {
            slowdownUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            slowdownUpgradeStates[upgradeLevel].upgradeState = UpgradeState.UNLOCKED;
            
            _slowdownUpgrades[upgradeLevel-1].interactable = false;
            _slowdownUpgrades[upgradeLevel].interactable = true;
        }
        else
        {
            slowdownUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            _slowdownUpgrades[upgradeLevel-1].interactable = false;
        }
        
        switch (upgradeLevel)
        {
            case 3:
                _slowdownRangeIncrease += 0.2f;
                break;
            
            case 2:
                _slowdownIncrease += 0.1f;
                break;
            
            case 1 :
                _slowdownRangeIncrease += 0.1f;
                break;
        }
        
        SaveUpgrades();
    }
    
    private void MeteorUpgrade(int upgradeLevel)
    {
        _levelSelectionUi.PlayUpgrade();
        Game.Instance.stars -= meteorUpgradeStates[upgradeLevel - 1].cost;
        
        if (upgradeLevel < 3)
        {
            meteorUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            meteorUpgradeStates[upgradeLevel].upgradeState = UpgradeState.UNLOCKED;
            
            _meteorUpgrades[upgradeLevel-1].interactable = false;
            _meteorUpgrades[upgradeLevel].interactable = true;
        }
        else
        {
            meteorUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            _meteorUpgrades[upgradeLevel-1].interactable = false;
        }

        switch (upgradeLevel)
        {
            case 3:
                _meteorDamageIncrease += 0.25f;
                _meteorCountDownDecrease += 10;
                break;
            
            case 2:
                _meteorDamageIncrease += 0.1f;
                _meteorRangeIncrease += 0.25f;
                _meteorCountDownDecrease += 10;
                break;
            
            case 1 :
                _meteorDamageIncrease += 0.1f;
                break;
        }
        
        SaveUpgrades();
    }

    private void SaveUpgrades()
    {
        for (int i = 0; i < archerUpgradeStates.Length; i++)
        {
            Game.Instance.archerUpgradeStates[i] = archerUpgradeStates[i].upgradeState;
        }
        
        for (int i = 0; i < mageUpgradeStates.Length; i++)
        {
            Game.Instance.mageUpgradeStates[i] = mageUpgradeStates[i].upgradeState;
        }
        
        for (int i = 0; i < bombUpgradeStates.Length; i++)
        {
            Game.Instance.bombUpgradeStates[i] = bombUpgradeStates[i].upgradeState;
        }
        
        for (int i = 0; i < slowdownUpgradeStates.Length; i++)
        {
            Game.Instance.slowdownUpgradeStates[i] = slowdownUpgradeStates[i].upgradeState;
        }
        
        for (int i = 0; i < meteorUpgradeStates.Length; i++)
        {
            Game.Instance.meteorUpgradeStates[i] = meteorUpgradeStates[i].upgradeState;
        }
        
        Game.Instance._archerRangeIncrease = _archerRangeIncrease;
        Game.Instance._archerDamageIncrease = _archerDamageIncrease;

        Game.Instance._mageRangeIncrease =_mageRangeIncrease;
        Game.Instance._mageDamageIncrease = _mageDamageIncrease;

        Game.Instance._slowdownIncrease = _slowdownIncrease;
        Game.Instance._slowdownRangeIncrease = _slowdownRangeIncrease;

        Game.Instance._bombDamageIncrease =_bombDamageIncrease;
        Game.Instance._bombRangeIncrease = _bombRangeIncrease;
        Game.Instance._bombCostDecrease = _bombCostDecrease;

        Game.Instance._meteorDamageIncrease = _meteorDamageIncrease;
        Game.Instance._meteorRangeIncrease = _meteorRangeIncrease;
        Game.Instance._meteorCountDownDecrease = _meteorCountDownDecrease;
        
        FindObjectOfType<LevelSelectionUi>().UpdateStars();
    }
}
