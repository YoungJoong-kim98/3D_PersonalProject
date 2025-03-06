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

    public void Heal(float amout) // 체력 회복
    {
        health.Add(amout);
    }
    public void Eat(float amout) //스태미너 회복
    {
        stamina.Add(amout);
        //stamina.maxValue += 10;
    }
    public void Die() // 플레이어 죽음
    {
        Debug.Log("죽음");
    }

    public void TakePhysicalDamage(int damage) //데미지 피격
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount) //스태미너
    {
        if (stamina.curValue - amount < 0f)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
}
