using UnityEngine;
using System;

namespace SADungeon.Player
{

    /// <summary>
    /// A ScriptableObject that creates Enemy objects(Factory pattern).
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public int maxHP;
        public int startingMoney;
        public int level;

        public LevelUpData[] nextLevelUpData;

        public Player CreatePlayer()
        {
            return new Player(maxHP, startingMoney, level, nextLevelUpData);
        }
    }

    [Serializable]
    public class Player
    {
        public int MaxHP;
        public int currentHP;
        public int Money => money;
        private int money;
        public int currentXp;

        public int level;

        public LevelUpData[] NextLevelUpData => nextLevelUpData;
        private LevelUpData[] nextLevelUpData;


        public Player(int pMaxHP, int pMoney, int pLevel, LevelUpData[] pLevelUpData)
        {
            MaxHP = pMaxHP;
            currentHP = pMaxHP;
            money = pMoney;
            currentXp = 0;
            level = pLevel;
            nextLevelUpData = pLevelUpData;
        }
    }
}

[Serializable]
public class LevelUpData
{
    public int xpRequired;

    public int baseHealthIncrease;
}