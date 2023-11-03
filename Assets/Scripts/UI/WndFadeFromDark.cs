using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WndFadeFromDark : MonoBehaviour
{
    private readonly float _duration= 4F;
    private Image _image;
    private Tween _tween = null;
    
    public void StartFade(Action onFadeEnd)
    {
        if(_image == null) _image = transform.GetComponent<Image>();
        _tween?.Kill();
        _tween =_image.DOFade( 0F, _duration).OnComplete(() => onFadeEnd?.Invoke());;
    }
}
