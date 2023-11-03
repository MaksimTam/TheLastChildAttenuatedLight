using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FallingTree : MonoBehaviour
{
  //  [SerializeField] private TriggersFallingTree triggerFallingTree;

    [SerializeField] private Transform pivotMeshTreeUp;
    [SerializeField] private Transform pivotMeshTreeDown;
    [SerializeField] private Transform pivotMeshTreeDownSeparated;

    [Header("Physics")] 
    [SerializeField] private Rigidbody separatedTreeUp; 
    [SerializeField] private Rigidbody separatedTreeDown; 
    [SerializeField] private Transform forcePos; 
    
    private float _distanceToStartFalling = 18F;

    [FormerlySerializedAs("_particleSystemCutting")]
    [Header("Particles")] 
    [SerializeField] private ParticleSystem particleSystemCutting;
    [SerializeField] private ParticleSystem particleSystemFall;
    
    private Transform _currentAnimatedTree = null;
    private float animationTime = 3F;

    private FallingTreeType _fallingTreeType;
    

    private bool _isFallingStarted = false;

    private void StartFallingTree()
    {
      if(_isFallingStarted) return;
      _isFallingStarted = true;
      CtrlSound.Instance.PlayFallingTree();
      var sequence = DOTween.Sequence();
      sequence.Pause();
      Vector3 preRot = new Vector3(2F, 1F, 5F);   
      Tween startFalling =  _currentAnimatedTree.DOLocalRotate(Vector3.zero, animationTime).SetEase(Ease.InCubic);
      Tween fallingJump =  _currentAnimatedTree.DOLocalRotate(preRot, 0.2F).SetEase(Ease.InCubic).OnComplete(OnCompleteFallJump);
      Tween fallingDone =  _currentAnimatedTree.DOLocalRotate(Vector3.zero, 0.3F).SetEase(Ease.OutBounce);
      sequence.Append(startFalling);
      sequence.Append(fallingJump);
      sequence.Append(fallingDone);
      //sequence.Append(_currentAnimatedTree.DOLocalRotate(Vector3.zero, animationTime)).SetEase(Ease.InCubic);
      //sequence.Append(_currentAnimatedTree.DOLocalRotate(preRot, animationTime/3F)).SetEase(Ease.OutElastic);
      sequence.Play();
      //  sequence.Append(_currentAnimatedTree.DOLocalRotate(preRot, 0.5F)).SetEase(Ease.InBounce);
      // sequence.Append(_currentAnimatedTree.DOLocalRotate(Vector3.zero, 0.5F)).SetEase(Ease.OutElastic);
    }


    private float _force = 200F;
    private void StartCrushTree()
    {
        Debug.Log("StartCrushTreeUp");
        CtrlSound.Instance.PlayCutTree();
        ShowTreeSeparated();
        StartParticleCutting();

        //EnablePhisics
        separatedTreeUp.isKinematic = false;
        separatedTreeDown.isKinematic = false;
        var position = forcePos.position;

        Vector3 forceDir = new Vector3(1F, 1F, 0);
        //separatedTreeDown.AddForceAtPosition(-right * _force, position, ForceMode.Impulse);
        //separatedTreeUp.AddForceAtPosition(right * _force, position, ForceMode.Impulse);
        separatedTreeDown.AddForceAtPosition(forceDir * _force, position, ForceMode.Impulse);
        separatedTreeUp.AddForceAtPosition(forceDir * _force, position, ForceMode.Impulse);

        StartCoroutine(HidePhysics(5F));
    }

    private IEnumerator HidePhysics(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        pivotMeshTreeDownSeparated.gameObject.SetActive(false);
    }

   

    private void OnCompleteFallJump()
    {
        StartParticleFall();
        GameLogic.Instance.CameraShakeFallTree();
    }
    
    


    #region PARTICLES

    private void StartParticleFall()
    {
        particleSystemFall.Play();
    }
    private void StartParticleCutting()
    {
        particleSystemCutting.Play();
    }
   
    #endregion
    
    
    #region Show Tree
    private void ShowRandomTree()
    {
        var boolean  = (Random.value > 0.5f);
       // if (ignoreRandom) boolean = true;
        if (boolean) ShowTreeUp();
        else ShowTreeDown();

       //ShowTreeUp();
        //ShowTreeDown();
        
        if (pivotMeshTreeDown.gameObject.activeSelf) _currentAnimatedTree = pivotMeshTreeDown;
        if (pivotMeshTreeUp.gameObject.activeSelf) _currentAnimatedTree = pivotMeshTreeUp;
        _currentAnimatedTree.localEulerAngles = new Vector3(90, 30, 0);
    }

    private void ShowTreeUp()
    {
        pivotMeshTreeUp.gameObject.SetActive(true);
        pivotMeshTreeDown.gameObject.SetActive(false);
        pivotMeshTreeDownSeparated.gameObject.SetActive(false);
        _fallingTreeType = FallingTreeType.Up;
        gameObject.name = "UpTree";
    }
    
    private void ShowTreeDown()
    {
        pivotMeshTreeDown.gameObject.SetActive(true);
        pivotMeshTreeUp.gameObject.SetActive(false);
        pivotMeshTreeDownSeparated.gameObject.SetActive(false);
        _fallingTreeType = FallingTreeType.Down;
        gameObject.name = "DownTree";
    }
    
    private void ShowTreeSeparated()
    {
        pivotMeshTreeDownSeparated.gameObject.SetActive(true);
        pivotMeshTreeUp.gameObject.SetActive(false);
        pivotMeshTreeDown.gameObject.SetActive(false);
        gameObject.name = "Separated";
    }

    

    #endregion
   
    
    private void SetupCallBacks()
    {
        /*
        if (triggerFallingTree)
        {
            triggerFallingTree.OnPlayerTouched = OnPlayerTouched;
            triggerFallingTree.OnEnemyTouched = OnHunterTouched;
        }
        else Debug.LogError("FallingTree : triggerFallingTree = NULL");
        */
    }
    
    private void  UpdatePlayerDistanceToFallingTree()
    {
        if(PersControl.Instance == null) return;
        //TODO Сделать что бы не апдейтились все деревья, а только ближние
        //TODO Сделать что бы минувшие деревья вообще перестали обновлять логику.
        var distance =  GetDistance(PersControl.Instance.transform);
        if (distance < _distanceToStartFalling)
        {
            StartFallingTree();
        }
    }


    private Vector3 _treePos;
    
    private void UpdatePlayerCrush()
    {
        if(PersControl.Instance == null) return;
        var state = PersControl.Instance.playerState;
        var playerPos = PersControl.Instance.transform.position;
        var playerX = playerPos.x;
        
        //Not update if player right
        if(playerX > _treePos.x) return;
      
        playerPos.y = 0;
        playerPos.z = 0;
        _treePos.y = 0;
        _treePos.z = 0;
        var distance = Vector3.Distance(playerPos, _treePos);
        if(distance > 0.5F) return;
        Debug.Log("TREE CHECKING PLAYER");
        
        //Узнать что ждем прыжок или подкат
        if (_fallingTreeType == FallingTreeType.Down)
        {
            //Нужен прыжок
            if (state != PlayerState.RunJumping)
            {
                Debug.Log("Player die . Tree = " + _fallingTreeType + "  player state = " + state);
                PersControl.Instance.DieOnFallTreeDown();
            }
        }
        
        if (_fallingTreeType == FallingTreeType.Up)
        {
            //Нужен подкат
            if (state != PlayerState.RunSliding)
            {
                Debug.Log("Player die . Tree = " + _fallingTreeType + "  player state = " + state);
                PersControl.Instance.DieOnFallTreeUp();
            }
        }
    }
    
    private float _distanceToJump = 3F;
    private float _distanceToAttackTree = 5F;
    private float _distanceToCrushTree = 2F;

    private bool _isJumped = false;
    private bool _isAttacked = false;
    private bool _isCrused = false;
    
    
    private void UpdateHunterDistanceToFallingTree()
    {
        if(CtrlHunterRun.Instance == null) return;
        var distance =  GetDistance(CtrlHunterRun.Instance.transform);
        //Jump
        if (_fallingTreeType == FallingTreeType.Down)
        {
            if (distance < _distanceToJump)
            {
                if (!_isJumped)
                {
                    CtrlHunterRun.Instance.StartJump();
                    _isJumped = true;
                }
            }
        }
        
        //Attack tree
        if (_fallingTreeType == FallingTreeType.Up)
        {
            if ( distance < _distanceToAttackTree)
            {
                if (!_isAttacked)
                {
                    CtrlHunterRun.Instance.StartAttack();
                    GameLogic.Instance.CameraShakeZoom(0.6F);
                    _isAttacked = true;
                }
            }
            //Crush tree
            if (distance < _distanceToCrushTree)
            {
                if (!_isCrused)
                {
                    StartCrushTree();
                    _isCrused = true;
                }
            }
        }
    }

    private float GetDistance(Transform somePlayer)
    {
        var playerPos = somePlayer.position;
        playerPos.y = 0;
        playerPos.z = 0;
        var treePos = transform.position;
        treePos.y = 0;
        treePos.z = 0;
        var distance = Vector3.Distance(playerPos, treePos);
        return distance;
    }
    
    #region TESTS

    [Header("Tests:")] 
    public bool testing = false; 
    public bool ignorePlayerDistance = false;
    public bool ignoreRandom = false;
    public bool showTreeUp = false;
    public bool showTreeDown = false;
    public bool showTreeSeparated = false;
    public bool startFalling = false;
    public bool startCrushing = false;
    
    private void UpdateTesting()
    {
        if (!testing) ignorePlayerDistance = false;
        if (!testing) ignoreRandom = false;
        if (!testing) return;

        if (startFalling)
        {
            StartFallingTree();
            startFalling = false;
        }
        
        if (showTreeUp)
        {
            ShowTreeUp();
            showTreeUp = false;
        }
        if (showTreeDown)
        {
            ShowTreeDown();
            showTreeDown = false;
        }
        
        if (showTreeSeparated)
        {
            ShowTreeSeparated();
            showTreeSeparated = false;
        }
        if (startCrushing)
        {
            StartCrushTree();
            startCrushing = false;
        }
    }

    #endregion

   
    
    private void Awake()
    {
        UpdateTesting();
        SetupCallBacks();
        ShowRandomTree();
        _treePos = transform.position;
        //triggerFallingTree.transform.GetComponent<Renderer>().enabled = false;
    }

    private void Update()
    {
        if(!ignorePlayerDistance) UpdatePlayerDistanceToFallingTree();
        UpdateHunterDistanceToFallingTree();
        UpdatePlayerCrush();
        UpdateTesting();
    }
}

public enum FallingTreeType
{
    Up,
    Down
}
