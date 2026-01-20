using UnityEngine;
using System;
using UnityEngine.Serialization;

namespace SADungeon.Player
{

    /// <summary>
    /// A ScriptableObject that creates Enemy objects(Factory pattern).
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public int maxHP;
        public int level;

        public LevelUpData[] nextLevelUpData;

        public Player CreatePlayer()
        {
            return new Player(maxHP, level, nextLevelUpData);
        }
    }

    [Serializable]
    public class Player
    {
        public int maxHP;
        public int currentHP;
        public int currentXP;

        public int level;

        public LevelUpData[] NextLevelUpData => nextLevelUpData;
        private LevelUpData[] nextLevelUpData;


        public Player(int pMaxHP, int pLevel, LevelUpData[] pLevelUpData)
        {
            maxHP = pMaxHP;
            currentHP = pMaxHP;
            currentXP = 0;
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