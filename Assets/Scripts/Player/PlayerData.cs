using UnityEngine;
using System;

/// <summary>
/// A ScriptableObject that creates Enemy objects(Factory pattern).
/// </summary>
[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int maxHP;
    public int startingMoney;
    public int level;

    public int[] xpRequirementsForNextLevel;

    public Player CreatePlayer()
    {
        return new Player(maxHP, startingMoney, level, xpRequirementsForNextLevel);
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
    public int currentXp;
    public int Level => level;
    private int level;
    
    public int[] XpRequirementsForNextLevel => xpRequirementsForNextLevel;
    private int[] xpRequirementsForNextLevel;
    

    public Player(int pMaxHP, int pMoney, int pLevel, int[] pXpReqs)
    {
        maxHP = pMaxHP;
        currentHP = pMaxHP;
        money = pMoney;
        currentXp = 0;
        level = pLevel;
        xpRequirementsForNextLevel = pXpReqs;
    }
}