using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMyData : MonoBehaviour
{

    //Intended to act as a way to stop stored values from being destroyed between loading scenes. - KGC
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    
}
