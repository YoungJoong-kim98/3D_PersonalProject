using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemData[] itemsToGive;
    public int quantityPerHit = 2; //�� �� �� ������
    public int capacy; //�� �� ���� �� �ִ���

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for (int i = 0; i < quantityPerHit; i++)
        {
            if (capacy <= 0) break;
            capacy -= 1;

            // ������ ������ ����
            ItemData selectedItem = itemsToGive[Random.Range(0, itemsToGive.Length)];

            
            //���õ� ������ ����
            Instantiate(selectedItem.dropPrefab, hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up));

        }
    }
}
