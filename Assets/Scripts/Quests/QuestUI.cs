using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    #region Singleton
    public static QuestUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one QuestUI!");
            return;
        }

        instance = this;
    }

    #endregion

    public int space = 4;
    public List<QuestGiver> quests = new List<QuestGiver>();
    public Transform QuestsParent;
    public GameObject QuestsWindow;
    public Text noQuestsText;


    QuestSlot[] slots;

    void Start()
    {
        slots = QuestsParent.GetComponentsInChildren<QuestSlot>();
        UpdateUI();
    }

    public bool AddQuest(QuestGiver quest)
    {
        if(quests.Count < space)
        {
            quests.Add(quest);
            UpdateUI();
            return true;
        }
        return false;
    }

    public void FinishedQuest(QuestGiver quest)
    {
        quests.Remove(quest);
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < quests.Count)
            {
                slots[i].gameObject.SetActive(true);
                slots[i].FillSlot(quests[i]);
                Debug.Log("Adding quest...");
            }
            else slots[i].gameObject.SetActive(false);
        }

        if (quests.Count > 0)
            noQuestsText.gameObject.SetActive(false);
        else noQuestsText.gameObject.SetActive(true);
    }

    public void OpenQuests()
    {
        QuestsWindow.SetActive(!QuestsWindow.activeSelf);
    }
}
