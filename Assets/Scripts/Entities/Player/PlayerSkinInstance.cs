using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSkinInstance : MonoBehaviour
{
	[SerializeField] private Transform _turretTransform;
	[SerializeField] private Transform _projectileSpawnPoint;
	[SerializeField] private MeshRenderer _nextColorRenderer;

	private MaterialPropertyBlock _materialPropertyBlock;

	private void Awake()
	{
		_materialPropertyBlock = new MaterialPropertyBlock();
	}

	public void AimAtTarget(Vector3 target)
	{
		float rot = Vector2.SignedAngle(Vector2.up, (target - transform.position).normalized);
		_turretTransform.rotation = Quaternion.Euler(0, 0, rot);
	}

	public void SetNextColor(Color color)
	{
		_materialPropertyBlock.SetColor(ShaderHash.COLOR, color);
		_nextColorRenderer.SetPropertyBlock(_materialPropertyBlock);
	}

	public Vector3 GetProjectileSpawnPoint() => _projectileSpawnPoint.position;
}
