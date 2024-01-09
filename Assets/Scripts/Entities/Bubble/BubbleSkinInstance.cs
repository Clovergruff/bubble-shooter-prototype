using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSkinInstance : MonoBehaviour
{
	[SerializeField] private MeshRenderer _renderer;

	private MaterialPropertyBlock _materialPropertyBlock;

	public void Init(Color color)
	{
		_materialPropertyBlock = new MaterialPropertyBlock();
		_materialPropertyBlock.SetColor("_BaseColor", color);
		_renderer.SetPropertyBlock(_materialPropertyBlock);
	}
}
