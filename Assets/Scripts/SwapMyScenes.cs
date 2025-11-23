using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SwapMyScenes : MonoBehaviour
{ //A scene swapper that runs whenever sir_swapsalot is called 
    public DialogueRunner dialogueRunner;

    // [YarnCommand("sir_swapsalot")]

    public void Awake()
    {
        dialogueRunner.AddCommandHandler<string>(
            "sir_swapsalot",     // the name of the command
            Scenecalibur // the method to run
        );
    }
    public void Scenecalibur (string sceneName)
    {
        SceneManager.LoadScene (sceneName);
        Debug.Log("Loaded scene" + (sceneName));
    }
}
