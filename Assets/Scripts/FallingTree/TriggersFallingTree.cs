using System;
using UnityEngine;

    public class TriggersFallingTree : MonoBehaviour
    {
        public Action OnPlayerTouched = null;
        public Action OnEnemyTouched = null;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("TriggersFallingTree : OnTriggerEnter : player touched");
              //  OnPlayerTouched?.Invoke();
            }
            
            if (other.CompareTag("HunterRun"))
            {
                Debug.Log("TriggersFallingTree : OnTriggerEnter : hunter touched");
              //  OnEnemyTouched?.Invoke();
            }
           
        }
    }

