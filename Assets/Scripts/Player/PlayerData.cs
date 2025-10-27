using UnityEngine;
using System;

/// <summary>
/// A ScriptableObject that creates Enemy objects(Factory pattern).
/// </summary>
[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int maxHP;
    public int money;
    public int xp;
    public int level;

    public Player CreatePlayer()
    {
        return new Player(maxHP, money, xp, level);
    }
}

[Serializable]
public class Player
{
    public int MaxHP => maxHP;
    private int maxHP;
    public int currentHP;
    public int Money => money;
    private int money;
    public int XP => xp;
    private int xp;
    public int Level => Level;
    private int level;

    public Player(int pMaxHP, int pMoney, int pXP, int pLevel)
    {
        maxHP = pMaxHP;
        currentHP = pMaxHP;
        money = pMoney;
        xp = pXP;
        level = pLevel;
    }
}