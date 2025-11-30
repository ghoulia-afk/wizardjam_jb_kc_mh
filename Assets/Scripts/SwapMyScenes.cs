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
     SceneManager.LoadScene (defaultScene);
        StartCoroutine(SetAsActiveScene(defaultScene));
}

    public IEnumerator SetAsActiveScene(string sceneName) //waits a half second and loads scene
    {
        yield return new WaitForSeconds(.25f);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName)); //tells unity this scene is what we use for rendering
    }


    public void Awake()
    {
        dialogueRunner.AddCommandHandler<string>(
            "sir_swapsalot",     // the name of the command
            Scenecalibur // the method to run
        );
    }
    public void Scenecalibur (string sceneName)
    {
        StartCoroutine(LoadSceneTime(sceneName));

       
    }

    public IEnumerator LoadSceneTime(string sceneName){
        fade.FadeOutScene();
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene (sceneName);
        Debug.Log("Loaded scene" + (sceneName));
        fade.FadeInScene();

    }


    


}
