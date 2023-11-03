using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostTrigger : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public string triggerTag = "Player";

    private DepthOfField depthOfField;
    public GameObject BlackBigRoom;
    public Animator BlackBigRoomAnim;
    public GameObject Door;
    public GameObject Door1;

    private void Start()
    {
        BlackBigRoomAnim.SetBool("Exit", true);
        Door.SetActive(false);
        Door1.SetActive(false);
    }

    private void Awake()
    {
        // Получаем компонент DepthOfField
        postProcessVolume.profile.TryGetSettings(out depthOfField);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            // Выключаем DepthOfField при входе игрока в зону
            depthOfField.enabled.value = false;
            BlackBigRoomAnim.enabled = true;
            BlackBigRoomAnim.SetBool("isEnter", true);
            BlackBigRoomAnim.SetBool("Exit", false);


            Door.SetActive(true);
            Door1.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            // Включаем DepthOfField при выходе игрока из зоны
            depthOfField.enabled.value = true;

            BlackBigRoomAnim.enabled = true;
            BlackBigRoomAnim.SetBool("Exit", true);
            BlackBigRoomAnim.SetBool("isEnter", false);


            Door.SetActive(false);
            Door1.SetActive(false);
        }
    }
    private void OnExit()
    {
        BlackBigRoomAnim.enabled = false;
    }
    private void OnEnter()
    {
        BlackBigRoomAnim.enabled = false;
    }
}