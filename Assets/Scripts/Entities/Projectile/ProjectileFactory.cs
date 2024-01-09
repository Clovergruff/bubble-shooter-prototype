using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectileFactory
{
	public static ProjectileEntity Create(ProjectileData data, BubbleData bubbleData, Vector3 position, Vector3 target, float speed)
	{
		var projectileGO = new GameObject("Projectile");
		projectileGO.transform.position = position;
		projectileGO.layer = LayerMask.NameToLayer("Projectile");

		var entity = projectileGO.AddComponent<ProjectileEntity>();

		entity.collider = entity.gameObject.AddComponent<CircleCollider2D>();
		entity.rigidbody = entity.gameObject.AddComponent<Rigidbody2D>();
		entity.rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
		entity.rigidbody.sharedMaterial = data.physicsMaterial;
		entity.rigidbody.isKinematic = false;
		entity.rigidbody.gravityScale = 0;

		entity.Init(data);
		entity.ApplyBubbleSkin(bubbleData);
		entity.SetVelocity(target - position, speed);
		
		return entity;
	}
}