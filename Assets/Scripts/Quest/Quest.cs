using System;
using UnityEngine;

namespace SADungeon.Quest
{
    
    /// <summary>
    /// Abstract class for quest types to inherit.
    /// Implements events and basic initialization and kill methods.
    /// </summary>
    
    public abstract class Quest : MonoBehaviour
    {

        public bool completed = false;
        
        public event Action<Quest> onQuestInit;
        public event Action<Quest> onQuestProgress;
        public event Action<Quest> onQuestCompleted;
        public event Action<Quest> onQuestDestroyed;

        private void OnEnable()
        {
            Init();
            onQuestInit?.Invoke(this);
        }

        /// <summary>
        /// Quest initialization logic goes here.
        /// </summary>
        protected abstract void Init();

        private void OnDisable()
        {
            Kill();
            onQuestDestroyed?.Invoke(this);
        }

        /// <summary>
        /// Can be overridden by quests that implement simple counter logic so external sources can influence quest progress.
        /// </summary>
        public virtual void ProgressQuest(int amount)
        {
            
        }
        
        /// <summary>
        /// Called before the quest gets destroyed. Useful for unbinding events and such.
        /// </summary>
        protected abstract void Kill();

        /// <summary>
        /// Used to broadcast onQuestProgress.
        /// </summary>
        protected void BroadcastProgressQuest(Quest quest)
        {
            Debug.Log($"Quest progress: {ToDisplayString()}");
            onQuestProgress?.Invoke(quest);
        }

        /// <summary>
        /// Used to broadcast onQuestCompleted.
        /// </summary>
        protected void BroadcastCompletedQuest(Quest quest)
        {
            Debug.Log("Quest completed");
            onQuestCompleted?.Invoke(quest);
        }
        
        public abstract string ToDisplayString();

        public sealed override string ToString()
        {
            return ToDisplayString();
        }
    }

}