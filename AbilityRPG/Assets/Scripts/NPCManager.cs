using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://keidy.tistory.com/205
// https://www.youtube.com/watch?v=mjnTPQipkcU      게임이 망했다 플레이 영상

public class NPCManager : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;

    public bool isAutoSentence;

    public int characterNumber;
    public bool isChangeCharacter;

    public Vector3 startRotation;

    public PlayerChangePopup popupWindow;

    private void Awake()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();

        startRotation = transform.rotation.eulerAngles;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            transform.LookAt(other.transform);

            // 근접해 있는 npc가 존재할 경우 에러 발생
            if (other.GetComponent<PlayerController>().isTalk)
            {
                dialogueTrigger.TriggerDialogue(isAutoSentence);
                other.GetComponent<PlayerController>().isTalk = false;

                // 캐릭터 변경 가능 NPC일 경우
                if (isChangeCharacter && characterNumber >= 0)
                    popupWindow.OpenPopupWindows(dialogueTrigger.dialogue.name);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.rotation = Quaternion.Euler(startRotation);
    }
}
