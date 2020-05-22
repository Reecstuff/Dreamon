using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DecisionButtons : MonoBehaviour
{
	public int decisionNumber;

	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(() => SetDecisionNumber());
	}

	public void SetDecisionNumber()
	{
		GetComponentInParent<DialogueManager>().selectedOpinion = decisionNumber;
	}
}
