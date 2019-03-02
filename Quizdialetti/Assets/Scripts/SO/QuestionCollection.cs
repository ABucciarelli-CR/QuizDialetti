using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Language")]
public class QuestionCollection : ScriptableObject 
{
	public List<SingleQuestion> questions;
	
	public List<bool> unlocked;
}
