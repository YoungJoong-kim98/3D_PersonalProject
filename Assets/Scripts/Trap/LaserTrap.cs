using UnityEngine;
using System.Collections;

public class LaserTrap : MonoBehaviour
{
    public Transform laserStart;  // 레이저 시작 위치
    public Transform laserEnd;    // 레이저 끝 위치
    public float laserDuration = 2f; // 레이저가 켜지는 시간
    public int damage = 10;  // 레이저 피해량
    public float damageCooldown = 1f; // 피해 딜레이 (1초)

    private LineRenderer lineRenderer;
    private bool isLaserActive = true;
    private float lastDamageTime = 0f; // 마지막 피해 시간 기록

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // 두 개의 포인트 (시작, 끝)

        StartCoroutine(LaserControl()); // 레이저 ON/OFF 루틴 시작

        // 레이저 시작점과 끝점을 설정 (한 번만)
        lineRenderer.SetPosition(0, laserStart.position);
        lineRenderer.SetPosition(1, laserEnd.position);
    }

    void Update()
    {
        if (isLaserActive)
        {
            // 레이저 충돌 감지
            RaycastHit hit;
            if (Physics.Raycast(laserStart.position, (laserEnd.position - laserStart.position).normalized, out hit, Vector3.Distance(laserStart.position, laserEnd.position)))
            {
                if (hit.collider.CompareTag("Player")) // 플레이어 충돌 감지
                {
                    //  피해 쿨다운 체크
                    if (Time.time >= lastDamageTime + damageCooldown)
                    {
                        hit.collider.GetComponent<PlayerCondition>().TakePhysicalDamage(damage); // 플레이어에게 피해 주기
                        lastDamageTime = Time.time; // 마지막 피해 시간 업데이트
                    }
                }
            }
        }
    }

    IEnumerator LaserControl()
    {
        while (true)
        {
            isLaserActive = !isLaserActive; // ON/OFF 전환
            lineRenderer.enabled = isLaserActive; // LineRenderer 활성화/비활성화
            yield return new WaitForSeconds(laserDuration); // 일정 시간 대기
        }
    }
}
