using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
	[System.Serializable]
	public class Talk
	{
		public Transform cameraTarget;
		public string name;

		[TextArea(3, 10)]
		public string sentence;
	}


	[System.Serializable]
	public class Option
	{
		public bool endSentence;

		public bool nextMinigame;

		public Talk[] talks;
		

		public string[] decisions;

		public int[] nextDecisions;
	}

	public Option[] option;
}
