using NaughtyAttributes;
using UnityEngine;

namespace SADungeon.Quest
{

    public class QuestStarter : MonoBehaviour
    {

        [SerializeField] private Quest questToStart;

        
        [SerializeField] private bool onTriggerEnter;
        
        [Layer] [ShowIf("onTriggerEnter")]
        [SerializeField] private int layer;

        public void StartQuest()
        {
            if (questToStart == null) 
                return;
            
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