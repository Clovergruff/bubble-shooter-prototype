using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "Data/Runtime/GameState")]
public class GameStateData : SingletonScriptableObject<GameStateData>
{
	public LevelEntity currentLevelEntity;
	public LevelData currentLevelData;
}
