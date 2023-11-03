using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class CtrlUi : MonoBehaviour
{
    [SerializeField] private Button buttonJump;

   
    [SerializeField] private WndDialog wndDialog;
    [SerializeField] private WndGamePlay wndGamePlay;
    [Header("Fade screens:")]
    [SerializeField] private GameObject wndFadeInOut;
    [SerializeField] private WndFadeToDark wndFadeToDark;
    [SerializeField] private WndFadeFromDark wndFadeFromDark;

    [Header("Tests:")] public bool isShowFadeInOut = false;
    
    public static CtrlUi Instance;


    #region Fade screens

    //On player die
    public void ShowWndFadeToDark(Action onFadeFinished)
    {
        Debug.Log("CtrlUi : ShowWndFadeToDark");
        wndFadeToDark.gameObject.SetActive(true);
        wndFadeToDark.StartFade(onFadeFinished);
    }
    
    //On level start
    public void ShowWndFadeFromDark()
    {
        Debug.Log("CtrlUi : ShowWndFadeFromDark");
        wndFadeFromDark.gameObject.SetActive(true);
        wndFadeFromDark.StartFade(() => { wndFadeFromDark.gameObject.SetActive(false); } );
    }
    
    public void ShowWndFadeOut()
    {
        Debug.Log("CtrlUi : ShowWndFadeOut");
        wndFadeInOut.SetActive(true);
    }
    
    #endregion
    
    private void HideAll()
    {
        wndGamePlay.gameObject.SetActive(false);
        wndDialog.gameObject.SetActive(false);
        wndFadeToDark.gameObject.SetActive(false);
        wndFadeFromDark.gameObject.SetActive(false);
    }
    
    
    public void ShowWndDialog(List<string> messages, bool isShowSkipAllButton)
    {
        Debug.Log("CtrlUi : ShowWndDialog");
        wndDialog.gameObject.SetActive(true);
        wndDialog.LoadMessages(messages, isShowSkipAllButton);
    }
    
    
    private void OnClickJump()
    {
        Debug.Log("CtrlUi : OnClickJump");
        PersControl.Instance.OnJumpButtonDown();
    }

    private void Awake()
    {
        Instance = this;
        HideAll();
        buttonJump.onClick.AddListener(OnClickJump);
       
    }

    private void Start()
    {
        ShowWndFadeFromDark();
    }

    private void Update()
    {
        if (isShowFadeInOut)
        {
            ShowWndFadeOut();
            isShowFadeInOut = false;
        }
    }
}
