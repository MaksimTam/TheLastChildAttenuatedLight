using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHammer : MonoBehaviour
{
    public ParticleSystem particles;

    private void Start()
    {
        particles.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Impact"))
        {
            particles.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Impact"))
        {
            particles.Stop();
        }
    }
}