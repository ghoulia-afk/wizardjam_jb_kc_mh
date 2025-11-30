using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransitionFade : MonoBehaviour
{

    public CanvasGroup canvasGroup;

    public AnimationCurve animationCurve;

    public void FadeOutScene()
    {
        canvasGroup.DOFade(1, 0.3f).SetUpdate(true);

    
    }

    public void FadeInScene()
    {
        canvasGroup.DOFade(0, 0.3f).SetUpdate(true);

    }

    public void FadeToHalf()
    {
        canvasGroup.DOFade(0.5f, 4f).SetUpdate(true);
    }



}
