using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadLobby : MonoBehaviour
{
    AsyncOperation asyncOperation;
    public int SceneID;

    private void Start()
    {
        StartCoroutine(LoadSceneCor());
    }


    IEnumerator LoadSceneCor()
    {
        yield return new WaitForSeconds(15f);
        asyncOperation = SceneManager.LoadSceneAsync(SceneID);

        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.9f;
            yield return 0;
        }
    }
    private void Update()
    {
        
    }
}
