using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    public static DialogueManager instance;
    public AudioClip typingSound;
    public Animator animator;
    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("More than one Dialogue Manager!");
        else instance = this;

    }
    #endregion

    public Queue<string> text;
    public Text dialogueText, nameText;
    public GameObject dialogueBox;

    void Start()
    {
        text = new Queue<string>();  
        StartConversation(FindObjectOfType<Dialogue>());

    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            NextSentence();
        }
    }
    public void StartConversation(Dialogue dialogue)
    {
        //dialogueBox.SetActive(true);
        nameText.text = dialogue.title;       
        text.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            text.Enqueue(sentence);
        }

        NextSentence();
    }

    public void NextSentence()
    {
        if(text.Count == 0)
        {
            //dialogueBox.SetActive(false);
            animator.SetBool("Done", true);
            return;
        }

        SoundManager.instance.PlayOnce(typingSound);
        string sentence = text.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeText(sentence));
        //SoundManager.instance.soundFXAudio.Stop();
    }

    IEnumerator TypeText(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

        SoundManager.instance.soundFXAudio.Stop();

    }

}
