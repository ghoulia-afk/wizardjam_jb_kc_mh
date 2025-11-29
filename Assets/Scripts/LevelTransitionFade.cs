using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransitionFade : MonoBehaviour
{

public CanvasGroup canvasGroup;

public AnimationCurve animationCurve;

public void Fade()
{
StartCoroutine(FadeAnim());

    
}

public IEnumerator FadeAnim(){
    float elapsed = 0f;
      float  duration = 1f;
      while(elapsed < duration){
        elapsed += Time.deltaTime;
        float progress = elapsed / duration;

        canvasGroup.alpha = progress;
        yield return null;
      }
}


}
