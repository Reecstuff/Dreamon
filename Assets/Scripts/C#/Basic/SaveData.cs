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
    public bool[] resultHistory;
    public bool[] interactHistory;
    public string animationStateName;
    public int footstepIndex;

    public SaveData()
    {
        position = new float[3];
        resultHistory = new bool[1];
        interactHistory = new bool[1];
        animationStateName = string.Empty;
    }

    public void ChangePosition(Vector3 pos)
    {
        position = new float[3] { pos.x, pos.y, pos.z };
    }

    public void AddResult(int index, bool value)
    {
        resultHistory = ChangeArray(ref index, ref value, resultHistory);
    }

    public void AddInteract(int index)
    {
        bool value = true;
        interactHistory = ChangeArray(ref index, ref value, interactHistory);
    }

    public bool? GetResult(int index)
    {
        if(index < resultHistory.Length)
        {
            return resultHistory[index];
        }

        return null;
    }

    public bool GetInteraction(int index)
    {
        if (index < interactHistory.Length)
        {
            return interactHistory[index];
        }

        return false;
    }


    bool[] ChangeArray(ref int index, ref bool value, bool[] array)
    {
        if (index < array.Length)
        {
            array[index] = value;
            return array;
        }
        else
        {
            bool[] temp = new bool[index + 1];
            array.CopyTo(temp, 0);
            temp[index] = value;

            return temp;
        }
    }
}
