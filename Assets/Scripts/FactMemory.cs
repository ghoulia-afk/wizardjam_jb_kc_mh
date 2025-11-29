using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class FactMemory : Yarn.Unity.VariableStorageBehaviour
{

    public FetchingList factPool;
    public DialogueRunner dialogueRunner;

    [Space(10)]

    public List<Fact> previewList = new List<Fact>();



    public static List<Fact> factList = new List<Fact>();

    //Code via Alexis Morin
    //integers that keep track of what ending the player is "working towards" - KGC
    static int togetherEndingCounter = 0;
    static int aloneEndingCounter = 0;

    public static void AddTogetherEndingCounter() //function to add to counter for Together Ending - KGC
    {
        togetherEndingCounter += 1;
    }

    public static void AddAloneEndingCounter() //function to add to counter for Alone Ending - KGC
    { 
        aloneEndingCounter += 1; 
    }

    public static bool TryTogetherEndingCounter(int testCount) //function that Yarn can call to see what ending the player has gotten
    {
        if (togetherEndingCounter >= testCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool TryAloneEndingCounter(int testCount) //function that Yarn can call to see what ending the player has gotten
    {
        if (aloneEndingCounter >= testCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool CheckTogetherEnding() //function that Yarn can call to see what ending the player has gotten
    {
        if (togetherEndingCounter >= aloneEndingCounter)
        {
            return true; //this means the player has gotten the together ending
        }
        else
        {
            return false; //this means the player has gotten the alone ending
        }
    }


    public void Awake()
    {
        dialogueRunner.AddCommandHandler<string>("AddFact", AddFact);
        dialogueRunner.AddFunction<string, bool>("TryFact", TryFact);

        dialogueRunner.AddFunction<bool>("CheckTogetherEnding", CheckTogetherEnding);

        dialogueRunner.AddCommandHandler("AddTogetherEndingCounter", AddTogetherEndingCounter);
        dialogueRunner.AddCommandHandler("AddAloneEndingCounter", AddAloneEndingCounter);

        dialogueRunner.AddFunction<int, bool>("TryTogetherEndingCounter", TryTogetherEndingCounter);
        dialogueRunner.AddFunction<int, bool>("TryAloneEndingCounter", TryAloneEndingCounter);
    }

    public void ClearFacts()
    {
        factList = new List<Fact>();
    }

    public void AddFact(string factName)
    {
        for (int i = 0; i < factPool.facts.Length; i++)
        {
            if (factName == factPool.facts[i].internalName)
            {
                if (!factList.Contains(factPool.facts[i]))
                {
                    factList.Add(factPool.facts[i]);
                }

            }
        }

        UpdatePreviewList();
    }

    public static void AddFactFromAsset(Fact fact)
    {
        if (!factList.Contains(fact))
        {
            factList.Add(fact);
        }
    }

    public static bool TryFact(string factName)
    {
        bool containsFact = false;
        for (int i = 0; i < factList.Count; i++)
        {
            if (factList[i].internalName == factName)
            {
                containsFact = true;
            }
        }
        return containsFact;
    }

    void UpdatePreviewList()
    {
        previewList = new();
        foreach (var item in factList)
        {
            previewList.Add(item);
        }
    }

    // Unused

    public override void SetValue(string variableName, string stringValue) { }
    public override void SetValue(string variableName, float floatValue) { }
    public override void SetValue(string variableName, bool boolValue) { }
    public override void Clear() { }

    public override void SetAllVariables(Dictionary<string, float> floats, Dictionary<string, string> strings, Dictionary<string, bool> bools, bool clear = true)
    {
    }

    public override (Dictionary<string, float>, Dictionary<string, string>, Dictionary<string, bool>) GetAllVariables()
    {
        Dictionary<string, float> floatDict = new();
        Dictionary<string, string> stringDict = new();
        Dictionary<string, bool> boolDict = new();
        return (floatDict, stringDict, boolDict);
    }

    public override bool TryGetValue<T>(string variableName, out T result)
    {
        bool containsFact = false;
        for (int i = 0; i < factList.Count; i++)
        {
            if (factList[i].internalName == variableName)
            {
                containsFact = true;
            }
        }

        // He we return junk
        System.Object output = new object();
        result = (T)output;

        return containsFact;
    }

    public override bool Contains(string variableName)
    {
        bool containsFact = false;
        for (int i = 0; i < factList.Count; i++)
        {
            if (factList[i].internalName == variableName)
            {
                containsFact = true;
            }
        }

        return containsFact;
    }
}
