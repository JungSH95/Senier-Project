using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHpBar : MonoBehaviour
{
    public Transform enemy;

    public Slider hpSlider;
    public Slider backHpSlider;

    public TextMeshProUGUI hpText;

    public bool backHpHit = false;

    public float maxHp = 100f;
    public float currentHp = 100f;

    private void Start()
    {
        SetSlider();
    }

    void Update()
    {
        transform.position = enemy.position;
        hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime * 5f);

        hpText.text = string.Format("{0:0.##}", currentHp);

        if (backHpHit)
        {
            backHpSlider.value = Mathf.Lerp(backHpSlider.value, hpSlider.value, Time.deltaTime * 10f);
            // 따라 잡으면
            if(hpSlider.value >= backHpSlider.value - 0.01f)
            {
                backHpHit = false;      // 초기화
                backHpSlider.value = hpSlider.value;
            }
        }
    }

    public void SetSlider()
    {
        maxHp = enemy.gameObject.GetComponent<EnemyBase>().maxHp;
        currentHp = maxHp;
    }

    public void Dmg(float atk)
    {
        currentHp -= atk;

        if (currentHp <= 0)
            currentHp = 0;

        Invoke("BackHpStart", 0.5f);
    }

    void BackHpStart()
    {
        backHpHit = true;
    }
}
