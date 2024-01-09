using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Data/Entities/Projectile")]
public class ProjectileData : ScriptableObject
{
	public PhysicsMaterial2D physicsMaterial;
	public float speed = 30;
}
