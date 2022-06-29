using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutScript : MonoBehaviour
{
    private Image _transitionImage;
    [SerializeField] private float _tweenTime;

    private void Start()
    {
        _transitionImage = GetComponent<Image>();
        _transitionImage.DOFade(0f, _tweenTime);
    }
}
