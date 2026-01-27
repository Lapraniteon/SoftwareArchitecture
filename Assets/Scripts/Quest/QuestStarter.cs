using NaughtyAttributes;
using UnityEngine;

namespace SADungeon.Quest
{   
    
    /// <summary>
    /// Class present on gameObjects to start a specified quest (prefab) when triggered, either manually or onTriggerEnter.
    /// </summary>

    public class QuestStarter : MonoBehaviour
    {

        [Tooltip("The quest prefab to spawn.")]
        [SerializeField] private Quest questToStart;
        
        [Tooltip("Should this quest start when the player enters this object's trigger collider?")]
        [SerializeField] private bool onTriggerEnter;
        
        [Tooltip("The layer of the object that can trigger this starter.")]
        [Layer] [ShowIf("onTriggerEnter")]
        [SerializeField] private int layer;

        private bool _triggered;

        public void StartQuest()
        {
            if (questToStart == null || _triggered)
                return;

            _triggered = true;
            QuestManager.Instance.StartQuest(questToStart);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != layer)
                return;

            StartQuest();
        }

    }
}