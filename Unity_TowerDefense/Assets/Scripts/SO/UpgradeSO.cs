using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/New Upgrade", fileName = "newUpgrade")]
public class UpgradeSO : ScriptableObject
{
    public int level;
    public int cost;
    public string description;
    public Sprite upgradeImage;
}