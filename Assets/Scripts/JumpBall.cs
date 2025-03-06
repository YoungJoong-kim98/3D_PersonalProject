using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBall : MonoBehaviour
{
    public float jumpPower; // ���� �� ����

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>(); // �÷��̾��� Rigidbody ��������
            if (playerRb != null)
            {
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z); // ���� Y �ӵ� �ʱ�ȭ
                playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // ���� �� ����
            }
        }
    }
}
