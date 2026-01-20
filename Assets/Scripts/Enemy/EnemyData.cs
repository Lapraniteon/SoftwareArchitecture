using UnityEngine;
using System;
using SADungeon.Inventory;

namespace SADungeon.Enemy
{

    /// <summary>
    /// A ScriptableObject that creates Enemy objects(Factory pattern).
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string id;
        public string enemyName;
        public int maxHP;
        public float speed;
        public int xp;

        public ItemData[] dropTable;

        public Enemy CreateEnemy()
        {
            return new Enemy(this);
        }
    }

    [Serializable]
    public class Enemy
    {
        private string id;
        public string Id => id;
        
        public string EnemyName => enemyName;
        private string enemyName;
        
        public int MaxHP => maxHP;
        private int maxHP;
        public int currentHP;
        public float Speed => speed;
        private float speed;
        public int XP => xp;
        private int xp;

        public ItemData[] DropTable => dropTable;
        private ItemData[] dropTable;

        public Enemy(EnemyData enemyData)
        {
            id = enemyData.id;
            enemyName = enemyData.enemyName;
            maxHP = enemyData.maxHP;
            currentHP = enemyData.maxHP;
            speed = enemyData.speed;
            xp = enemyData.xp;
            dropTable = enemyData.dropTable;
        }
    }
}