using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Inventory!");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void ItemChanged();
    public ItemChanged itemsChangedCall; 
    public int space = 12;
    //public List<Item> items = new List<Item>();
    public List<GameObject> objects = new List<GameObject>();
    GameObject item;

    public bool Add(GameObject newItem)
    {
        if (objects.Count >= space)
        {
            Debug.Log("Not enough space!");
            return false;
        }
        else objects.Add(newItem);
        Debug.Log("Adding " + newItem.name);
        if (itemsChangedCall != null)
            itemsChangedCall.Invoke();
        return true;


    }
    public void Remove(GameObject item)
    {
        Debug.Log("Removing " + item.name);
        objects.Remove(item);
        if (itemsChangedCall != null)
            itemsChangedCall.Invoke();
    }


    public void UpdateCurrent()
    {
        if (PlayerController.instance.gatherQuest)
        {
            for (int i = 0; i < QuestUI.instance.quests.Count; i++)
            {
                int count = 0;
                if (QuestUI.instance.quests[i].quest.goal.Goal == GoalType.Gather)
                {
                    Sprite questSprite;
                    for (int j = 0; j < objects.Count; j++) {
                        questSprite = QuestUI.instance.quests[i].quest.goal.requiredType;
                        if (questSprite == objects[j].GetComponent<SpriteRenderer>().sprite || objects[j].name.Contains(questSprite.name))
                            count++;
                    }
                    //Debug.Log("REQUIRED TYPE: " + QuestUI.instance.quests[i].quest.goal.requiredType + " )
                    QuestUI.instance.quests[i].quest.goal.current = count;
                    QuestUI.instance.quests[i].quest.Evaluate();

                }
            }
        }
    }

    public void RemoveQuestItems(Sprite item, int count)
    {
        //for (int j = 0; j < objects.Count && count > 0; j++)
        for (int j = objects.Count - 1; j >= 0 && count > 0; j--)
            if (objects[j].GetComponent<SpriteRenderer>().sprite == item)
            {
                //objects.RemoveAt(j);
                Remove(objects[j]);
                count--;
                Debug.Log("Removing apple, " + count + "more to remove.");
            }

        if (itemsChangedCall != null)
            itemsChangedCall.Invoke();
    }


}
