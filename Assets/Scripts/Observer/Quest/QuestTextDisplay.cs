using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SADungeon.Quest
{

    public class QuestTextDisplay : QuestObserver
    {

        [SerializeField] private TextMeshProUGUI displayText;

        protected override void OnQuestInit(Quest quest)
        {
            displayText.color = Color.white;
            UpdateTextDisplay(quest);
        }

        protected override void OnQuestProgress(Quest quest)
        {
            UpdateTextDisplay(quest);
        }

        protected override void OnQuestComplete(Quest quest)
        {
            UpdateTextDisplay(quest);
            displayText.color = Color.green;
        }

        protected override void OnQuestDestroyed(Quest quest)
        {
            displayText.color = Color.white;
            displayText.text = "None";
        }

        private void UpdateTextDisplay(Quest quest)
        {
            displayText.text = quest.ToDisplayString();
            //displayText.text = "Hello";
        }
    }
}