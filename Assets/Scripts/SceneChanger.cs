using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public Sprite sound, mute;
    public Button icon;
    public GameObject OptionsMenu;
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadScene(string scene)
    {
        SaveGame.load = true;
        ChangeScene(scene);
    }

    //public IEnumerator ChangeScene(string scene)
    //{

    //}

    public void Exit()
    {
        Debug.Log("exitgame");
        Application.Quit();
    }

    public void Mute()
    {
        AudioSource audio = GetComponent<AudioSource>();
        bool activate = !audio.gameObject.activeSelf;
        audio.gameObject.SetActive(activate);
        if (activate)
            icon.image.sprite = sound;
        else icon.image.sprite = mute;
    }

    public void OpenOptions()
    {
        OptionsMenu.SetActive(!OptionsMenu.activeSelf);
    }
}
