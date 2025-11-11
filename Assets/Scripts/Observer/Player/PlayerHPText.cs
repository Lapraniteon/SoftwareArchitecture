using TMPro;
using UnityEngine;

public class PlayerHPText : PlayerObserver
{

    [SerializeField] private TextMeshProUGUI hpText;
    
    protected override void OnPlayerInit(Player player)
    {
        UpdateHPText(player);
    }

    protected override void OnPlayerLevelUp(Player player)
    {
        UpdateHPText(player);
    }

    protected override void OnPlayerHit(Player player)
    {
        UpdateHPText(player);
    }

    private void UpdateHPText(Player player)
    {
        hpText.text = $"HP: {player.currentHP} / {player.MaxHP}";
    }
}
