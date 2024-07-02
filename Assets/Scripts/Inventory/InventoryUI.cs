using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public Transform ItemsParent;
    public GameObject InventoryParent;

    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.itemsChangedCall += UpdateUI;
        slots = ItemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateUI()
    {       
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.objects.Count)
            {
                if (inventory.objects[i] != null)
                    slots[i].AddItem(inventory.objects[i]);
            }
            else slots[i].ClearSlot();
        }

        Inventory.instance.UpdateCurrent();
    }

    //public void UpdateCurrent()
    //{
    //    if (PlayerController.instance.gatherQuest)
    //    {
    //        for (int i = 0; i < QuestUI.instance.quests.Count; i++)
    //        {
    //            int count = 0;
    //            if (QuestUI.instance.quests[i].quest.goal.Goal == GoalType.Gather)
    //            {
    //                for (int j = 0; j < inventory.objects.Count; j++)
    //                    if (QuestUI.instance.quests[i].quest.goal.requiredType == inventory.objects[j].GetComponent<SpriteRenderer>().sprite)
    //                        count++;
    //                QuestUI.instance.quests[i].quest.goal.current = count;
    //                QuestUI.instance.quests[i].quest.Evaluate();

    //            }
    //        }
    //    }
    //}

    public void OpenInventory()
    {
        InventoryParent.SetActive(!InventoryParent.activeSelf);
    }
}
