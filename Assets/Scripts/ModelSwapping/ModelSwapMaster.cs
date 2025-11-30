using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwapMaster : MonoBehaviour
{
    public List<GameObject> bodies;
    public List<GameObject> heads;
    public List<string> tags;

    private GameObject currentBody;

    private static List<ModelSwapMaster> swappers;

    // Start is called before the first frame update
    void Start()
    {
        swappers.Add(this);
    }

    public static void StaticSwapModels(string tag)
    {
        foreach (ModelSwapMaster swapper in swappers)
        {
            if (swapper == null) swappers.Remove(swapper);
            else swapper.SwapModels(tag);
        }
    }

    public void SwapModels(string tag)
    {
        if (bodies.Count == 0 | tags.Count == 0)
        {
            Debug.LogError($"No models found, or no tags found, or both!");
            return;
        }

        if (!tags.Contains(tag)) return; // Request was prolly meant for a different swapper

        int requestIndex = tags.IndexOf(tag);

        if (bodies.Count <= requestIndex)
        {
            Debug.LogError($"Index {requestIndex} doesn't exist in models!");
            return;
        }

        if (currentBody != null) currentBody.transform.localPosition = Vector3.down * 100;

        currentBody = bodies[requestIndex];
        currentBody.SetActive(true);
    }
}
