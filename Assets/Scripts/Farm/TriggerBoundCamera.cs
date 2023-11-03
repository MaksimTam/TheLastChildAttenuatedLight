using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoundCamera : MonoBehaviour
{
    
    public GameObject Bound;
    public Transform player;

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            CameraBounds.virtualCamera.Follow = player;
            Destroy(Bound);

        }
    }
}
