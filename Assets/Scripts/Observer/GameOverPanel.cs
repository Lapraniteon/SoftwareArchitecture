using System;
using SADungeon.Player;
using UnityEngine;

/// <summary>
/// Controls the logic in the Game Over UI panel.
/// </summary>

public class GameOverPanel : MonoBehaviour
{

    public GameObject gameOverPanel;
    
    private void OnEnable()
    {
        PlayerModel.onPlayerDead += ShowPanel;
    }

    private void OnDisable()
    {
        PlayerModel.onPlayerDead -= ShowPanel;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void ShowPanel(Player player)
    {
        gameOverPanel.SetActive(true);
    }

    public void RespawnButton()
    {
        GameManager.Instance.RestartGame();
    }
}
