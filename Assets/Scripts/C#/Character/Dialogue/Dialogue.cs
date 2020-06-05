using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
	[System.Serializable]
	public class Option
	{
		public string name;

		public bool endSentence;

		public int nextDialogue;

		public bool nextMinigame;

		[TextArea(3, 10)]
		public string[] sentences;

		public string[] decisions;

		public int[] nextDecisions;
	}

	public Option[] option;
}
