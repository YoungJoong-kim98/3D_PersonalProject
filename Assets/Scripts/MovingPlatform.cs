using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA, pointB;
    public float speed;
    private Vector3 target;
    private bool isMoving = false;

    private void Start()
    {
        target = transform.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                target = (target == pointA.position) ? pointB.position : pointA.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            isMoving = true;
            other.transform.SetParent(transform); // 플레이어를 발판의 자식으로 설정
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = false;
            other.transform.SetParent(null); // 발판에서 플레이어를 다시 분리
        }
    }
}
