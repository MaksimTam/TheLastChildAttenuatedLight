using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Imapct3Trigger3 : MonoBehaviour
{
    public GameObject Impuls;
    public PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;
    public static bool left;
    public static bool True3;
    public GameObject WallImpact;
    public static bool TriggerTrue;
    public static bool TriggerWall3True;
    public GameObject Tentacle;
    public GameObject TentacleWall;
    private Animator AnimTent;
    private Animator AnimTentWall;

    public GameObject Map;
    private Animator MapAnim;

    public GameObject Barrier;

    public GameObject Trigger;
    public GameObject TriggerCentre;

    public Transform player; // ссылка на объект Player

    public float smoothSpeed; // скорость плавного перемещения

    private bool isMoving = false; // флаг, указывающий, движется ли объект
    private bool FlipTrue;


    private void Awake()
    {
        // Получаем компонент DepthOfField
        postProcessVolume.profile.TryGetSettings(out depthOfField);
    }
    private void Start()
    {
        Impuls.SetActive(false);
        WallImpact.SetActive(false);
        AnimTent = Tentacle.GetComponent<Animator>();
        AnimTentWall = TentacleWall.GetComponent<Animator>();
        MapAnim = Map.GetComponent<Animator>();
        AnimTent.enabled = false;
        Barrier.SetActive(false);
        FlipTrue = false;
        PlayerMovement.isMoving = false;
        TriggerTrue = false;
        TriggerWall3True = false;
        TriggerCentre.SetActive(false);
        True3 = false;
        AnimTentWall.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Impuls.SetActive(true);
            // Включаем DepthOfField при выходе игрока из зоны
            depthOfField.enabled.value = true;
            AnimTentWall.enabled = true;
            AnimTentWall.SetTrigger("Impact");
            ImpactWall3.KinWall3 = true;
            TriggerWall3True = true;
            Impact3.Kin3 = true;
            TriggerTrue = true;
            AnimTent.enabled = true;
            Barrier.SetActive(true);
            Invoke("BarrierExit", 10f);
            Invoke("FlipMap", 11.5f);
            Invoke("Wall", 12f);
            left = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerWall3True = false;
            TriggerTrue = false;
            Trigger.SetActive(false);
            AnimTentWall.enabled = false;
        }
    }
    private void BarrierExit()
    {
        MapAnim.SetTrigger("MpLeft");
        Barrier.SetActive(false);
        FlipTrue = true;
    }

    private void FlipMap()
    {
        MapAnim.SetTrigger("MpExitLeft");
        Invoke("Impact1", 1.4f);
    }
    void Update()
    {
        // Находим объект с тегом "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (FlipTrue == true)
        {
            PlayerMovement.isMoving = true;
        }
    }
    private void Impact1()
    {
        TriggerCentre.SetActive(true);
    }
    private void Wall()
    {
        True3 = true;
        WallImpact.SetActive(true);
    }
}
