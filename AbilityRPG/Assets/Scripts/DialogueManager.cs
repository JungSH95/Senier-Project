using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// https://www.youtube.com/watch?v=_nRzoTzeyxU      다이얼로그 시스템 만드는법
public class DialogueManager : MonoBehaviour
{
    public TextMeshPro text;
    public GameObject quad;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    // 다이얼로그, 내용 출력 처음부터 시작
    public void StartDialogue(Dialogue dialogue, Transform dialoguePos, bool isAutoDialogue)
    {
        transform.position = dialoguePos.position;

        Debug.Log("Starting conversation with " + dialogue.name);   // 다이얼로그의 이름

        sentences.Clear();                                          // 기존 내용 비우기

        foreach (string sentence in dialogue.sentences)
        {
            // 내용들 큐에 전부 넣고
            sentences.Enqueue(sentence);
        }

        // 자동으로 대사를 할 것인지
        if (isAutoDialogue)
        {
            StopAllCoroutines();
            StartCoroutine(CoAutoSentence(dialoguePos));
        }
        else
            DisplayNextSentence(dialoguePos);
    }

    IEnumerator CoAutoSentence(Transform dialoguePos)
    {
        yield return null;

        while(sentences.Count > 0)
        {
            string sentence = sentences.Dequeue();

            StartCoroutine(CoTypeSentence(sentence, dialoguePos));
            yield return new WaitForSeconds(2f);
        }
    }

    // 다음 내용으로
    public void DisplayNextSentence(Transform dialoguePos)
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
        StartCoroutine(CoTypeSentence(sentence, dialoguePos));
    }

    IEnumerator CoTypeSentence(string sentence, Transform dialoguePos)
    {
        text.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;

            DialogueBoxSizeSetting();
            transform.position = new Vector3(dialoguePos.position.x, 1f, dialoguePos.position.z + text.preferredHeight / 2f);
            yield return null;
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
    }

    void DialogueBoxSizeSetting()
    {
        float x = text.preferredWidth;
        x = (x > 3) ? 3 : x + 0.3f; 
        quad.transform.localScale = new Vector3(x, text.preferredHeight + 0.3f);
    }
}
