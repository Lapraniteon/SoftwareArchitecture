using UnityEngine;
using System;
using SADungeon.Inventory;

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

    public ItemData[] dropTable;

    public Enemy CreateEnemy()
    {
        return new Enemy(maxHP, speed, money, xp, dropTable);
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
    
    public ItemData[] DropTable => dropTable;
    private ItemData[] dropTable;

    public Enemy(int pMaxHP, float pSpeed, int pMoney, int pXP, ItemData[] pDropTable)
    {
        maxHP = pMaxHP;
        currentHP = pMaxHP;
        speed = pSpeed;
        money = pMoney;
        xp = pXP;
        dropTable = pDropTable;
    }
}