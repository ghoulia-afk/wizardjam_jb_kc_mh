using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollMyCredits : MonoBehaviour
//Made in collaboration with Nick Kornek + partially based off of a tutorial by skGameDev
//https://www.youtube.com/watch?v=Eeee4TU69x4
{
    public float scrollSpeed = 5f; //how fast the credits move
    public bool rollCreds = false;

    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        
        //Get RectTransform of UI element
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rollCreds)
        {
            return;
        }

        //Move text upwards over time
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
