using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WndFadeToDark : MonoBehaviour
    {
        private readonly float _duration= 4F;
        private Image _image;
        private Tween _tween = null;
    
        public void StartFade(Action onFadeEnd)
        {
            if(_image == null) _image = transform.GetComponent<Image>();
           
            _tween?.Kill();
            _tween =_image.DOFade( 1F, _duration).OnComplete(() => onFadeEnd?.Invoke());;
        }
    }
}
