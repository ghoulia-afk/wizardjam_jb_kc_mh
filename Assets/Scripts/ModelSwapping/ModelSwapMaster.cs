using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwapMaster : MonoBehaviour
{
    public List<GameObject> bodies;
    public List<GameObject> heads;
    public List<string> bodyTags;
    public List<string> headTags;

    private GameObject currentBody;

    private static List<ModelSwapMaster> swappers;

    private void Awake()
    {
        swappers.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bodies.Count; i++) bodies[i].gameObject.SetActive(false);
    }

    /// <summary>
    /// NOTE: MUST BE INITIATED AT THE START OF THE SCENE FOR BOTH THE HEAD AND THE BODY!
    /// Switches between different bodies or heads for the two characters.
    /// </summary>
    /// <param name="tag">Denotes which model to switch to according to the 'tags' list in each object with a ModelSwapMaster component.
    /// Bodies can be swapped independently of the head, but you only need to give one arguement to do either since only one of the two swapper
    /// types will pick it up.</param>
    public static void StaticSwapModels(string tag)
    {
        for (int i = 0; i < swappers.Count; i++)
        {
            if (swappers[i] == null) swappers.RemoveAt(i);
            else { swappers[i].SwapBodies(tag); swappers[i].SwapHeads(tag); }
        }
    }

    public void SwapBodies(string tag) // WHEN BODIES ACTIVATE, THEIR HEADS ACTIVATE AS WELL!
    {
        if (bodies.Count == 0 | bodyTags.Count == 0)
        {
            Debug.LogError($"No bodies found, or no tags found, or both!");
            return;
        }

        if (!bodyTags.Contains(tag)) return; // Request was prolly meant for a different swapper, or the head

        int requestIndex = bodyTags.IndexOf(tag);

        if (bodies.Count <= requestIndex)
        {
            Debug.LogError($"Index {requestIndex} doesn't exist in bodies!");
            return;
        }

        if (currentBody != null) currentBody.gameObject.SetActive(false);
        currentBody = bodies[requestIndex];
        currentBody.gameObject.SetActive(true);
    }

    public void SwapHeads(string tag)
    {
        if (heads.Count == 0)
        {
            Debug.LogError($"No heads found!");
            return;
        }

        if (!headTags.Contains(tag)) return; // Request was prolly meant for a different swapper, or the body

        foreach (GameObject head in heads) head.transform.localEulerAngles = GetRotation(tag);
    }

    private Vector3 GetRotation(string tag)
    {
        if (tag.Contains("forward")) return Vector3.zero;
        if (tag.Contains("left")) return new Vector3(0, 0, -45);
        if (tag.Contains("right")) return new Vector3(0, 0, 45);
        if (tag.Contains("up")) return new Vector3(45, 0, 0);
        return Vector3.zero;
    }
}
