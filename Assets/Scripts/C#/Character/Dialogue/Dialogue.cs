using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
	[System.Serializable]
	public class Opinion
	{
		public string name;

		public bool endSentence;

		[TextArea(3, 10)]
		public string[] sentences;

		public string[] decisions;

		public int[] nextDecisions;
	}

	public Opinion[] opinion;
}
