using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwapMaster : MonoBehaviour
{
    public List<GameObject> models;
    public List<string> tags;

    private GameObject current;

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

        }
        /*
        if (models.Count == 0 | tags.Count == 0)
        {
            Debug.LogError($"No models found, or no tags found, or both!");
            return;
        }

        if (!tags.Contains(tag))
        {
            Debug.LogError($"Tried swapping to model with tag of {tag} but failed: tag not found.");
            return;
        }

        int requestIndex = tags.IndexOf(tag);

        if (models.Count <= requestIndex)
        {
            Debug.LogError($"Model at index {requestIndex} doesn't exist.");
            return;
        }

        if (current != null) current.SetActive(false);

        current = models[requestIndex];
        current.SetActive(true);*/
    }

    public void SwapModels(string tag)
    {

    }
}
