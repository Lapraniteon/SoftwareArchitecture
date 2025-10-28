using TMPro;
using UnityEngine;

public class PlayerXPLevelText : PlayerObserver
{
    
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI lvText;

    protected override void OnPlayerInit(Player player)
    {
        xpText.text = $"XP: {player.currentXp}/{player.XpRequirementsForNextLevel[0]}";
        lvText.text = $"Level: {player.Level}";
    }
    
    protected override void OnPlayerXPGained(Player player)
    {
        xpText.text = $"XP: {player.currentXp}/{player.XpRequirementsForNextLevel[player.Level - 1]}";
    }
    
    protected override void OnPlayerLevelUp(Player player)
    {
        
    }
}
