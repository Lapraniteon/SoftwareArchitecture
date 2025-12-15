using SADungeon.Enemy;
using SADungeon.Inventory;
using UnityEngine;
// All game events are listed here


/// <summary>
/// Published by the dying enemy, it contains the enemy object
/// and enemy data
/// </summary>
public class EnemyDieEventData : EventData
{
    public Enemy enemy;
    public GameObject enemyObject;
    public EnemyDieEventData(Enemy pEnemy, GameObject pEnemyObject)
    {
        name = "EnemyDieEvent";
        enemy = pEnemy;
        enemyObject = pEnemyObject;
    }

    //Overriding ToString method to display event information for debugging
    public override string ToString()
    {
        if (enemyObject == null)
            return "Enemy object already destroyed";
        else
        {
            return "Event name: " + name + "\n" +
                   "Enemy died at position: " + enemyObject.transform.position + "\n"
                    + "Enemy Droped Money: " + enemy.Money
                    + "\n" + "Enemy gave XP: " + enemy.XP;
        }
    }
}

public class GroundPickupEventData : EventData
{
    public Item item;
    public GameObject pickupObject;
    public GroundPickupEventData(Item pItem, GameObject pPickupObject)
    {
        name = "GroundPickupEvent";
        item = pItem;
        pickupObject = pPickupObject;
    }

    //Overriding ToString method to display event information for debugging
    public override string ToString()
    {
        if (pickupObject == null)
            return "Pickup object already destroyed";
        else
        {
            return "Event name: " + name + "\n" +
                   "Ground pickup collected at position: " + pickupObject.transform.position + "\n";
        }
    }
}

public class InventorySingletonInitializedEventData : EventData
{
    public Inventory inventory;

    public InventorySingletonInitializedEventData(Inventory pInventory)
    {
        name = "InventorySingletonInitializedEvent";
        inventory = pInventory;
    }
    
    //Overriding ToString method to display event information for debugging
    public override string ToString()
    {
        if (inventory == null)
            return "Inventory is null";
        else
        {
            return "Event name: " + name + "\n" +
                   "Inventory initialized.";
        }
    }
}