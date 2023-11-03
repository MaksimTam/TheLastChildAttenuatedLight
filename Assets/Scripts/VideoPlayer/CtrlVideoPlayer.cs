using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace VideoClip
{
    public class CtrlVideoPlayer : MonoBehaviour
    {
        public static CtrlVideoPlayer Instance;
        [SerializeField] private bool enableProgressSlider = true;
        [SerializeField] private bool enableSkipButton = true;
        [SerializeField] private UnityEngine.Video.VideoPlayer player;
        [SerializeField] private GameObject pivotPlayerCanvas;
        [SerializeField] private Button buttonSkipVideo; 
        [SerializeField] private Slider sliderProgress; 
        [SerializeField] private List<Video> videos; 
        private Action _onVideoFinished;

        [Header("Test:")] public bool testNow = false;
        
       public void PlayVideo(VideoType videoType, Action onVideoFinished)
       {
           transform.SetAsLastSibling();
           _onVideoFinished = onVideoFinished;
           player.enabled = true;
           var video = FindVideo(videoType);
           if (video == null)
           {
               Debug.LogError("CtrlVideoPlayer : PlayVideo : video = NULL");
               return;
           }
           player.clip = video;
           player.Prepare();
       }

      
       private UnityEngine.Video.VideoClip FindVideo(VideoType videoType)
       {
           return (from video in videos where video.videoType == videoType select video.videoClip).FirstOrDefault();
       }
       

        private void SkipVideo()
        {
            Debug.Log("CtrlVideoPlayer : SkipVideo");
            player.Stop();
            player.enabled = false;
            ShowPanel(false);
            _onVideoFinished?.Invoke();
        }


        private void ShowPanel(bool isShow)
        {
            Debug.Log("CtrlVideoPlayer : ShowPanel : isShow = " + isShow);
            if(pivotPlayerCanvas)pivotPlayerCanvas.SetActive(isShow);
            else Debug.LogError("CtrlVideoPlayer : ShowPanel : PivotPlayerCanvas = NULL");
        }

        private void UpdateProgress()
        {
            if (player.isPlaying)
            {
                if(sliderProgress)sliderProgress.value = PlayedFraction();
            }
        }

        private float PlayedFraction()
        {
            var fraction = (float)player.frame / (float)player.clip.frameCount;
            return fraction;
        }
        
        #region EVENTS
        private void OnPrepareCompleted(UnityEngine.Video.VideoPlayer source)
        {
            Debug.Log("CtrlVideoPlayer : OnPrepareCompleted");
            ShowPanel(true);
            source.Play();
        }

        private void OnEndReached(UnityEngine.Video.VideoPlayer vp)
        {
            Debug.Log("CtrlVideoPlayer : OnEndReached");
            player.Stop();
            player.clip = null;
            player.enabled = false;
            ShowPanel(false);
            _onVideoFinished?.Invoke();
        }
       
        private void OnErrorReceived(UnityEngine.Video.VideoPlayer source, string message)
        {
            Debug.LogError("CtrlVideoPlayer : OnErrorReceived : " + message);
            player.Stop();
            player.clip = null;
            player.enabled = false;
            _onVideoFinished?.Invoke();
            ShowPanel(false);
        }
        #endregion

        #region UNITY

        private void Awake()
        {
            Instance = this;
            buttonSkipVideo.gameObject.SetActive(enableSkipButton);
            if (enableSkipButton)buttonSkipVideo.onClick.AddListener(SkipVideo);
            player.playOnAwake = false;
            player.isLooping = false;
            player.prepareCompleted += OnPrepareCompleted;
            player.loopPointReached += OnEndReached;
            player.errorReceived += OnErrorReceived;
            player.clip = null;
            player.enabled = false;
            ShowPanel(false);
            sliderProgress.gameObject.SetActive(enableProgressSlider);  
        }

        private void Start()
        {
            transform.SetAsLastSibling();
        }

        private void Update()
        {
            if(enableProgressSlider) UpdateProgress();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SkipVideo();
            }
            if (testNow)
            {
                PlayVideo(VideoType.FirstLevelHunter, null);
                testNow = false;
            }
        }
        private void OnDestroy()
        {
            player.prepareCompleted -= OnPrepareCompleted;
            player.loopPointReached -= OnEndReached;
            player.errorReceived -= OnErrorReceived;
        }

        #endregion
        
    }

    public enum VideoType
    {
        FirstLevelHunter,
        FirstLevelFinal,
        SomeNextNextVideo
    }

    [Serializable]
    public class Video
    {
        public VideoType videoType;
        public UnityEngine.Video.VideoClip videoClip;
    }
}
