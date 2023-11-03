using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WndFadeInOut : MonoBehaviour
    {
        private readonly float _durationToOne = 4F;
        private readonly float _durationToZero = 2F;
        private Image _image;
        private Tween _tween = null;

        private void StartFadeIn()
        {
            if(_image == null) _image = transform.GetComponent<Image>();
            _tween?.Kill();
            _tween =_image.DOFade( 1F, _durationToOne).OnComplete(StartFadeOut);;
        }
    
        private void StartFadeOut()
        {
            _tween = _image.DOFade( 0F, _durationToZero).OnComplete(() => { gameObject.SetActive(false);});
        }
    
        private void OnEnable()
        {
            StartFadeIn();
        }
    }
}
