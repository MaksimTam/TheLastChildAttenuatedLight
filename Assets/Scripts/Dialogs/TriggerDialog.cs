using System;
using UnityEngine;

namespace Dialogs
{
    public class TriggerDialog : MonoBehaviour
    {
        [SerializeField] private Dialog dialog;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            var isDialogBeforeShowed = PlayerPrefs.HasKey(dialog.uid);
            CtrlUi.Instance.ShowWndDialog(dialog.messages, isDialogBeforeShowed);
            PlayerPrefs.SetInt(dialog.uid,1);
            gameObject.SetActive(false);
        }

        private void Start()
        {
            transform.GetComponent<Renderer>().enabled = false;
        }
    }
}
