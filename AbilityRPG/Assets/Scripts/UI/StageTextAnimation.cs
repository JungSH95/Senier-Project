using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageTextAnimation : MonoBehaviour
{
    private Animator animator;

    public TextMeshProUGUI text;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StageAnimationStart(string stagenumber)
    {
        text.text = stagenumber + " - Stage";
        StartCoroutine(CoStageAnimation());
    }

    private IEnumerator CoStageAnimation()
    {
        animator.SetBool("StageIn", true);
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("StageIn", false);
    }
}
