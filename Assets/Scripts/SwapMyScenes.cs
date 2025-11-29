using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SwapMyScenes : MonoBehaviour
{ //A scene swapper that runs whenever sir_swapsalot is called 
    
    public string defaultScene;
    
    public DialogueRunner dialogueRunner;

    public LevelTransitionFade fade;

    // [YarnCommand("sir_swapsalot")]

public void Start(){
     SceneManager.LoadScene (defaultScene, LoadSceneMode.Additive);
     SceneManager.SetActiveScene(SceneManager.GetSceneByName(defaultScene));
}

    public void Awake()
    {
        dialogueRunner.AddCommandHandler<string>(
            "sir_swapsalot",     // the name of the command
            Scenecalibur // the method to run
        );

        dialogueRunner.AddCommandHandler<string>(
            "sir_tolaspaws",
            Excalamune
        );
    }
    public void Scenecalibur (string sceneName)
    {
        StartCoroutine(LoadSceneTime(sceneName));

       
    }

    public IEnumerator LoadSceneTime(string sceneName){
    fade.Fade();
    yield return new WaitForSeconds(0.2f);
    SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);
    SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    Debug.Log("Loaded scene" + (sceneName));
    }

    public IEnumerator UnloadSceneTime(string sceneName){
    fade.Fade();
    yield return new WaitForSeconds(0.2f);
    SceneManager.UnloadSceneAsync(sceneName); //may need to change to UnloadScene if there are issues - KGC
    Debug.Log("Unloaded scene" + sceneName);
    }


    public void Excalamune(string sceneName)
    {
 StartCoroutine(UnloadSceneTime(sceneName));

        
    }


}
