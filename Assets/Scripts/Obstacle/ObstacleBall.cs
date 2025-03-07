using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBall : MonoBehaviour
{
    public int damage;
    public float damageRate;
    private Rigidbody rb;  // ���� Rigidbody �߰�

    List<IDamagalbe> things = new List<IDamagalbe>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Rigidbody�� �������� ������ �ڿ������� �ϱ� ���� ����
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

            // �÷��̾�� �浹 �� �з����� �ϱ�
            if (other.TryGetComponent(out Rigidbody playerRb))
            {
                // �÷��̾��� �� �������� �з����� �ϱ�
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized; // �÷��̾�� ���� ��ġ ���̸� �̿��� �з����� ���� ���
                knockbackDirection.y = 0; // Y�� ������ 0���� �����Ͽ� ���� �������θ� �з����� ��
                playerRb.AddForce(knockbackDirection * 3000f, ForceMode.Impulse); // �з����� ���� ����
            }

            // ���� ���� �浹 �� ƨ�⵵�� �ϴ� ���� (���� ������ �ʰ� ����)
            Vector3 bounceDirection = (transform.position - other.transform.position).normalized;
            bounceDirection.y = 0; // ���� ���� ƨ�ܳ����� �ʵ��� Y���� 0���� ����
            rb.AddForce(bounceDirection * 2f, ForceMode.Impulse); // ���� ���� ���� ����
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
