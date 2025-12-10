using System;
using SADungeon.Player;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        PlayerModel.onPlayerDead += PlayerDead;
    }

    private void OnDisable()
    {
        PlayerModel.onPlayerDead -= PlayerDead;
    }

    private void PlayerDead(Player player)
    {
        Debug.Log("Player Dead");

        Time.timeScale = 0f;
    }
}
