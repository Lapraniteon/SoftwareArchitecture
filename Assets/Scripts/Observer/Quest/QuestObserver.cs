using SADungeon.Inventory;
using UnityEngine;

namespace SADungeon.Quest
{

    /// <summary>
    /// Abstract class for scripts that need to observe Quest status. Quest's notify their events and this script captures them.
    /// </summary>
    
    public abstract class QuestObserver : MonoBehaviour
    {

        protected QuestManager questManager;
        
        private void OnEnable()
        {
            if (questManager == null)
            {
                questManager = QuestManager.Instance;
            }
            
            QuestManager.Instance.onQuestInit += OnQuestInit;
            QuestManager.Instance.onQuestProgress += OnQuestProgress;
            QuestManager.Instance.onQuestCompleted += OnQuestComplete;
            QuestManager.Instance.onQuestDestroyed += OnQuestDestroyed;
        }
        
        private void OnDisable()
        {
            QuestManager.Instance.onQuestInit -= OnQuestInit;
            QuestManager.Instance.onQuestProgress -= OnQuestProgress;
            QuestManager.Instance.onQuestCompleted -= OnQuestComplete;
            QuestManager.Instance.onQuestDestroyed -= OnQuestDestroyed;
        }

        protected virtual void OnQuestInit(Quest quest)
        {
            
        }

        protected virtual void OnQuestProgress(Quest quest)
        {
            
        }

        protected virtual void OnQuestComplete(Quest quest)
        {
            
        }

        protected virtual void OnQuestDestroyed(Quest quest)
        {
            
        }
    }

}