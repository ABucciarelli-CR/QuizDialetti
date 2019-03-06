using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Questions")]
public class QuestionCollection : ScriptableObject 
{
	public List<SingleQuestion> questions;
	
}
