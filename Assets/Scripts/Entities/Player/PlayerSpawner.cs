using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	private void Awake()
	{
		var config = GlobalConfig.I;
		PlayerFactory.Create(config.data.player, new Vector3(0, -8, 0));
	}
}
