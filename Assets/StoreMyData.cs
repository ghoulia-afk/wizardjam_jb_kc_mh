using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMyData : MonoBehaviour
{

    // Start is called before the first frame update
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
