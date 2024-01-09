using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BubbleFactory
{
	public static BubbleEntity Create(BubbleData data, Vector3 position, Transform parent)
	{
		var bubbleGO = new GameObject(data.name);
		bubbleGO.transform.position = position;
		bubbleGO.transform.SetParent(parent);
		bubbleGO.layer = LayerMask.NameToLayer("Bubble");

		var entity = bubbleGO.AddComponent<BubbleEntity>();

		entity.collider = entity.gameObject.gameObject.AddComponent<CircleCollider2D>();
		entity.rigidbody = entity.gameObject.gameObject.AddComponent<Rigidbody2D>();
		entity.rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
		entity.rigidbody.sharedMaterial = data.physicsMaterial;

		entity.Init(data);
		
		return entity;
	}
}