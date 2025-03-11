using UnityEngine;
using System.Collections;

public class LaserTrap : MonoBehaviour
{
    public Transform laserStart;  // ������ ���� ��ġ
    public Transform laserEnd;    // ������ �� ��ġ
    public float laserDuration = 2f; // �������� ������ �ð�
    public int damage = 10;  // ������ ���ط�
    public float damageCooldown = 1f; // ���� ������ (1��)

    private LineRenderer lineRenderer;
    private bool isLaserActive = true;
    private float lastDamageTime = 0f; // ������ ���� �ð� ���

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // �� ���� ����Ʈ (����, ��)

        StartCoroutine(LaserControl()); // ������ ON/OFF ��ƾ ����

        // ������ �������� ������ ���� (�� ����)
        lineRenderer.SetPosition(0, laserStart.position);
        lineRenderer.SetPosition(1, laserEnd.position);
    }

    void Update()
    {
        if (isLaserActive)
        {
            // ������ �浹 ����
            RaycastHit hit;
            if (Physics.Raycast(laserStart.position, (laserEnd.position - laserStart.position).normalized, out hit, Vector3.Distance(laserStart.position, laserEnd.position)))
            {
                if (hit.collider.CompareTag("Player")) // �÷��̾� �浹 ����
                {
                    //  ���� ��ٿ� üũ
                    if (Time.time >= lastDamageTime + damageCooldown)
                    {
                        hit.collider.GetComponent<PlayerCondition>().TakePhysicalDamage(damage); // �÷��̾�� ���� �ֱ�
                        lastDamageTime = Time.time; // ������ ���� �ð� ������Ʈ
                    }
                }
            }
        }
    }

    IEnumerator LaserControl()
    {
        while (true)
        {
            isLaserActive = !isLaserActive; // ON/OFF ��ȯ
            lineRenderer.enabled = isLaserActive; // LineRenderer Ȱ��ȭ/��Ȱ��ȭ
            yield return new WaitForSeconds(laserDuration); // ���� �ð� ���
        }
    }
}
