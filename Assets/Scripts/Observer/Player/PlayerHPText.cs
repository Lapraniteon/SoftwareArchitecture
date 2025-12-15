using TMPro;
using UnityEngine;

namespace SADungeon.Player
{

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

        protected override void OnPlayerHealthChanged(Player player)
        {
            UpdateHPText(player);
        }

        private void UpdateHPText(Player player)
        {
            hpText.text = $"HP: {player.currentHP} / {player.MaxHP}";
        }
    }
}