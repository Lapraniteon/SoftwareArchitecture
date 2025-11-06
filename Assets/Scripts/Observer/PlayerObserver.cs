using UnityEngine;

/// <summary>
/// The abstract class for concrete enemy observers, the abstract class subscribes to onEnemyCreated
/// for initialization and onEnemyHit for getting enemy hit notification and acting accordingly
/// </summary>

public abstract class PlayerObserver : MonoBehaviour
{
    //[SerializeField]
    //protected PlayerModel playerModel;

    protected void OnEnable()
    {
        PlayerModel.onPlayerInit += OnPlayerInit;
        PlayerModel.onPlayerXPGained += OnPlayerXPGained;
        PlayerModel.onPlayerLevelUp += OnPlayerLevelUp;
    }

    protected void OnDisable()
    {
        PlayerModel.onPlayerInit -= OnPlayerInit;
        PlayerModel.onPlayerXPGained -= OnPlayerXPGained;
        PlayerModel.onPlayerLevelUp -= OnPlayerLevelUp;
    }

    protected abstract void OnPlayerInit(Player player);
    protected abstract void OnPlayerLevelUp(Player player);
    
    protected abstract void OnPlayerXPGained(Player player);
}
