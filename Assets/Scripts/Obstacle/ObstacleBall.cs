using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBall : MonoBehaviour
{
    public int damage;
    public float damageRate;
    private Rigidbody rb;  // 공의 Rigidbody 추가

    List<IDamagalbe> things = new List<IDamagalbe>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Rigidbody의 물리적인 반응을 자연스럽게 하기 위한 설정
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagalbe damagalbe))
        {
            things.Add(damagalbe);

            // 플레이어와 충돌 시 밀려나게 하기
            if (other.TryGetComponent(out Rigidbody playerRb))
            {
                // 플레이어의 뒤 방향으로 밀려나게 하기
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized; // 플레이어와 공의 위치 차이를 이용해 밀려나는 방향 계산
                knockbackDirection.y = 0; // Y축 방향을 0으로 설정하여 수평 방향으로만 밀려나게 함
                playerRb.AddForce(knockbackDirection * 3000f, ForceMode.Impulse); // 밀려나는 강도 조절
            }

            // 공이 벽과 충돌 시 튕기도록 하는 로직 (공이 날라가지 않게 설정)
            Vector3 bounceDirection = (transform.position - other.transform.position).normalized;
            bounceDirection.y = 0; // 공이 위로 튕겨나가지 않도록 Y축을 0으로 설정
            rb.AddForce(bounceDirection * 2f, ForceMode.Impulse); // 공의 반응 강도 조절
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamagalbe damagalbe))
        {
            things.Remove(damagalbe);
        }
    }
}
