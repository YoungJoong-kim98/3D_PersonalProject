using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // 구체 프리팹
    public Transform spawnPoint;  // 생성 위치 (X, Y 값만 사용)
    public float spawnInterval = 2f; // 생성 간격 (초)

    void Start()
    {
        // 일정 시간마다 공 생성
        InvokeRepeating("SpawnBall", 0f, spawnInterval);
    }

    void SpawnBall()
    {
        // Z 위치를 -10 ~ 10 사이의 랜덤 값으로 설정
        float randomZ = Random.Range(-10f, 10f);
        // 새로운 위치 생성 (X, Y는 spawnPoint 기준, Z만 랜덤)
        Vector3 spawnPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y, randomZ);

        Instantiate(ballPrefab, spawnPos, Quaternion.identity);
    }
}
