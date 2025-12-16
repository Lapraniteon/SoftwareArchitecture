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

        [Header("Temporary")] 
        [SerializeField] private Quest questToStart;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }
        
        #region TEMPORARY

        private void Start()
        {
            StartQuest(questToStart);
        }
        
        #endregion

        public bool StartQuest(Quest quest)
        {
            if (currentQuest != null)
                return false;

            Quest newQuest = Instantiate(quest, transform);

            currentQuest = newQuest;
            
            currentQuest.onQuestInit += onQuestInit;
            currentQuest.onQuestProgress += onQuestProgress;
            currentQuest.onQuestCompleted += onQuestCompleted;
            currentQuest.onQuestDestroyed += onQuestDestroyed;
            
            onQuestInit?.Invoke(newQuest);

            return true;
        }

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