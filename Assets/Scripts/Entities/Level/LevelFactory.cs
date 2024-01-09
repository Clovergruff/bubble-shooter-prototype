using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelFactory
{
	public static LevelEntity Create(LevelData data)
	{
		var levelGO = new GameObject(data.name);
		var entity = levelGO.AddComponent<LevelEntity>();
		entity.Init(data);

		var bubbleHolderTransform = entity.CreateBubbleHolder();

		const float vertSpacing = 1.05f;
		const float horSpacing = 1.15f;
		const float halfHorSpacing = horSpacing * 0.5f;
		float worldWidth = data.columns * horSpacing;
		float worldWidthHalf = worldWidth * 0.5f;
		float worldHeight = data.rows * vertSpacing;

		// Spawn all the bubbles
		for (int y = 0; y < data.rows; y++)
		{
			int rowIsOdd = y % 2;
			float xoffset = halfHorSpacing * rowIsOdd;
			for (int x = 0; x < data.columns + (1 - rowIsOdd); x++)
			{
				var bubbleData = data.bubbles[Random.Range(0, data.bubbles.Length)];
				var bubble = BubbleFactory.Create(bubbleData, new Vector3(-worldWidthHalf + xoffset + x * horSpacing, worldHeight - y * vertSpacing, 0), bubbleHolderTransform);

				// Anchor the bubbles on sides and the top
				if (y == 0 || x == 0 || x > data.columns - 2)
					bubble.rigidbody.isKinematic = true;

				entity.AddBubbleEntity(bubble);
			}
		}

		// Setup bubble neighbors
		foreach (var bubbleEntity in entity.bubbleEntities)
			bubbleEntity.FindNeighbors();

		entity.SetBubbleHeight(7);

		var gameState = GameStateData.I;
		gameState.currentLevelData = data;
		gameState.currentLevelEntity = entity;
		
		return entity;
	}
}