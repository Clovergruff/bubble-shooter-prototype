using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Data/Level")]
public class LevelData : ScriptableObject
{
	public int rows = 25;
	public int columns = 15;
	public BubbleData[] bubbles;
}
