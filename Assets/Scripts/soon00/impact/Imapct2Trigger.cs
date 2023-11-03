using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Imapct2Trigger : MonoBehaviour
{
    public GameObject Impuls;
    public PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;
    public static bool right;
    public GameObject ImpactWall;
    public static bool True2;
    public static bool TriggerTrue;
    public static bool TriggerWallTrue;
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

    private bool FlipTrue;

    private void Awake()
    {
        // Получаем компонент DepthOfField
        postProcessVolume.profile.TryGetSettings(out depthOfField);
    }
    private void Start()
    {
        Impuls.SetActive(false);
        ImpactWall.SetActive(false);
        AnimTent = Tentacle.GetComponent<Animator>();
        AnimTentWall = TentacleWall.GetComponent<Animator>();
        MapAnim = Map.GetComponent<Animator>();
        Barrier.SetActive(false);
        FlipTrue = false;
        PlayerMovement.isMoving = false;
        TriggerTrue = false;
        TriggerWallTrue = false;
        TriggerCentre.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Impuls.SetActive(true);
            // Включаем DepthOfField при выходе игрока из зоны
            depthOfField.enabled.value = true;
            True2 = true;
            AnimTentWall.SetTrigger("Impact");
            ImpactWall1.KinWall = true;
            TriggerWallTrue = true;
            Impact2.Kin = true;
            TriggerTrue = true;
            AnimTent.enabled = true;
            Barrier.SetActive(true);
            Invoke("BarrierExit", 10f);
            Invoke("FlipMap", 11.5f);
            Invoke("Wall", 12f);
            right = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerWallTrue = false;
            TriggerTrue = false;
            Trigger.SetActive(false);
        }
    }
    private void BarrierExit()
    {
        Barrier.SetActive(false);
        MapAnim.SetTrigger("Mp");
        FlipTrue = true;
    }

    private void FlipMap()
    {
        MapAnim.SetTrigger("MpExit");
        Invoke("Impact1", 1.4f);
    }
    void Update()
    {
        // Находим объект с тегом "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (FlipTrue == true)
        {
            True2 = true;
            PlayerMovement.isMoving = true;
        }
    }
    private void Impact1()
    {
        TriggerCentre.SetActive(true);
    }
    private void Wall()
    {
        ImpactWall.SetActive(true);
    }
}
