using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int stars;
    public int level;

    public LevelState[] levelStates;
    public int[] levelScore;
    
    public GameData(Game game)
    {
        stars = game.stars;
        level = game.level;

        levelStates = game.levelStates;
        levelScore = game.levelScore;
    }
}