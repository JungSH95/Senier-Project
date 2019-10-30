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
            // 코루틴으로 시작해버리면 자리 잡는 부분에서 딜레이 생김
            DisplayNextSentence(dialoguePos);       
            StartCoroutine(CoAutoSentence(dialoguePos));
        }
        else
            DisplayNextSentence(dialoguePos);
    }

    IEnumerator CoAutoSentence(Transform dialoguePos)
    {
        yield return new WaitForSeconds(1.5f);

        while (sentences.Count > 0)
        {
            string sentence = sentences.Dequeue();
            StartCoroutine(CoTypeSentence(sentence, dialoguePos));

            yield return new WaitForSeconds(1.5f);
        }
    }

    // 다음 내용으로
    public void DisplayNextSentence(Transform dialoguePos)
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(CoTypeSentence(sentence, dialoguePos));
    }

    // 대사 출력 코루틴
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


    // 창 사이즈 조절
    void DialogueBoxSizeSetting()
    {
        float x = text.preferredWidth;
        x = (x > 3) ? 3 : x + 0.3f; 
        quad.transform.localScale = new Vector3(x, text.preferredHeight + 0.3f);
    }
}
