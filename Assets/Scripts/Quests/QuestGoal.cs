using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType Goal;
    public Sprite requiredType;
    public int requiredCount, current;

}

public enum GoalType {
    Kill,
    Gather
}
