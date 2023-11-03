using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector3;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody))]
public class PersControl : MonoBehaviour
{
    public static PersControl Instance;
    
    
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [Header("Player height")]
    [SerializeField] private float playerHeight = 1.6F;
    [SerializeField] private float playerHeightJump = 0.8F;
    [SerializeField] private float playerHeightCrunch = 0.8F;
    [SerializeField] private float playerHeightJumpRun = 1F;

    public PlayerState playerState;
    
    
    public float speed = 1f;
    public float speedFastRun = 2.5F;

    public bool isControllerEnabled = true;
    
    public float jump;
    public static bool jumpbool = false;
    private bool jumpanim;
    public float jumpFastRun = 1.8F;
    private float moveInput;
    private bool facingRight = true;
    public float stop;
  
    private Rigidbody rb;
   
    public Joystick joystick;
    private Animator anim;
    public static bool isGrounded;
    public Transform _GroundChackObject;
    private float checkRadius = 0.3f;
    private RaycastHit hit;



    public static bool _alwaysRun = false;
    private bool _joystikEnabled = false;
    [Header("Tests mode")] 
    public static bool setNormalMode = false;
    public bool setRunnerMode = false;
    [Header("Tests animations")]
    public bool runJump = false;


    private float _speedChangeCapsuleSize = 0.07F;
    private Coroutine _coroutine;
    private IEnumerator JumpHeight()
    {
        while (capsuleCollider.height > playerHeightJump)
        {
            capsuleCollider.height -= _speedChangeCapsuleSize;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        while (capsuleCollider.height < playerHeight)
        {
            capsuleCollider.height += _speedChangeCapsuleSize;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
    
    public void OnAnimationEventStep()
    {
        if(CtrlSound.Instance)
            CtrlSound.Instance.PlayPlayerSteps();
        else Debug.LogError("PersControl : OnAnimationEventStep : CtrlSound.Instance = NULL");
    }



    #region DIE

    [Header("Test die:")] 
    public static bool testDieUp = false;
    public bool testDieDown = false;
    public void DieOnFallTreeUp()
    {
        Debug.Log("DieOnFallTreeUp");
        PlayAnimationDieUp();
        OnPlayerDie();
    }
    
    public void DieOnFallTreeDown()
    {
        Debug.Log("DieOnFallTreeDown");
        PlayAnimationDieDown();
        OnPlayerDie();
    }
    
    private void OnPlayerDie()
    {
        Debug.Log("OnPlayerDie");
        SetControlEnabled(false);
        CtrlUi.Instance.ShowWndFadeToDark(() => {  GameLogic.Instance.LevelRestart(0); });
    }
    
    
    private void UpdateTestDie()
    {
        if (testDieUp)
        {
            DieOnFallTreeUp();
            testDieUp = false;
        }
        
        if (testDieDown)
        {
            DieOnFallTreeDown();
            testDieDown = false;
        }
    }
    
    
    #endregion
    
  

    public void SetRunnerMode()
    {
        Debug.Log("PersControl : SetRunnerMode");
        SetJoystickEnabled(false);
        _alwaysRun = true;
        PlayAnimationRunFast();
    }
    
    public void SetNormalMode()
    {
        Debug.Log("PersControl : SetNormalMode");
        SetJoystickEnabled(true);
        _alwaysRun = false;
    }

    public void SetJoystickEnabled(bool isEnabled)
    {
        Debug.Log("PersControl : SetJoystickEnabled : isEnabled = " + isEnabled);
        moveInput = 0F;
        if(joystick)joystick.gameObject.SetActive(isEnabled);
        _joystikEnabled = isEnabled;
        if (isEnabled == false)
        {
            anim.SetBool(IsRunning, false);
        }
    }

    private void UpdateTest()
    {
        if (setNormalMode)
        {
            this.SetNormalMode();
            setNormalMode = false;
        }
        
        if (setRunnerMode)
        {
            this.SetRunnerMode();
            setRunnerMode = false;
        }
        if (runJump)
        {
            this.SetRunnerMode();
            runJump = false;
        }
    }
    
    
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;

        Scaler.z *= -1;
        transform.localScale = Scaler;
    }

  
    public void OnJumpButtonDown()
    {
        if (!isControllerEnabled)
        {
            Debug.Log("IGNORE CONTROLLER. isControllerEnabled == " + isControllerEnabled );
            return;
        }
        
        if(_alwaysRun) return;
        Debug.Log("PersControl : OnJumpButtonDown");
      //  rb.AddForce(Vector3.up * 1000F * jump, ForceMode.VelocityChange);
        if (isGrounded == true)
        {
           
            isGrounded = false;
            rb.AddForce(Vector3.up * 1F * jump, ForceMode.VelocityChange);
            PlayAnimationJump();
            if(_coroutine!=null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(JumpHeight());
        }
    }


    #region ANIMATIONS

    private const string ANIM_RUN = "";

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int FastRun = Animator.StringToHash("FastRun");
    private static readonly int JumpRun = Animator.StringToHash("JumpRun");
    private static readonly int SlideDown = Animator.StringToHash("SlideDown");
    private static readonly int AnimJump = Animator.StringToHash("takeOff");
    
    private static readonly int DieDown = Animator.StringToHash("FallFlat");
    private static readonly int DieUp = Animator.StringToHash("FallUp");

    
    private void PlayAnimationRunJump()
    {
        Debug.Log("PersControl : PlayAnimationRunJump");
        anim.SetTrigger(JumpRun);
        playerState = PlayerState.RunJumping;
    }
    
    private void PlayAnimationJump()
    {
        Debug.Log("PersControl : PlayAnimationJump");
        anim.SetTrigger(AnimJump);
      

    }
    
    private void PlayAnimationRunFast()
    {
        Debug.Log("PersControl : PlayAnimationRunFast");
        anim.SetTrigger(FastRun);
        playerState = PlayerState.Run;
    }
    
    private void PlayAnimationSlideDown()
    {
        Debug.Log("PersControl : PlayAnimationSlideDown");
        anim.SetTrigger(SlideDown);
        playerState = PlayerState.RunSliding;
        StartCoroutine(RevertStateFromSlide());
    }

    
    //TODO костыль, надо как то стейт иначе делать
    private IEnumerator RevertStateFromSlide()
    {
        yield return new WaitForSeconds(2F);
        playerState = PlayerState.Run;
    }
    
    private void PlayAnimationDieDown()
    {
        Debug.Log("PersControl : PlayAnimationDieDown");
        anim.SetTrigger(DieDown);
        playerState = PlayerState.DieDown;
    }
    
    private void PlayAnimationDieUp()
    {
        Debug.Log("PersControl : PlayAnimationDieUp");
        anim.SetTrigger(DieUp);
        playerState = PlayerState.DieUp;
    }

    public void PlayAnimationIdle()
    {
        anim.SetBool(IsRunning, false);
        playerState = PlayerState.Idle;
    }

    
    #endregion

    #region SWIPES

    private void OnSwipeUp()
    {
        Debug.Log("PersControl : OnSwipeUp");
        if (!isControllerEnabled)
        {
            Debug.Log("IGNORE CONTROLLER. isControllerEnabled == " + isControllerEnabled );
            return;
        }
        PlayAnimationRunJump();
        jumpSound.Play();
    }
    
    private void OnSwipeDown()
    {
        Debug.Log("PersControl : OnSwipeDown");
        if (!isControllerEnabled)
        {
            Debug.Log("IGNORE CONTROLLER. isControllerEnabled == " + isControllerEnabled );
            return;
        }
        PlayAnimationSlideDown();
    }


    private bool testIsDown = false;
    private bool testIsUp = false;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    private void UpdateSwipeKeyEmulation()
    {
        if (testIsUp == false)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("UpdateSwipeKeyEmulation : UP");
                OnSwipeUp();
                testIsUp = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            testIsUp = false;
        }
        
        if (testIsDown == false)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("UpdateSwipeKeyEmulation : DOWN");
                testIsDown = true;
                OnSwipeDown();
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            testIsDown = false;
        }
    }
    
   
    
    #endregion

    private void UpdateKeyBoard()
    {
       // if(Input.)
    }


    private IEnumerator ControllerIgnoreOnStartLevel()
    {
        if(GameLogic.Instance.skipStandUp) yield break;
        SetControlEnabled(false);
        yield return new WaitForSeconds(11F);
        SetControlEnabled(true);
    }
    
    public void SetControlEnabled(bool isEnabled)
    {
        Debug.Log("PersControl : SetControlEnabled : isEnabled = " + isEnabled);
        isControllerEnabled = isEnabled;
    }


    private void UpdateMoving()
    {
        if(_joystikEnabled) moveInput = joystick.Horizontal;
        if (_alwaysRun) moveInput = 1F;
     

        var curSpeed = speed;
        if (_alwaysRun) curSpeed = speedFastRun;
        
        rb.velocity = new Vector2(moveInput * curSpeed, rb.velocity.y);



        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            
            Flip();
        }
        

        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            if(_alwaysRun == false) anim.SetBool("isRunning", true);

        }

    
    }

    #region  UNITY

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        FixedUpdate();
    }

    private void Start()
    {
        this.SetNormalMode();
        StartCoroutine(ControllerIgnoreOnStartLevel());
    }

    private void FixedUpdate()
    {
        if (isControllerEnabled)
        {
            UpdateMoving();
        }
    }
    
    private void Update()
    {
        isGrounded = Physics.Raycast(_GroundChackObject.transform.position, Vector3.down, checkRadius);

        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
            jumpbool = false;

        }
        else
        {
            jumpSound.Play();
            anim.SetBool("isJumping", true);
            jumpbool = true;

        }
        UpdateTest();
        UpdateTestDie();
        UpdateSwipeKeyEmulation();
    }

    #endregion
    
}

public enum PlayerState
{
    RunJumping,
    RunSliding,
    Run,
    DieDown,
    DieUp,
    Idle
}

