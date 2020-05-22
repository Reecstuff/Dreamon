using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionButtons : MonoBehaviour
{
	public int decisionNumber;

	public void SetDecisionNumber()
	{
		FindObjectOfType<DialogueManager>().selectedOpinion = decisionNumber;
	}
}
