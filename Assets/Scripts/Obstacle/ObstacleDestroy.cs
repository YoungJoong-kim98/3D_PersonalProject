using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
        if(other.CompareTag("Player"))
        {

            other.transform.position = new Vector3(0, 2, 0);
        }
    }
}
