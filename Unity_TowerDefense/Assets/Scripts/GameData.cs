using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int stars;
    public int level;

    public GameData(Game game)
    {
        stars = game.stars;
        level = game.level;
    }
}