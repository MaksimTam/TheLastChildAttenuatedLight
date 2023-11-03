using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class ImpactLast : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;
    public GameObject Menu;
    public GameObject Inv;

    public GameObject Black;
    private Animator BAnim;
    public GameObject VidioObject;
    public GameObject Player;
    private Animator PAnim;
    public GameObject Monstr;
    private Animator MonAnim;
    public GameObject Camera;

    public GameObject Ten1;
    public GameObject Ten2;
    public GameObject Ten3;
    public GameObject Ten4;
    public GameObject Ten5;
    public GameObject Ten6;
    public GameObject Ten7;
    private Animator Ten1Anim;
    private Animator Ten2Anim;
    private Animator Ten3Anim;
    private Animator Ten4Anim;
    private Animator Ten5Anim;
    private Animator Ten6Anim;
    private Animator Ten7Anim;

    public GameObject Map;
    private Animator MapJump;

    private void Awake()
    {
        // Получаем компонент DepthOfField
        postProcessVolume.profile.TryGetSettings(out depthOfField);
    }
    private void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PAnim = Player.GetComponent<Animator>();
    }
    private void Start()
    {
        Black.SetActive(false);
        BAnim = Black.GetComponent<Animator>();
        VidioObject.SetActive(false);
        MonAnim = Monstr.GetComponent<Animator>();
        Camera.SetActive(false);
        MapJump = Map.GetComponent<Animator>();

        Ten1Anim = Ten1.GetComponent<Animator>();
        Ten2Anim = Ten2.GetComponent<Animator>();
        Ten3Anim = Ten3.GetComponent<Animator>();
        Ten4Anim = Ten4.GetComponent<Animator>();
        Ten5Anim = Ten5.GetComponent<Animator>();
        Ten6Anim = Ten6.GetComponent<Animator>();
        Ten7Anim = Ten7.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Imapct3Trigger3.left == true && Imapct2Trigger.right == true)
        {
            MonAnim.SetTrigger("Agressiv");
            Invoke("AnimTem", 1.5f);
            MapJump.SetTrigger("Jump");
            Inv.SetActive(false);
            Menu.SetActive(false);
        }
    }
    private void AnimTem()
    {
        // Выключаем DepthOfField при выходе игрока из зоны
        depthOfField.enabled.value = false;
        Black.SetActive(true);
        BAnim.SetTrigger("End");
        PAnim.SetTrigger("Falling");
        Camera.SetActive(true);
        Ten1Anim.SetTrigger("Impact");
        Ten2Anim.SetTrigger("Impact");
        Invoke("KillMap", 3.5f);
        Inv.SetActive(false);
        Menu.SetActive(false);

    }
    private void KillMap()
    {
        Ten3Anim.enabled = true;
        Ten4Anim.enabled = true;
        Ten5Anim.enabled = true;
        Ten6Anim.enabled = true;
        Ten7Anim.enabled = true;
        Invoke("KillGame", 1.4f);
        VidioObject.SetActive(true);
        Invoke("BlackOff", 0.5f);
        Inv.SetActive(false);
        Menu.SetActive(false);
    }
    private void KillGame()
    {
        Destroy(Ten1);
        Destroy(Ten2);
        Destroy(Ten3);
        Destroy(Ten4);
        Destroy(Ten5);
        Destroy(Ten6);
        Destroy(Ten7);
        Inv.SetActive(false);
        Menu.SetActive(false);
    }
    private void BlackOff()
    {
        Black.SetActive(false);
        Inv.SetActive(false);
        Menu.SetActive(false);
    }    
}
