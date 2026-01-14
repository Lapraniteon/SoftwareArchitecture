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