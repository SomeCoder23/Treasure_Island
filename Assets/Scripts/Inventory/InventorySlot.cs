using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    GameObject item;
    public Image icon;
    public Button removeButton;
    //new public AudioSource audio;
    public AudioClip healthPotion;
    
    public void AddItem(GameObject newItem) {
        if (newItem != null){
            item = newItem;
            icon.sprite = item.GetComponent<SpriteRenderer>().sprite;
            icon.enabled = true;
            removeButton.interactable = true;
            Debug.Log("Adding to UI " + item.name);
        }
    }

    public void ClearSlot()
    {
        Debug.Log("Clearing from UI ");
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveItem()
    {
        
        Instantiate(item);
        item.transform.position = PlayerController.instance.shootPoint.position;
        item.SetActive(true);
        Debug.Log("Removing " + item.name);
        Inventory.instance.Remove(item);       
    }

    public void UseItem()  {
        if (item != null) {

            Debug.Log("Using " + item.name);
            if (item.tag == "Food")
                PlayerController.instance.Eat();

            if (item.tag == "Potion")
            {
                PlayerUI.instance.healthbar.SetHealth(100);
                SoundManager.instance.PlayOnce(healthPotion);
            }
            
            Inventory.instance.Remove(item);
        }
    }
}
