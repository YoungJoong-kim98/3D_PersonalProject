using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Condition : MonoBehaviour
{
    public float curValue; // ���� ��
    public float startValue; //���۰�
    public float maxValue; // �ִ밪
    public float passiveValue; //�ֱ������� ��ȭ�ϴ� ��
    public Image uiBar; //�̹���
    void Start()
    {
        curValue = startValue;
    }

    // Update is called once per frame
    void Update()
    {
        // ui������Ʈ
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }
    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);
    }
}
