using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMyCameras : MonoBehaviour
{

#if UNITY_EDITOR //only compiles if in editor - KGC
    private void OnDrawGizmos()
    {
        //shows the forward direction of given camera pos (BE SURE TO ATTACH THIS SCRIPT TO EACH DESIRED CAM LOCATION :) )
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 1);
       /* Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 1); */
    }
#endif

}
