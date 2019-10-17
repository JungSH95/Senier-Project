using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// https://www.youtube.com/watch?v=_nRzoTzeyxU      다이얼로그 시스템 만드는법
public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public TextMeshPro text;
    public GameObject quad;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    // 다이얼로그, 내용 출력 처음부터 시작
    public void StartDialogue(Dialogue dialogue, Transform dialoguePos)
    {
        transform.position = dialoguePos.position;

        Debug.Log("Starting conversation with " + dialogue.name);   // 다이얼로그의 이름
        nameText.text = dialogue.name;

        sentences.Clear();                                          // 기존 내용 비우기

        foreach (string sentence in dialogue.sentences)
        {
            // 내용들 큐에 전부 넣고
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(dialoguePos);
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
        dialogueText.text = "";
        text.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            text.text += letter;

            DialogueBoxSizeSetting();
            transform.position = new Vector2(dialoguePos.position.x, dialoguePos.position.y + text.preferredHeight / 2f);
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
