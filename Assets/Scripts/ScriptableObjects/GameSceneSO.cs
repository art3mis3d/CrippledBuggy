using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// This class is a base class which contains what is common to all game scenes (Location, Menus, Managers)
/// </summary>
public class GameSceneSO : DescriptionBaseSO
{
	public GameSceneType sceneType;
	public AssetReference sceneReference; // Used at runtime to load the scene from the right AssetBundle.

	/// <summary>
	/// Used by the SceneSelector tool to discern what type of scene it needs to load.
	/// </summary>
	public enum GameSceneType
	{
		// Playable Scenes
		Location, // SceneSelector tool will also load PersistentManagers and Gameplay.
		Menu, // SceneSelector tool will also load PersistentManagers and GamePlay.

		// Special scenes
		Initialisation,
		PersistentManagers,
		Gameplay,

		//Work in progress scenes that don't need to be played
		Art,
	}
}