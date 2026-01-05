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

    public static List<ModelSwapMaster> swappers = new List<ModelSwapMaster>();

    private void Awake()
    {
        swappers.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //foreach (var b in bodies) b.SetActive(false);
    }

    /// <summary>
    /// Switches between different bodies or heads for the two characters.
    /// </summary>
    /// <param name="tag">Denotes which model to switch to according to the 'tags' list in each object with a ModelSwapMaster component.
    /// Bodies can be swapped independently of the head and vice versa using either "(name)_body_..." or "(name)_head_...".
    /// types will pick it up.</param>
    public static void StaticSwapModels(string tag)
    {
        Debug.Log($"Attempting method StaticSwapModels('{tag}')...");

        List<int> nullIndices = new List<int>();
        for (int i = 0; i < swappers.Count; i++) if (swappers[i] == null) nullIndices.Add(i);
        swappers = RemoveNullIndices(swappers, nullIndices);

        Debug.Log($"Current swapper count: {swappers.Count}");

        foreach (var swapper in swappers) { swapper.SwapBodies(tag); swapper.SwapHeads(tag); } 

        //else { swappers[i].SwapBodies(tag); swappers[i].SwapHeads(tag); }
    }

    [ContextMenu("Test List Indices")]
    public void TestLists()
    {
        List<int> mainList = new List<int>
        { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, };

        string output = string.Empty;
        foreach (int i in mainList) output += ($"{i}, ");
        Debug.Log($"mainList: {output}");

        ////

        List<int> removeIndices = new List<int>
        { 2, 4, 3, 1, };

        output = string.Empty;
        foreach (int i in removeIndices) output += ($"{i}, ");
        Debug.Log($"removeIndices: {output}");

        ////

        removeIndices.Sort();
        removeIndices.Reverse();

        output = string.Empty;
        foreach (int i in removeIndices) output += ($"{i}, ");
        Debug.Log($"removeIndices (sorted and reversed): {output}");

        ////

        foreach (int i in removeIndices) mainList.RemoveAt(i);

        output = string.Empty;
        foreach (int i in mainList) output += ($"{i}, ");
        Debug.Log($"mainList (removed indices): {output}");
    }

    /// <summary>
    /// Removes the specified indices from a list. (ModelSwapMaster)
    /// </summary>
    /// <param name="list">The initial list.</param>
    /// <param name="indices">The indices you'd like to remove from the initial list.</param>
    /// <returns></returns>
    private static List<ModelSwapMaster> RemoveNullIndices(List<ModelSwapMaster> list, List<int> indices)
    {
        indices.Sort();
        indices.Reverse();
        foreach (int i in indices) list.RemoveAt(i);
        return list;
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

        foreach (var body in bodies) body.SetActive(false);
        bodies[requestIndex].SetActive(true);

        Debug.Log($"Successfully swapped BODIES for tag: '{tag}'");
    }

    public void SwapHeads(string tag)
    {
        if (heads.Count == 0)
        {
            Debug.LogError($"No heads found!");
            return;
        }

        if (!headTags.Contains(tag)) return; // Request was prolly meant for a different swapper, or the body

        foreach (var head in heads) head.transform.localEulerAngles = GetRotation(tag);

        Debug.Log($"Successfully swapped HEADS for tag: '{tag}'");
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
