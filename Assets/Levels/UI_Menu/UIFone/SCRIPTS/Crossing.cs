using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Video;


public class Crossing : MonoBehaviour
{
    [SerializeField] public float timer;
    public  GameObject videoPlayer;

    // Start is called before the first frame update
    void VPlay()
    {

    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            videoPlayer.gameObject.SetActive(false);
            if (videoPlayer == false)
            {
            }

        }

    }

}
