using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Button[] archerUpgrades;
    [SerializeField] private Button[] mageUpgrades;
    [SerializeField] private Button[] bombUpgrades;
    [SerializeField] private Button[] slowdownUpgrades;
    [SerializeField] private Button[] meteorUpgrades;

    private void Start()
    {
        /*archerUpgradeStates[0].upgradeState = UpgradeState.UNLOCKED;
        mageUpgradeStates[0].upgradeState = UpgradeState.UNLOCKED;
        bombUpgradeStates[0].upgradeState = UpgradeState.UNLOCKED;
        slowdownUpgradeStates[0].upgradeState = UpgradeState.UNLOCKED;
        meteorUpgradeStates[0].upgradeState = UpgradeState.UNLOCKED;*/

        int index;
        for (int i = 0; i < archerUpgrades.Length; i++)
        {
            index = i + 1;
            archerUpgrades[i].onClick.AddListener(()=> ArcherUpgrade(index));
        }
        
        for (int i = 0; i < mageUpgrades.Length; i++)
        {
            index = i + 1;
            mageUpgrades[i].onClick.AddListener(()=> MageUpgrade(index));
        }
        
        for (int i = 0; i < bombUpgrades.Length; i++)
        {
            index = i + 1;
            bombUpgrades[i].onClick.AddListener(()=> BombUpgrade(index));
        }
        
        for (int i = 0; i < slowdownUpgrades.Length; i++)
        {
            index = i + 1;
            slowdownUpgrades[i].onClick.AddListener(()=> SlowdownUpgrade(index));
        }
        
        for (int i = 0; i < meteorUpgrades.Length; i++)
        {
            index = i + 1;
            meteorUpgrades[i].onClick.AddListener(()=> MeteorUpgrade(index));
        }
    }

    void Init()
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

        for (int i = 0; i < archerUpgrades.Length; i++)
        {
            if (archerUpgradeStates[i].upgradeState == UpgradeState.UNLOCKED)
            {
                archerUpgrades[i].interactable = true;

                if (PlayerStats.Instance.Coins < archerUpgradeStates[i].cost)
                {
                    archerUpgrades[i].interactable = false;
                }
            }
            else
            {
                archerUpgrades[i].interactable = false;
            }
        }
        
        for (int i = 0; i < mageUpgrades.Length; i++)
        {
            if (mageUpgradeStates[i].upgradeState == UpgradeState.UNLOCKED)
            {
                mageUpgrades[i].interactable = true;
                
                if (PlayerStats.Instance.Coins < mageUpgradeStates[i].cost)
                {
                    mageUpgrades[i].interactable = false;
                }
            }
            else
            {
                mageUpgrades[i].interactable = false;
            }
        }
        
        for (int i = 0; i < bombUpgrades.Length; i++)
        {
            if (bombUpgradeStates[i].upgradeState == UpgradeState.UNLOCKED)
            {
                bombUpgrades[i].interactable = true;
                
                if (PlayerStats.Instance.Coins < bombUpgradeStates[i].cost)
                {
                    bombUpgrades[i].interactable = false;
                }
            }
            else
            {
                bombUpgrades[i].interactable = false;
            }
        }

        for (int i = 0; i < slowdownUpgrades.Length; i++)
        {
            if (slowdownUpgradeStates[i].upgradeState == UpgradeState.UNLOCKED)
            {
                slowdownUpgrades[i].interactable = true;
                
                if (PlayerStats.Instance.Coins < slowdownUpgradeStates[i].cost)
                {
                    slowdownUpgrades[i].interactable = false;
                }
            }
            else
            {
                slowdownUpgrades[i].interactable = false;
            }
        }

        for (int i = 0; i < meteorUpgrades.Length; i++)
        {
            if (meteorUpgradeStates[i].upgradeState == UpgradeState.UNLOCKED)
            {
                meteorUpgrades[i].interactable = true;
                
                if (PlayerStats.Instance.Coins < meteorUpgradeStates[i].cost)
                {
                    meteorUpgrades[i].interactable = false;
                }
            }
            else
            {
                meteorUpgrades[i].interactable = false;
            }
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
    }

    private void ArcherUpgrade(int upgradeLevel)
    {
        if (upgradeLevel < 4)
        {
            archerUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            archerUpgradeStates[upgradeLevel].upgradeState = UpgradeState.UNLOCKED;
            
            archerUpgrades[upgradeLevel-1].interactable = false;
            archerUpgrades[upgradeLevel].interactable = true;
        }
        else
        {
            archerUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            archerUpgrades[upgradeLevel-1].interactable = false;
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
        if (upgradeLevel < 4)
        {
            mageUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            mageUpgradeStates[upgradeLevel].upgradeState = UpgradeState.UNLOCKED;
            
            mageUpgrades[upgradeLevel-1].interactable = false;
            mageUpgrades[upgradeLevel].interactable = true;
        }
        else
        {
            mageUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            mageUpgrades[upgradeLevel-1].interactable = false;
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
        if (upgradeLevel < 4)
        {
            bombUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            bombUpgradeStates[upgradeLevel].upgradeState = UpgradeState.UNLOCKED;
            
            bombUpgrades[upgradeLevel-1].interactable = false;
            bombUpgrades[upgradeLevel].interactable = true;
        }
        else
        {
            bombUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            bombUpgrades[upgradeLevel-1].interactable = false;
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
        if (upgradeLevel < 4)
        {
            slowdownUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            slowdownUpgradeStates[upgradeLevel].upgradeState = UpgradeState.UNLOCKED;
            
            slowdownUpgrades[upgradeLevel-1].interactable = false;
            slowdownUpgrades[upgradeLevel].interactable = true;
        }
        else
        {
            slowdownUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            slowdownUpgrades[upgradeLevel-1].interactable = false;
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
        if (upgradeLevel < 4)
        {
            meteorUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            meteorUpgradeStates[upgradeLevel].upgradeState = UpgradeState.UNLOCKED;
            
            meteorUpgrades[upgradeLevel-1].interactable = false;
            meteorUpgrades[upgradeLevel].interactable = true;
        }
        else
        {
            meteorUpgradeStates[upgradeLevel-1].upgradeState = UpgradeState.BOUGHT;
            meteorUpgrades[upgradeLevel-1].interactable = false;
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
    }
}
