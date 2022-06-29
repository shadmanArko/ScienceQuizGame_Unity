using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenHandler : MonoBehaviour
{
    public Transform CenterLogo;
    public CanvasGroup TopLogoCanvas;
    public CanvasGroup BottomLogoCanvas;
    public string SceneToLoad;
    
    [SerializeField] private float _targetCenterLogoScale;
    [SerializeField] private float _tweenTime;
    [SerializeField] private float _targetCanvasFade;
    
    private AsyncOperation asyncOperation = new AsyncOperation();
    
    private void Start()
    {
        Initialize();
    }
    
    private void Initialize()
    {
        StartCoroutine(WaitForSplashAnim());
        
        Sequence splashAnim = DOTween.Sequence();
        
        splashAnim.Append(CenterLogo.DOScale(_targetCenterLogoScale, _tweenTime));
        
        splashAnim.Append(TopLogoCanvas.DOFade(_targetCanvasFade, _tweenTime));
        
        splashAnim.Append(BottomLogoCanvas.DOFade(_targetCanvasFade, _tweenTime));
        splashAnim.Append(CenterLogo.DOScale(0f, 0.5f));
        splashAnim.Join(TopLogoCanvas.DOFade(0f, 0.5f));
        splashAnim.Join(BottomLogoCanvas.DOFade(0f, 0.5f).OnComplete(() =>
        {
            asyncOperation.allowSceneActivation = true;
        }));
    }
    
    private IEnumerator WaitForSplashAnim()
    {
        asyncOperation = SceneManager.LoadSceneAsync(SceneToLoad);
        
        asyncOperation.allowSceneActivation = false;

        yield return null;
    }
}
