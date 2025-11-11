using TMPro;
using UnityEngine;

public class EnemyHPText : EnemyObserver
{

    public TMP_Text hpText;
    
    protected override void OnEnemyCreated(Enemy enemy)
    {
        UpdateText(enemy);
    }
    
    protected override void OnEnemyHit(Enemy enemy)
    {
        UpdateText(enemy);
    }

    private void UpdateText(Enemy enemy)
    {
        hpText.text = $"{enemy.currentHP}/{enemy.MaxHP}";
    }
    
}
