using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerXPLevelText : PlayerObserver
{
    
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI levelText;

    protected override void OnPlayerInit(Player player)
    {
        xpText.text = $"XP: {player.currentXp}/{player.XpRequirementsForNextLevel[0]}";
        levelText.text = GetLevelText(player);
    }
    
    protected override void OnPlayerXPGained(Player player)
    {
        xpText.text = GetXPText(player);
    }
    
    protected override void OnPlayerLevelUp(Player player)
    {
        xpText.text = GetXPText(player);
        levelText.text = GetLevelText(player);
    }

    private string GetXPText(Player player)
    {
        if (player.level > player.XpRequirementsForNextLevel.Length) // Is player at max level?
        {
            return $"XP: Max. level";
        }
        
        return $"XP: {player.currentXp}/{player.XpRequirementsForNextLevel[player.level - 1]}";
    }

    private string GetLevelText(Player player)
    {
        if (player.level > player.XpRequirementsForNextLevel.Length) // Is player at max level?
        {
            return $"Level: {player.level} (Max)";
        }
        
        return $"Level: {player.level}";
    }
}
