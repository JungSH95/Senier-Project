using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// https://lesslate.github.io/unity/%EC%9C%A0%EB%8B%88%ED%8B%B0-%ED%94%8C%EB%A1%9C%ED%8C%85-%EB%8D%B0%EB%AF%B8%EC%A7%80-%ED%85%8D%EC%8A%A4%ED%8A%B8/
public class FloatingText : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;

    private TextMeshPro textMeshPro;

    private Color startColor;

    Color alpha;

    private bool isStart;
    
    void Update()
    {
        if (isStart)
        {
            transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

            alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
            textMeshPro.color = alpha;
        }
    }

    public void SetText(float fontsize, string str = "")
    {
        GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, -1f);

        moveSpeed = 0.5f;
        alphaSpeed = 3.0f;

        textMeshPro = GetComponent<TextMeshPro>();
        alpha = textMeshPro.color;
        alpha.a = 1.0f;
        textMeshPro.text = str;
        textMeshPro.fontSize = fontsize;

        Invoke("FloatingTextEnd", 1.0f);
        isStart = true;
        gameObject.SetActive(true);
    }

    public void FloatingTextEnd()
    {
        ObjectPool.Instance.PushToPool(gameObject.name, this.gameObject);
    }
}
