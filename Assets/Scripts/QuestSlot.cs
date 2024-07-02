using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    public Image giverIcon, requiredIcon;
    public Text goalText, gold, XP, currentCount;

    public void FillSlot(QuestGiver giver) {

        goalText.text = giver.quest.title + ", " + giver.quest.goal.Goal.ToString() + " " + giver.quest.goal.requiredCount.ToString() + " " + giver.quest.goal.requiredType.name;
        if (giver.quest.goal.requiredCount > 1)
            goalText.text += "s";

        giverIcon.sprite = giver.GetComponent<SpriteRenderer>().sprite;
        requiredIcon.sprite = giver.quest.goal.requiredType;
        gold.text = giver.quest.goldReward.ToString();
        XP.text = giver.quest.XPReward.ToString();
        currentCount.text = giver.quest.goal.current.ToString() + "/" + giver.quest.goal.requiredCount.ToString();
    }





}
