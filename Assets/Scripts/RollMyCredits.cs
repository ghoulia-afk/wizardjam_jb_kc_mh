using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollMyCredits : MonoBehaviour

    //credits to skGameDev for this one
{
    public float scrollSpeed = 5f; //how fast the credits move

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
        //Move text upwards over time
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
