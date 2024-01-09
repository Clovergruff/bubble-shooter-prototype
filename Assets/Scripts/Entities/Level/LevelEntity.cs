using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEntity : MonoBehaviour
{
	private LevelData _data;

	private Transform _bubbleHolder;
	public List<BubbleEntity> bubbleEntities = new List<BubbleEntity>();

	private Coroutine _moveDownCoroutine;

	public void Init(LevelData data)
	{
		_data = data;
	}

	public void AddBubbleEntity(BubbleEntity bubble)
	{
		if (!bubbleEntities.Contains(bubble))
			bubbleEntities.Add(bubble);
	}

	public void RemoveBubbleEntity(BubbleEntity bubble)
	{
		bubbleEntities.Remove(bubble);
	}

	public Transform CreateBubbleHolder()
	{
		_bubbleHolder = new GameObject("Bubbles").transform;
		_bubbleHolder.SetParent(transform);

		return _bubbleHolder;
	}

	public void SetBubbleHeight(int height)
	{
		_bubbleHolder.position = new Vector3(0, height, 0);
	}

	public Transform GetBubbleHolderTransform() => _bubbleHolder;

	public void AdvanceLevel()
	{
		if (_moveDownCoroutine != null)
		{
			StopCoroutine(_moveDownCoroutine);
			_moveDownCoroutine = null;
		}

		_moveDownCoroutine = StartCoroutine(MoveDownCoroutine());
	}

	IEnumerator MoveDownCoroutine()
	{
		float t = 0;
		Vector3 startPos = _bubbleHolder.position;
		Vector3 endPos = startPos + new Vector3(0, -1, 0);

		while (t < 1)
		{
			t += Time.deltaTime * 2;

			_bubbleHolder.position = Vector3.Lerp(startPos, endPos, t);

			yield return null;
		}
	}
}
