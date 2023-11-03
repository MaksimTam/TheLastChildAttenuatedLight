using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
    public AudioSource Rain;
    public AudioSource RainStreet;

    public float fadeSpeed = 1f; // Скорость плавного уменьшения громкости
    private float targetVolume; // Целевая громкость
    private float targetVolume1; // Целевая громкость
    private bool fadingOut; // Флаг начала плавного уменьшения громкости
    private bool fadingOut1; // Флаг начала плавного уменьшения громкости
    public float targetFogStartDistance = 8f;
    public float targetFogStartDistance1 = 1.8f;// Желаемое значение fogStartDistance
    public float fogChangeSpeed = 3f;
    public float fogChangeSpeed1 = 3f; // Скорость изменения fogStartDistance
    public GameObject WalsAmbar;
    public GameObject BlackBigRoom;
    public Animator BlackBigRoomAnim;

    private float originalFogStartDistance;    // Исходное значение fogStartDistance
    private bool fogChanging;

    private void Start()
    {
        BlackBigRoom.SetActive(true);
        Rain.Stop();
        originalFogStartDistance = RenderSettings.fogStartDistance;
        fogChanging = false;
        BlackBigRoomAnim.enabled = false;
    }

    private void Update()
    {
        // Плавно уменьшаем громкость, если включен флаг плавного уменьшения
        if (fadingOut && RainStreet.volume > targetVolume)
        {

            RainStreet.volume -= fadeSpeed * Time.deltaTime;
        }
        else
        {

            RainStreet.volume += fadeSpeed * Time.deltaTime;
        }
        if (fadingOut1 && Rain.volume > targetVolume1)
        {

            Rain.volume -= fadeSpeed * Time.deltaTime;
        }
        else
        {

            Rain.volume += fadeSpeed * Time.deltaTime;
        }
        if (fogChanging == true)
        {
            float currentFogStartDistance = Mathf.Lerp(RenderSettings.fogStartDistance, targetFogStartDistance, fogChangeSpeed * Time.deltaTime);
            RenderSettings.fogStartDistance = currentFogStartDistance;

            // Проверяем, достигло ли значение fogStartDistance целевого значения
            if (Mathf.Approximately(currentFogStartDistance, targetFogStartDistance))
            {
                RenderSettings.fogStartDistance = currentFogStartDistance;
            }
        }
        if (fogChanging == false)
        {
            float currentFogStartDistance2 = Mathf.Lerp(RenderSettings.fogStartDistance, targetFogStartDistance1, fogChangeSpeed1 * Time.deltaTime);
            RenderSettings.fogStartDistance = currentFogStartDistance2;

            // Проверяем, достигло ли значение fogStartDistance целевого значения
            if (Mathf.Approximately(currentFogStartDistance2, targetFogStartDistance1))
            {
                RenderSettings.fogStartDistance = currentFogStartDistance2;
                Debug.Log("True");
            }

        }
    }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                fogChanging = true; // Выключаем параметр fog
                fadingOut = true;
                fadingOut1 = false;
                targetVolume = 0f;
                Rain.Play();
                WalsAmbar.SetActive(false);
                BlackBigRoom.SetActive(true);
                BlackBigRoomAnim.enabled = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                fogChanging = false;
                fadingOut = false;
                fadingOut1 = true;
                targetVolume = 0f;
                WalsAmbar.SetActive(true);
                BlackBigRoom.SetActive(false);
                BlackBigRoomAnim.enabled = true;
            }
        }
 }


