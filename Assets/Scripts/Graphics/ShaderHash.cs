using UnityEngine;

public static class ShaderHash
{
	public static int COLOR = Shader.PropertyToID("_BaseColor");
	public static int EMISSION = Shader.PropertyToID("_EmissionColor");
	public static int SMOOTHNESS = Shader.PropertyToID("_Smoothness");
	public static int METALLIC = Shader.PropertyToID("_Metallic");
}
