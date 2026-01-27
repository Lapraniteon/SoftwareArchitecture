using SADungeon.Inventory;
using UnityEngine;

/// <summary>
/// Class responsible for spawning GroundPickup gameObjects as specified in a passed in array of ItemData's.
/// </summary>

public class LootSpawner : MonoBehaviour
{
    
    /// <summary>
    /// Instantiate GroundPickups at the specified point.
    /// </summary>
    /// <param name="lootArray">The ItemData's to assign to the GroundPickups</param>
    /// <param name="center">The location to spawn the pickups.</param>
    /// <param name="range">The range in which to spawn pickups around the point.</param>
    public void SpawnLoot(ItemData[] lootArray, Vector3 center, float range = 1f)
    {
        foreach (ItemData lootData in lootArray)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * range;
            spawnPosition.y = 0f;
            spawnPosition += center;
            
            GroundPickup pickup = Instantiate(lootData.groundPickupPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
