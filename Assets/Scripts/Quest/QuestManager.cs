using System;
using UnityEngine;

namespace SADungeon.Quest
{
    
    /// <summary>
    /// Singleton to keep a reference of the current active quest and to broadcast events based on that quest's state.
    /// </summary>

    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance { get; private set; }

        /// <summary>
        /// Fires after quest initialization.
        /// </summary>
        public event Action<Quest> onQuestInit;
        /// <summary>
        /// Fires after a quest has broadcasted a progress update.
        /// </summary>
        public event Action<Quest> onQuestProgress;
        /// <summary>
        /// Fires after a quest has broadcasted its completion.
        /// </summary>
        public event Action<Quest> onQuestCompleted;
        /// <summary>
        /// Fires when a quest ends and/or gets destroyed.
        /// </summary>
        public event Action<Quest> onQuestDestroyed;

        [Tooltip("The current active quest.")]
        public Quest currentQuest;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void StartQuest(Quest quest)
        {
            if (currentQuest != null) // New quest overrides old quest.
            {
                Debug.LogWarning("Previous quest was still active. Stopping Quest " + currentQuest.name);
                EndQuest();
            }

            Quest newQuest = Instantiate(quest, transform); // Start the new quest.

            currentQuest = newQuest;
            
            currentQuest.onQuestInit += onQuestInit;
            currentQuest.onQuestProgress += onQuestProgress;
            currentQuest.onQuestCompleted += onQuestCompleted;
            currentQuest.onQuestDestroyed += onQuestDestroyed;
            
            Debug.Log("Started quest: " + currentQuest.name);
            
            onQuestInit?.Invoke(newQuest);
        }

        // Globally accessible method to progress the current quest by 1.
        public void ProgressCurrentQuest() => currentQuest.ProgressQuest(1);

        public void EndQuest()
        {
            if (currentQuest == null)
                return;
            
            currentQuest.onQuestInit -= onQuestInit;
            currentQuest.onQuestProgress -= onQuestProgress;
            currentQuest.onQuestCompleted -= onQuestCompleted;
            currentQuest.onQuestDestroyed -= onQuestDestroyed;
            
            onQuestDestroyed?.Invoke(currentQuest);
            
            Destroy(currentQuest.gameObject);

            currentQuest = null;
        }
        
    }
}