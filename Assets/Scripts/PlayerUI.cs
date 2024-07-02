using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{
    #region Singleton
    public static PlayerUI instance;
    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("More than one PlayerUI!");
        else instance = this;

        if (SaveGame.load)
        {
            Debug.Log("loading data...");
            LoadData();
            SaveGame.load = false;
        }
    }
    #endregion

    

    public Text experienceCount, coinCount;
    public GameObject deathStuff;
    public HealthBar healthbar;
    public int currentHealth = 100;
    
    private int MaxHealth = 100; 

    [HideInInspector]
    public static int XP = 0, coins = 0;

    GameData data;

    private void Start()
    {
        healthbar.SetMaxHealth(MaxHealth);
        healthbar.SetHealth(currentHealth);
    }

    public void UpdateUI()
    {
        experienceCount.text = XP.ToString();
        coinCount.text = coins.ToString();
        healthbar.SetHealth(currentHealth);

    }

    public void Die()
    {
        deathStuff.SetActive(true);
        gameObject.SetActive(false);
    }

    public void LoadData()
    {
        data = SaveGame.LoadSystem();
      
        currentHealth = data.health;
        //healthbar.SetHealth(currentHealth);  
        coins = data.coins;
        XP = data.XP;
        Vector3 position;
        position.x = data.pos[0];
        position.y = data.pos[1];
        position.z = 0.0f;
        UpdateUI();
        gameObject.transform.position = position;

        //PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        //player.currentHealth = data.health;
        //player.healthbar.SetHealth(data.health);
    }

    public void SaveData()
    {
        SaveGame.SaveSystem(this);
    }
}
