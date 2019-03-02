using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Single Question & Answers")]
public class SingleQuestion : ScriptableObject
{

	public string question;
	public List<string> answers;
	public int rightAnswer;
}
