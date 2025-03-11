using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemData[] itemsToGive;
    public int quantityPerHit = 2; //몇 개 씩 나올지
    public int capacy; //몇 번 때릴 수 있는지

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for (int i = 0; i < quantityPerHit; i++)
        {
            if (capacy <= 0) break;
            capacy -= 1;

            // 무작위 아이템 선택
            ItemData selectedItem = itemsToGive[Random.Range(0, itemsToGive.Length)];

            
            //선택된 아이템 복사
            Instantiate(selectedItem.dropPrefab, hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up));

        }
    }
}
