using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileEntity : MonoBehaviour
{
	private ProjectileData _data;

	private BubbleSkinInstance _skinInstance;
	private BubbleData _bubbleSkinData;

	public new CircleCollider2D collider;
	public new Rigidbody2D rigidbody;

	private Vector2 _velocity;
	private bool _killed;

	public void Init(ProjectileData data)
	{
		_data = data;
	}

	public void SetVelocity(Vector3 newDirection, float speed)
	{
		_velocity = ((Vector2)newDirection).normalized * speed;
		rigidbody.velocity = _velocity;
	}

	private void FixedUpdate()
	{
		if (transform.position.y < -11 || transform.position.y > 11)
			Kill();
	}

	private void Kill()
	{
		if (_killed)
			return;

		_killed = true;
		gameObject.SetActive(false);
	}

	public void ApplyBubbleSkin(BubbleData bubbleData)
	{
		if (_skinInstance)
			Destroy(_skinInstance.gameObject);

		_bubbleSkinData = bubbleData;

		_skinInstance = Instantiate(bubbleData.skinPrefab, transform.position, Quaternion.identity);
		_skinInstance.transform.SetParent(transform);
		_skinInstance.Init(bubbleData.color);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (_killed)
			return;

		if (other.gameObject.TryGetComponent<BubbleEntity>(out var bubbleEntity))
		{
			Kill();

			var bubble = BubbleFactory.Create(_bubbleSkinData, transform.position, GameStateData.I.currentLevelEntity.GetBubbleHolderTransform());
			bubble.FindNeighbors();
			bubble.TryToKillNeighbors();
		}
	}
}
