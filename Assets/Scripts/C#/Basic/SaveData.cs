using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SaveData
{
    public float[] position;
    public int currentScene;
    public Dictionary<string, bool> resultHistory;
    public Dictionary<string, bool> interactHistory;
    public string animationStateName;
    public int footstepIndex;

    public SaveData()
    {
        position = new float[3];
        resultHistory = new Dictionary<string, bool>();
        interactHistory = new Dictionary<string, bool>();
        animationStateName = string.Empty;
    }

    public void ChangePosition(Vector3 pos)
    {
        position = new float[3] { pos.x, pos.y, pos.z };
    }

    public void AddResult(string index, bool value)
    {
        if (resultHistory.ContainsKey(index))
            resultHistory[index] = value;
        else
        {
            resultHistory.Add(index, value);
        }
    }

    public void AddInteract(string index)
    {
        interactHistory.Add(index, true);
    }

    public bool? GetResult(string index)
    {
        bool value;


        if (resultHistory.TryGetValue(index, out value))
        {

            return value;
        }
        else
            return null;
    }

    public bool GetInteraction(string index)
    {
        return interactHistory.ContainsKey(index);
    }
}
