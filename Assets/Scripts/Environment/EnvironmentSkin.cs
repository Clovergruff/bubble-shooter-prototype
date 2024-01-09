using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Environment", menuName = "Data/Environment Skin")]
public class EnvironmentSkin : ScriptableObject
{
	[Header("Background")]
	public Material backgroundMaterial;

	[Header("Light")]
	public Color ambientLightColor = Color.white;
	public float ambientLightIntensity = 1;

	[Space]
	public Color sunlightColor = Color.white;
	public float sunlightIntensity = 1;
}
