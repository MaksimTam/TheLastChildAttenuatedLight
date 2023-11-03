using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OpenSceneIn : MonoBehaviour
{
    public Image screenOverlay;
    public float fadeDuration = 3f;
    public string nextSceneName = "Inventory";

    private bool isTransitioning = false;

    public void OnButtonClick()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToScene());
        }
    }

    private IEnumerator TransitionToScene()
    {
        isTransitioning = true;

        // Добавляем компонент Image на экран, настраиваем его на полный screen размер и задаем начальный цвет черный
        screenOverlay.gameObject.SetActive(true);
        screenOverlay.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
        screenOverlay.color = Color.black;

        // Запускаем анимацию затемнения
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            screenOverlay.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        // Загружаем сцену Inventory
        SceneManager.LoadScene(nextSceneName);
    }
}
