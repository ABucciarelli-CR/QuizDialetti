using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LanguageCollection")]
public class LanguageCollection : ScriptableObject 
{
	public List<QuestionCollection> languageQuestionCollection;
}
