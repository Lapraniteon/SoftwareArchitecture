using UnityEngine;
using System;

/// <summary>
/// A ScriptableObject that creates Enemy objects(Factory pattern).
/// </summary>
[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int maxHP;
    public float speed;
    public int money;
    public int xp;

    public Enemy CreateEnemy()
    {
        return new Enemy(maxHP, speed, money, xp);
    }
}

[Serializable]
public class Enemy
{
    public int MaxHP => maxHP;
    private int maxHP;
    public int currentHP;
    public float Speed => speed;
    private float speed;
    public int Money => money;
    private int money;
    public int XP => xp;
    private int xp;

    public Enemy(int pMaxHP, float pSpeed, int pMoney, int pXP)
    {
        maxHP = pMaxHP;
        currentHP = pMaxHP;
        speed = pSpeed;
        money = pMoney;
        xp = pXP;
    }
}