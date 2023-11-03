using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WndDialog : MonoBehaviour
    {
        [SerializeField] private Button buttonNext;
        [SerializeField] private Button buttonSkipAll;
        [SerializeField] private TMP_Text textMessage;
        
        private List<String> _messages;
        
        public void LoadMessages(List<String> messages, bool isShowSkipButton)
        {
            Debug.Log("WndDialog : LoadMessages : count =" + messages.Count + " isShowSkipButton = " + isShowSkipButton);
            _messages = new List<string>(messages);
            _messages.Add(""); //Null message for end click
            textMessage.text = "";
            buttonSkipAll.gameObject.SetActive(isShowSkipButton);
            PersControl.Instance.SetJoystickEnabled(false);
            NextMessage();
        }

        private int _counter = 0;
        private void NextMessage()
        {
            if(_messages.Count < 1) return;
            textMessage.text = _messages[_counter];
            _counter++;
            if (_counter >= _messages.Count) OnMessagesEnd();
        }

        public void OnClickButtonNext()
        {
            Debug.Log("WndDialog : OnClickButtonNext");
            NextMessage();
        }
        
        public void OnClickButtonSkipAll()
        {
            Debug.Log("WndDialog : OnClickButtonSkipAll");
            OnMessagesEnd();
        }

        private void OnMessagesEnd()
        {
            Debug.Log("WndDialog : OnMessagesEnd");
            CleanUp();
            PersControl.Instance.SetJoystickEnabled(true);
            gameObject.SetActive(false);
            //TODO потом оформить в калбек, что бы не пересекалось с другими диалогами
            GameLogic.Instance.OnDialogHunterFinish();
        }

        private void CleanUp()
        {
            _counter = 0;
            _messages.Clear();
        }
        
        private void Awake()
        {
            buttonNext.onClick.AddListener(OnClickButtonNext);
            buttonSkipAll.onClick.AddListener(OnClickButtonSkipAll);
        }
    }
}
