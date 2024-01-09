using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bubble", menuName = "Data/Entities/Bubble")]
public class BubbleData : ScriptableObject
{
	public PhysicsMaterial2D physicsMaterial;
	public BubbleSkinInstance skinPrefab;
	public Color color = Color.white;
}
