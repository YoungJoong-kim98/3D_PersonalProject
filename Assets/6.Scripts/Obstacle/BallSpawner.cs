using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // ��ü ������
    public Transform spawnPoint;  // ���� ��ġ (X, Y ���� ���)
    public float spawnInterval = 2f; // ���� ���� (��)

    void Start()
    {
        // ���� �ð����� �� ����
        InvokeRepeating("SpawnBall", 0f, spawnInterval);
    }

    void SpawnBall()
    {
        // Z ��ġ�� -10 ~ 10 ������ ���� ������ ����
        float randomZ = Random.Range(-10f, 10f);
        // ���ο� ��ġ ���� (X, Y�� spawnPoint ����, Z�� ����)
        Vector3 spawnPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y, randomZ);

        Instantiate(ballPrefab, spawnPos, Quaternion.identity);
    }
}
