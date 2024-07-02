using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager instance;
    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("More than one Sound Manager!");
        else instance = this;
    }
    #endregion
    public AudioSource soundFXAudio, musicAudio;       
    public void PlayOnce(AudioClip clip)
    {
        soundFXAudio.PlayOneShot(clip);
    }

    public void RandomClip(AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        soundFXAudio.pitch = Random.Range(0.95f, 1.05f);
        soundFXAudio.PlayOneShot(clips[randomIndex]);
    }

    public void MuteMusic()
    {
        musicAudio.enabled = !musicAudio.isActiveAndEnabled;
    }
    public void MuteSound()
    {
        soundFXAudio.enabled = !soundFXAudio.isActiveAndEnabled;
    }
}
