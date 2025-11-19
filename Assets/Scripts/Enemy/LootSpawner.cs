using SADungeon.Inventory;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    
    [SerializeField] 
    private GroundPickup groundPickupPrefab;

    public void SpawnLoot(ItemData[] lootArray, Vector3 center, float range = 1f)
    {
        foreach (ItemData lootData in lootArray)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * range;
            spawnPosition.y = 0f;
            spawnPosition += center;
            
            GroundPickup pickup = Instantiate(groundPickupPrefab, spawnPosition, Quaternion.identity);
            pickup.itemData = lootData;
        }
    }
}
