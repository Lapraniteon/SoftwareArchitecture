using System;
using UnityEngine;

namespace SADungeon.Quest
{

    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance { get; private set; }

        public event Action<Quest> onQuestInit;
        public event Action<Quest> onQuestProgress;
        public event Action<Quest> onQuestCompleted;
        public event Action<Quest> onQuestDestroyed;

        public Quest currentQuest;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void StartQuest(Quest quest)
        {
            if (currentQuest != null)
            {
                Debug.LogWarning("Previous quest was still active. Stopping Quest " + currentQuest.name);
                EndQuest();
            }

            Quest newQuest = Instantiate(quest, transform);

            currentQuest = newQuest;
            
            currentQuest.onQuestInit += onQuestInit;
            currentQuest.onQuestProgress += onQuestProgress;
            currentQuest.onQuestCompleted += onQuestCompleted;
            currentQuest.onQuestDestroyed += onQuestDestroyed;
            
            Debug.Log("Started quest: " + currentQuest.name);
            
            onQuestInit?.Invoke(newQuest);
        }

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