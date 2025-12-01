using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

//Made in collaboration with Alexis Morin
public class CameraMover : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    //Script called from YarnSpinner to move camera to location when certain thing/trigger happens - KGC

    // [YarnCommand("wheeCam")] //wheeCam is for moving the camera - KGC

    public void Awake()
    {
        dialogueRunner.AddCommandHandler<string>(
            "camMoveWhee",     // the name of the command
            MoveCamera // the method to run
        );
    }
    public void MoveCamera(string locationName)
    {
        GameObject perspCam = GameObject.Find (locationName);

        transform.position = perspCam.transform.position;
        transform.rotation = perspCam.transform.rotation;
    }


}
