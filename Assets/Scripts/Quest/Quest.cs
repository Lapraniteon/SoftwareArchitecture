using System;
using UnityEngine;

namespace SADungeon.Quest
{

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

        protected abstract void Init();

        private void OnDisable()
        {
            Kill();
            onQuestDestroyed?.Invoke(this);
        }

        protected abstract void Kill();

        protected void BroadcastProgressQuest(Quest quest)
        {
            Debug.Log($"Quest progress: {ToDisplayString()}");
            onQuestProgress?.Invoke(quest);
        }

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