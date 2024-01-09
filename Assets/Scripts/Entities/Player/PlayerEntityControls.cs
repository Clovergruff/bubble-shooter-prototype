using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerEntityControls : MonoBehaviour
{
	private const int SHOTS_UNTIL_LEVEL_ADVANCE = 5;

	private PlayerEntity _playerEntity;
	private Plane _backgroundPlane;
	private Camera _camera;
	private PlayerControls _controls;

	private Vector3 _aimPoint;

	private GlobalConfig _globalConfig;
	private GameStateData _gameStateData;

	private BubbleData nextBubbleData;
	private int _shotsTaken;

	private void Awake()
	{
		_globalConfig = GlobalConfig.I;
		_gameStateData = GameStateData.I;

		_controls = new PlayerControls();
		_controls.Default.Fire.performed += OnPressFire;
	}

	private void OnEnable()
	{
		_controls.Enable();
	}

	private void OnDisable()
	{
		_controls.Disable();
	}

	public void Init(PlayerEntity playerEntity)
	{
		_playerEntity = playerEntity;
		_backgroundPlane = new Plane(Vector3.back, Vector3.zero);

		_camera = Camera.main;
		
		playerEntity.onSetSkin += OnSkinSet;

		SetNextBubbleData();
		RefreshNextBubbleColor();
	}

	private void OnSkinSet(PlayerSkinInstance skin)
	{
		RefreshNextBubbleColor();
	}

	private void Update()
	{
		Ray ray = _camera.ScreenPointToRay(_controls.Default.PointerPosition.ReadValue<Vector2>());

		if (_backgroundPlane.Raycast(ray, out var enter))
		{
			_aimPoint = ray.GetPoint(enter);
			_playerEntity.GetSkin().AimAtTarget(_aimPoint);
		}
	}

	private void SetNextBubbleData()
	{
		var bubbleDatas = _gameStateData.currentLevelData.bubbles;
		nextBubbleData = bubbleDatas[Random.Range(0, bubbleDatas.Length)];
	}

	private void RefreshNextBubbleColor() => _playerEntity.GetSkin().SetNextColor(nextBubbleData.color);

	private void OnPressFire(InputAction.CallbackContext context)
	{
		var projectileData = _globalConfig.data.defaultProjectile;

		ProjectileFactory.Create(
			projectileData,
			nextBubbleData,
			_playerEntity.GetSkin().GetProjectileSpawnPoint(),
			_aimPoint,
			projectileData.speed);

		SetNextBubbleData();
		RefreshNextBubbleColor();

		_shotsTaken++;
		if (_shotsTaken > SHOTS_UNTIL_LEVEL_ADVANCE)
		{
			_shotsTaken = 0;
			_gameStateData.currentLevelEntity.AdvanceLevel();
		}
	}
}
