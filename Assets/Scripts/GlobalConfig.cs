using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalConfig", menuName = "Data/Global Config")]
public class GlobalConfig : SingletonScriptableObject<GlobalConfig>
{
	public Data data;
	public Layers layers;
	public LevelData[] levels;

	[System.Serializable]
	public struct Data
	{
		public PlayerData player;
		public EnvironmentSkin defaultEnvironmentSkin;
		public ProjectileData defaultProjectile;
	}

	[System.Serializable]
	public struct Layers
	{
		public LayerMask bubbles;
		public LayerMask projectiles;
	}
}
