using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // ��ü ������
    public Transform spawnPoint;  // ���� ��ġ
    public float spawnInterval = 2f; // ���� ���� (��)

    void Start()
    {
        // ���� �ð����� �� ����
        InvokeRepeating("SpawnBall", 0f, spawnInterval);
    }

    void SpawnBall()
    {
        Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
    }
}
