using UnityEngine;

namespace SADungeon.Player
{

    /// <summary>
    /// The abstract class for concrete enemy observers, the abstract class subscribes to onEnemyCreated
    /// for initialization and onEnemyHit for getting enemy hit notification and acting accordingly
    /// </summary>

    public abstract class PlayerObserver : MonoBehaviour
    {
        protected void OnEnable()
        {
            PlayerModel.onPlayerInit += OnPlayerInit;
            PlayerModel.onPlayerXPGained += OnPlayerXPGained;
            PlayerModel.onPlayerHit += OnPlayerHit;
            PlayerModel.onPlayerDead += OnPlayerDead;
            PlayerModel.onPlayerLevelUp += OnPlayerLevelUp;
            PlayerModel.onPlayerHeal += OnPlayerHeal;
            PlayerModel.onPlayerHealthChanged += OnPlayerHealthChanged;
        }

        protected void OnDisable()
        {
            PlayerModel.onPlayerInit -= OnPlayerInit;
            PlayerModel.onPlayerXPGained -= OnPlayerXPGained;
            PlayerModel.onPlayerHit -= OnPlayerHit;
            PlayerModel.onPlayerDead -= OnPlayerDead;
            PlayerModel.onPlayerLevelUp -= OnPlayerLevelUp;
            PlayerModel.onPlayerHeal -= OnPlayerHeal;
            PlayerModel.onPlayerHealthChanged -= OnPlayerHealthChanged;
        }

        protected virtual void OnPlayerInit(Player player)
        {

        }

        protected virtual void OnPlayerLevelUp(Player player)
        {

        }

        protected virtual void OnPlayerHit(Player player)
        {

        }

        protected virtual void OnPlayerDead(Player player)
        {

        }

        protected virtual void OnPlayerXPGained(Player player)
        {

        }
        
        protected virtual void OnPlayerHeal(Player player)
        {

        }
        
        protected virtual void OnPlayerHealthChanged(Player player)
        {

        }
    }
}