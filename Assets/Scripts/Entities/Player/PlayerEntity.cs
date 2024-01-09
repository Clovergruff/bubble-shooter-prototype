using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
	private PlayerSkinInstance _skinInstance;
	private PlayerEntityControls _controls;

	public Action<PlayerSkinInstance> onSetSkin = skin => {};

	public void Init(PlayerData data)
	{
		ApplySkin(data.skinPrefab);

		_controls = gameObject.AddComponent<PlayerEntityControls>();
		_controls.Init(this);
	}

	public void ApplySkin(PlayerSkinInstance skinPrefab)
	{
		if (_skinInstance)
			Destroy(_skinInstance.gameObject);

		_skinInstance = Instantiate(skinPrefab, transform.position, Quaternion.identity);
		_skinInstance.transform.SetParent(transform);

		onSetSkin.Invoke(_skinInstance);
	}

	public PlayerSkinInstance GetSkin() => _skinInstance;
}
