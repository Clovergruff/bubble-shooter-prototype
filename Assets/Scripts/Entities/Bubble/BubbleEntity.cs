using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleEntity : MonoBehaviour
{
	public BubbleData data;

	[Space]
	private BubbleSkinInstance _skinInstance;
	public new CircleCollider2D collider;
	public new Rigidbody2D rigidbody;

	[Space]
	public bool isKilled;

	private List<BubbleEntity> _neighbors = new List<BubbleEntity>();
	private List<SpringJoint2D> _joints = new List<SpringJoint2D>();

	private GlobalConfig _globalConfig;


	public void Init(BubbleData data)
	{
		_globalConfig = GlobalConfig.I;

		this.data = data;
		ApplySkin(data.skinPrefab);
	}

	public void ApplySkin(BubbleSkinInstance skinPrefab)
	{
		if (_skinInstance)
			Destroy(_skinInstance.gameObject);

		_skinInstance = Instantiate(skinPrefab, transform.position, Quaternion.identity);
		_skinInstance.transform.SetParent(transform);
		_skinInstance.Init(data.color);
	}

	public BubbleSkinInstance GetSkin() => _skinInstance;

	public void ClearNeighbors()
	{
		_neighbors.Clear();
		ClearJoints();
	}

	private void ClearJoints()
	{
		for (int i = 0; i < _joints.Count; i++)
			Destroy(_joints[i]);
		_joints.Clear();
	}

	public void FindNeighbors()
	{
		ClearNeighbors();

		var nearbyColliders = Physics2D.OverlapCircleAll(transform.position, 1.4f, _globalConfig.layers.bubbles);
		foreach (var coll in nearbyColliders)
		{
			if (coll.gameObject.TryGetComponent<BubbleEntity>(out var otherBubble) && otherBubble != this)
			{
				_neighbors.Add(otherBubble);

				if (!rigidbody.isKinematic)
				{
					var newJoint = gameObject.AddComponent<SpringJoint2D>();
					newJoint.connectedBody = otherBubble.rigidbody;
					newJoint.enableCollision = true;
					newJoint.distance = 1.1f;
					newJoint.frequency = 10;

					_joints.Add(newJoint);
				}
			}
		}

	}

	public void TryToKillNeighbors()
	{
		if (isKilled)
			return;

		// foreach (var neighbor in _neighbors)
		// {
		// 	if (neighbor.isKilled || neighbor.data != data)
		// 		continue;

		// 	TryToKillNeighbors();
		// }

		List<BubbleEntity> matchingNeighbors = new List<BubbleEntity>();
		matchingNeighbors.Add(this);

		foreach (var neighbor in _neighbors)
		{
			if (neighbor.isKilled || neighbor.data != data || matchingNeighbors.Contains(neighbor))
				continue;

			matchingNeighbors.Add(neighbor);
			foreach (var neighborNeighbor in neighbor._neighbors)
			{
				if (neighborNeighbor != isKilled && neighborNeighbor.data == data && !matchingNeighbors.Contains(neighborNeighbor))
				{
					matchingNeighbors.Add(neighborNeighbor);
				}
			}
		}

		if (matchingNeighbors.Count > 2)
		{
			Kill();
			foreach (var similarNeighbor in matchingNeighbors)
			{
				similarNeighbor.TryToKillNeighbors();
				similarNeighbor.Kill();
			}
		}
	}

	public void Kill()
	{
		if (isKilled)
			return;

		isKilled = true;

		StartCoroutine(KillCoroutine());
		IEnumerator KillCoroutine()
		{
			yield return new WaitForSeconds(0.1f);
			gameObject.SetActive(false);
			ClearJoints();

			foreach (var neighbor in _neighbors)
			{
				neighbor.CleanupJoints();
			}
		}
	}

	private void CleanupJoints()
	{
		for (int i = 0; i < _joints.Count; i++)
		{
			SpringJoint2D joint = _joints[i];
			if (!joint.attachedRigidbody ^ !joint.attachedRigidbody.gameObject.activeInHierarchy)
			{
				Destroy(_joints[i]);
			}
		}
	}
}
