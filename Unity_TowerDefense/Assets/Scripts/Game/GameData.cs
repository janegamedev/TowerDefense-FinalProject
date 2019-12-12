using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int stars;
    public int level;

    public LevelState[] levelStates;
    public int[] levelScore;

    private string _path;
    
    public UpgradeState[] archerUpgradeStates = new UpgradeState[3];
    public UpgradeState[] mageUpgradeStates = new UpgradeState[3];
    public UpgradeState[] slowdownUpgradeStates = new UpgradeState[3];
    public UpgradeState[] bombUpgradeStates = new UpgradeState[3];
    public UpgradeState[] meteorUpgradeStates = new UpgradeState[3];
    
    //archer modifiers
    public float _archerRangeIncrease;
    public float _archerDamageIncrease;
    
    //mage modifiers
    public float _mageRangeIncrease;
    public float _mageDamageIncrease;
    
    //slowdown modifiers
    public float _slowdownRangeIncrease;
    public float _slowdownIncrease;
    
    //bomb modifiers
    public float _bombDamageIncrease;
    public float _bombRangeIncrease;
    public float _bombCostDecrease;
    
    //meteor modifiers
    public float _meteorDamageIncrease;
    public float _meteorRangeIncrease;
    public float _meteorCountDownDecrease;
    
    
    public GameData(Game game)
    {
        stars = game.stars;
        level = game.currentLevelUnlocked;

        levelStates = game.levelStates;
        levelScore = game.levelScore;

        _path = game.Path;
        
        archerUpgradeStates = game.archerUpgradeStates;
        mageUpgradeStates = game.mageUpgradeStates;
        bombUpgradeStates = game.bombUpgradeStates;
        slowdownUpgradeStates = game.slowdownUpgradeStates;
        meteorUpgradeStates = game.meteorUpgradeStates;
        
        _archerRangeIncrease = game._archerRangeIncrease;
        _archerDamageIncrease = game._archerDamageIncrease;

        _mageRangeIncrease = game._mageRangeIncrease;
        _mageDamageIncrease = game._mageDamageIncrease;

        _slowdownIncrease = game._slowdownIncrease;
        _slowdownRangeIncrease = game._slowdownRangeIncrease;

        _bombDamageIncrease = game._bombDamageIncrease;
        _bombRangeIncrease = game._bombRangeIncrease;
        _bombCostDecrease = game._bombCostDecrease;

        _meteorDamageIncrease = game._meteorDamageIncrease;
        _meteorRangeIncrease = game._meteorRangeIncrease;
        _meteorCountDownDecrease = game._meteorCountDownDecrease;
    }
}