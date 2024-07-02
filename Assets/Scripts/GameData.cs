using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coins, XP, health;
    public float[] pos = new float[3];

    public GameData(PlayerUI player)
    {
        coins = PlayerUI.coins;
        XP = PlayerUI.XP;
        //if (PlayerController.instance != null)
        //{
            health = player.currentHealth;
            pos[0] = player.transform.position.x;
            pos[1] = player.transform.position.y;
        //}
    }
}
