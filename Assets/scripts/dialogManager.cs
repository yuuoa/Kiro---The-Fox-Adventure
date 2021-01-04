using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogManager : MonoBehaviour
{

    public Text dialogText;

    private Queue<string>  sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void startDialog (dialog dialogs)
    {
        sentences.Clear();

        foreach(string sentence in dialogs.sentences)
        {
            sentences.Enqueue(sentence);
        }

        displayNextSentence();
    }

    public void displayNextSentence()
    {
        if (sentences.Count == 0)
        {
            endDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
    }

    void endDialog()
    {

    }
}
