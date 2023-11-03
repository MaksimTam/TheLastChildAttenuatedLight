using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalsMill : MonoBehaviour
{
    public GameObject Wals;

    private void Start()
    {
        Wals.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Wals.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Wals.SetActive(false);
        }
    }
}
