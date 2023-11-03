using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using VideoClip;
using Object = System.Object;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance;
    
    private GameObject _playerSpawnPoint;

    [Header("Prefabs:")]
    [SerializeField] private Transform prefabPlayer;
    [SerializeField] private Transform prefabRunningHounter;
    [SerializeField] private Transform prefabCameraPlayer;
    [SerializeField] private Transform prefabPlayerBlood;
    
    
    [Header("Camera:")]
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;
    
    private PersControl _persControl = null;
    private Transform _persTransform = null;
    private Transform _hunterRunTransform = null;
    private GameObject _hunterSit = null;
    private bool _isRunHunter = false;

    [Header("Skip stand up player:")] public bool skipStandUp = false;
    
    public void StartGame()
    {
        Debug.Log("StartGame");
        if (_playerSpawnPoint)
        {
            _persTransform =GetPlayer();
            SetupCamera(_persTransform);
        }
    }
    
    private void SetupFinishPoint()
    {
        var finishPoint = GameObject.FindFirstObjectByType<FinishPlayerPoint>();
    }

    private void KillAllDarkForest()
    {
        Debug.Log("KillAllDarkForest");
        //Kill player
        _persTransform.gameObject.SetActive(false);
        //KillHunter
      if(_hunterRunTransform)  _hunterRunTransform.gameObject.SetActive(false);
        //Stop all sound in level
        Destroy(CtrlSound.Instance.gameObject);
    }

    public void LevelRestart(float delay)
    {
        Debug.Log("LevelRestart");
        Debug.LogError("НЕ РЕАЛИЗОВАНО");
        var currScene = SceneManager.GetActiveScene().name;
        StartCoroutine(LevelRestartDelayed(delay, currScene));
    }

    private IEnumerator LevelRestartDelayed(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(LoadYourAsyncScene(sceneName));
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    
    
    
    private Transform GetPlayer()
    {
        var obj = Instantiate(prefabPlayer, _playerSpawnPoint.transform.position, Quaternion.Euler(new Vector3(0F,90F,0F)));
        _persControl = obj.transform.GetComponent<PersControl>();
        DynamicJoystick joystick = GameObject.FindObjectOfType<DynamicJoystick>();
        _persControl.joystick = joystick;
        return obj.transform;
    }

    private Transform GetHunterRun()
    {
        var obj = Instantiate(prefabRunningHounter, _persTransform.position - Vector3.left, Quaternion.Euler(new Vector3(0F,90F,0F)));
        //_persControl = obj.transform.GetComponent<PersControl>();
        //DynamicJoystick joystick = GameObject.FindObjectOfType<DynamicJoystick>();
        //_persControl.joystick = joystick;
        return obj.transform;
    }

    #region Camera

    private void SetupCamera(Transform player)
    {
        var camPos = player.position + Vector3.back * 5F + Vector3.left * 2F + Vector3.back * 5F ;
        var cam =Instantiate(prefabCameraPlayer, camPos, Quaternion.Euler(new Vector3(0,6F,0))); 
        CinemachineVirtualCamera virtualCamera = cam.GetComponent<CinemachineVirtualCamera>();
        virtualCamera.m_Follow = player;
        virtualCamera.m_LookAt = player;
    }
    
    //TODO create camera controller

    public void CameraShakeZoom(float delay)
    {
        Vector3 vel = new Vector3(0, -0.2F, 0.5F);
        StartCoroutine(CameraShakeDelayed(delay, vel));
    }
    
    public void CameraShakeFallTree()
    {
        Vector3 vel = new Vector3(0, 0.1F, 0);
        CameraShake(vel);
    }
    
    public void CameraShakePlayerRunTouchTree()
    {
        Vector3 vel = new Vector3(0.5F,-0.2F,0);
        CameraShake(vel);
    }


    private IEnumerator CameraShakeDelayed(float delay, Vector3 dir)
    {
        yield return new WaitForSeconds(delay);
        CameraShake(dir);
    }
    
    
    private void CameraShake(Vector3 dir)
    {
        Debug.Log("CameraShake");
        cinemachineImpulseSource.GenerateImpulse(dir);
    }

    
    

    [Header("Test Cam Shakes:")] 
    public bool shakeFallingTree = false;
    public bool shakePlayerRunTouchTree = false;
    private void UpdateTestCamShakes()
    {
        if (shakeFallingTree)
        {
            CameraShakeFallTree();
            shakeFallingTree = false;
        }
        
        if (shakePlayerRunTouchTree)
        {
            CameraShakePlayerRunTouchTree();
            shakePlayerRunTouchTree = false;
        }
    }
    
    
    #endregion




    #region VIDEO

    public void OnDialogHunterFinish()
    {
        Debug.Log("OnDialogHunterFinish");
        PlayVideoHunterDialog();
        PersControl.Instance.SetJoystickEnabled(false);
    }
    
    private void PlayVideoHunterDialog()
    {
        Debug.Log("PlayVideoHunterDialog");
        if(CtrlVideoPlayer.Instance) CtrlVideoPlayer.Instance.PlayVideo(VideoType.FirstLevelHunter, OnPlayVideoFinishedHunterDialog);
        else Debug.LogError("PlayVideoHunterDialog : CtrlVideoPlayer.Instance = NULL");
    }

    
    private void OnPlayVideoFinishedHunterDialog()
    {
        Debug.Log("OnPlayVideoFinishedHunterDialog");
        PersControl.Instance.SetRunnerMode();
        if(_hunterSit)_hunterSit.SetActive(false);
        else Debug.LogError("GameLogic : OnPlayVideoFinished : _hunterSit = NULL");
        _hunterRunTransform = GetHunterRun();
        _hunterRunTransform.position = _playerPos - _hunterOffsetRun;
        if (_hunterRunTransform) _isRunHunter = true;
        else Debug.LogError("_hunterRunTransform = NULL");
    }

    #endregion

    


    private Vector3 _hunterOffsetRun;
    private readonly Vector3 _hunterOffsetDieDown = new Vector3(-2,0,0);
    private readonly Vector3 _hunterOffsetDieUp = new Vector3(1,0,0);
    private float _hunterRunDistancePlayerMin = 0.1F;
    private float _hunterRunDistancePlayerMax = 0.6F;
    private float _distanceUpdateEveryTime = 1.5F;
    private float _distanceUpdatedLastTime = 0F;

    private Vector3 _playerPos;
    
    private void UpdateHunterRunPos()
    {
        if(_isRunHunter == false) return;
        if(_hunterRunTransform == null) return;
        UpdateRandDistanceRunHunter();
        _playerPos = _persTransform.position;
        _playerPos.y = 0;
        _hunterRunTransform.position = Vector3.Lerp(_hunterRunTransform.position,  _playerPos - _hunterOffsetRun, Time.deltaTime * 2F);
        if (PersControl.Instance.playerState == PlayerState.DieDown || PersControl.Instance.playerState == PlayerState.DieUp)
        {
            Debug.Log("KILLING PLAYER");
            //Run to player and kill
            if(PersControl.Instance.playerState == PlayerState.DieDown)_hunterOffsetRun = _hunterOffsetDieDown;
            if(PersControl.Instance.playerState == PlayerState.DieUp)_hunterOffsetRun = _hunterOffsetDieUp;
            HunterKillingPlayer(PersControl.Instance.playerState);
        }
    }

    private bool isKillingPlayerStarted = false;
    private void HunterKillingPlayer(PlayerState state)
    {
        if(isKillingPlayerStarted) return;
        isKillingPlayerStarted = true;
        //TODO drop player
        CtrlHunterRun.Instance.StartAttack();
        StartCoroutine(ShowPlayerBlood(1F, state));
        //TODO Reset triggers Hunter
    }

    private IEnumerator ShowPlayerBlood(float delay, PlayerState state)
    {
        yield return new WaitForSeconds(delay);
        var posOffset = Vector3.zero;
        var posUp = Vector3.up * 0.5F;
        if(state == PlayerState.DieDown) posOffset = _persTransform.position - _hunterOffsetDieDown + posUp;
        if(state == PlayerState.DieUp) posOffset = _persTransform.position - _hunterOffsetDieUp + posUp;
        var blood = Instantiate(prefabPlayerBlood, posOffset, Quaternion.Euler(0,0F,90));
        blood.localScale = Vector3.one * 7F;
        //RESET ALL TRIGGERS
        CtrlHunterRun.Instance.ResetAllTriggers();
        CtrlHunterRun.Instance.StartAttack();
        StartCoroutine(ShowPlayerBloodAgain(1F, state));
    }
    
    private IEnumerator ShowPlayerBloodAgain(float delay, PlayerState state)
    {
        yield return new WaitForSeconds(delay);
        var posOffset = Vector3.zero;
        var posUp = Vector3.up * 0.5F;
        if(state == PlayerState.DieDown) posOffset = _persTransform.position - _hunterOffsetDieDown + posUp;
        if(state == PlayerState.DieUp) posOffset = _persTransform.position - _hunterOffsetDieUp + posUp;
        var blood = Instantiate(prefabPlayerBlood, posOffset, Quaternion.Euler(0,0F,90));
        blood.localScale = Vector3.one * 7F;
        //RESET ALL TRIGGERS
        CtrlHunterRun.Instance.ResetAllTriggers();
        CtrlHunterRun.Instance.StartAttack();
    }
    
    
    private void UpdateRandDistanceRunHunter()
    {
        if (Time.time > _distanceUpdatedLastTime +_distanceUpdateEveryTime)
        {
            _hunterOffsetRun.x = Random.Range(_hunterRunDistancePlayerMin, _hunterRunDistancePlayerMax);
            _hunterOffsetRun.y = 0;
            _hunterOffsetRun.z = 0;
            _distanceUpdatedLastTime = Time.time;
        }
    }
    
    private void Awake()
    {
        Instance = this;
        _playerSpawnPoint = GameObject.FindWithTag("SpawnPlayerPoint");
        var hunter = GameObject.FindObjectOfType<DialogButton>();
        if (hunter)
        {
            _hunterSit = GameObject.FindObjectOfType<DialogButton>().gameObject;
        }
    }

    private void Start()
    {
        StartGame();
        SetupFinishPoint();
    }

    private void Update()
    {
        UpdateHunterRunPos();
        UpdateTestCamShakes();
    }
}
