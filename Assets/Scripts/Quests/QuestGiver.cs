using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public GameObject questUI, acceptButton;
    public Text questName, questDescription, gold, experiance;
    [TextArea(2, 4)]
    public string thanksText;
    public AudioClip thanksAudio;

    public void OpenQuest()
    {
        if (!quest.completed)
        {
            questUI.SetActive(true);
            questName.text = quest.title;
            questDescription.text = quest.description;
            gold.text = quest.goldReward.ToString();
            experiance.text = quest.XPReward.ToString();
        }
        else
        {
            questDescription.text = thanksText;
            questUI.SetActive(true);
            QuestUI.instance.FinishedQuest(this);
            Inventory.instance.RemoveQuestItems(quest.goal.requiredType, quest.goal.requiredCount);
            if(thanksAudio != null)
                SoundManager.instance.PlayOnce(thanksAudio);
            quest.giveReward();
        }
    }



    public void Accepted()
    {
        if (QuestUI.instance.AddQuest(this))
        {
            quest.isActive = true;
            questUI.SetActive(false);
            PlayerController.instance.quest = quest;
            if (quest.goal.Goal == GoalType.Gather)
            {
                PlayerController.instance.gatherQuest = true;
                Inventory.instance.UpdateCurrent();
            }
            else PlayerController.instance.killQuest = true;
            acceptButton.SetActive(false);
            
        }
        else Debug.Log("You can't add any more quests! Go finish the ones you already accepted ya lazy!");


        
    }

    public void CloseQuest()
    {
        questUI.SetActive(false);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            OpenQuest();
    }


}
