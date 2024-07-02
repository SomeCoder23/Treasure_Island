using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public string title;
    [TextArea(2, 6)]
    public string description;
    public int XPReward = 10, goldReward = 10;
    public bool isActive;
    public QuestGoal goal;

    [HideInInspector]
    public bool completed = false;

    public void EvaluateKill(Sprite requiredItem)
    {
        Debug.Log("Evaluating...");
        //if (requiredItem == goal.requiredType)
            goal.current++;
        QuestUI.instance.UpdateUI();

        if (goal.requiredCount <= goal.current)
        {
            completed = true;
            isActive = false;
        }
    }

    public void Evaluate()
    {
        QuestUI.instance.UpdateUI();

        if (goal.requiredCount <= goal.current) {
            completed = true;
            isActive = false;
        }
        else {
            completed = false;
            isActive = true;
        }
    }



    public void giveReward()
    {
        if (completed)
        {
            PlayerUI.coins += goldReward;
            PlayerUI.XP += XPReward;
            PlayerUI.instance.UpdateUI();
            completed = false;
        }

    }

}
