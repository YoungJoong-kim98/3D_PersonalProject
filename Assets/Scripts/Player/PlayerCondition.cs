using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagalbe
{
    void TakePhysicalDamage(int damage);
}
public class PlayerCondition : MonoBehaviour, IDamagalbe
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }


    public event Action onTakeDamage;

    void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (health.curValue == 0f)
        {
            Die();
        }

    }

    public void Heal(float amout) // ü�� ȸ��
    {
        health.Add(amout);
    }
    public void Eat(float amout) //���¹̳� ȸ��
    {
        stamina.Add(amout);
        //stamina.maxValue += 10;
    }
    public void Die() // �÷��̾� ����
    {
        Debug.Log("����");
    }

    public void TakePhysicalDamage(int damage) //������ �ǰ�
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount) //���¹̳�
    {
        if (stamina.curValue - amount < 0f)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
}
