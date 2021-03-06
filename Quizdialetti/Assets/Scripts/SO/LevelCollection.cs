﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels")]
public class LevelCollection : ScriptableObject
{
	public bool archievementUnlocked = false;
	public List<QuestionCollection> questionCollection;
	public List<bool> unlocked;
	public Sprite questionBG;
	public Sprite buttonSprite;
}
