using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // 구체 프리팹
    public Transform spawnPoint;  // 생성 위치
    public float spawnInterval = 2f; // 생성 간격 (초)

    void Start()
    {
        // 일정 시간마다 공 생성
        InvokeRepeating("SpawnBall", 0f, spawnInterval);
    }

    void SpawnBall()
    {
        Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
    }
}
