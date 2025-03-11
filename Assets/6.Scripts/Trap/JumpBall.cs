using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBall : MonoBehaviour
{
    public float jumpPower; // 점프 힘 설정

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>(); // 플레이어의 Rigidbody 가져오기
            if (playerRb != null)
            {
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z); // 기존 Y 속도 초기화
                playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // 점프 힘 적용
            }
        }
    }
}
