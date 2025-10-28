using UnityEngine;

/// <summary>
/// The abstract class for concrete enemy observers, the abstract class subscribes to onEnemyCreated
/// for initialization and onEnemyHit for getting enemy hit notification and acting accordingly
/// </summary>

public abstract class PlayerObserver : MonoBehaviour
{
    [SerializeField]
    protected PlayerModel playerModel;

    protected void OnEnable()
    {
        playerModel.onPlayerInit += OnPlayerInit;
        playerModel.onPlayerXPGained += OnPlayerXPGained;
        playerModel.onPlayerLevelUp += OnPlayerLevelUp;
    }

    protected void OnDisable()
    {
        playerModel.onPlayerInit -= OnPlayerInit;
        playerModel.onPlayerXPGained -= OnPlayerXPGained;
        playerModel.onPlayerLevelUp -= OnPlayerLevelUp;
    }

    protected abstract void OnPlayerInit(Player player);
    protected abstract void OnPlayerLevelUp(Player player);
    
    protected abstract void OnPlayerXPGained(Player player);
}
