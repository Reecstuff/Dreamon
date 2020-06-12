using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Needs to be Switched
/// </summary>
public class Dementum : DialogueTrigger
{
    [SerializeField]
    GameObject[] objectsToDeactivate;

    public override void TheEnd()
    {
        base.TheEnd();
        if(objectsToDeactivate.Length > 0)
        {
            for (int i = 0; i < objectsToDeactivate.Length; i++)
            {
                objectsToDeactivate[i].SetActive(false);
            }
        }
    }
}
