using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerFactory
{
	public static PlayerEntity Create(PlayerData data, Vector3 position)
	{
		var playerGO = new GameObject("Player");
		playerGO.transform.position = position;

		var entity = playerGO.AddComponent<PlayerEntity>();
		entity.Init(data);
		
		return entity;
	}
}