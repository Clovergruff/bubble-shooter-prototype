using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	private LevelEntity _levelEntity;

	private void Awake()
	{
		var config = GlobalConfig.I;
		CreateLevel(config.levels[0]);
	}

	private void CreateLevel(LevelData data)
	{
		if (_levelEntity)
			Destroy(_levelEntity.gameObject);

		_levelEntity = LevelFactory.Create(data);
	}
}
