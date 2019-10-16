using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// https://www.youtube.com/watch?v=_nRzoTzeyxU      다이얼로그 시스템 만드는법
public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    // 다이얼로그, 내용 출력 처음부터 시작
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);   // 다이얼로그의 이름
        nameText.text = dialogue.name;

        sentences.Clear();                                          // 기존 내용 비우기

        foreach (string sentence in dialogue.sentences)
        {
            // 내용들 큐에 전부 넣고
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    // 다음 내용으로
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        //Debug.Log(sentence);
        //dialogueText.text = sentence;

        StopAllCoroutines();
        StartCoroutine(CoTypeSentence(sentence));
    }

    IEnumerator CoTypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}
