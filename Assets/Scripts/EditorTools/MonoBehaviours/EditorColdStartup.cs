using Unity.Burst;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// Allows a "cold start" in the editor, when pressing Play and not passing from the Initialisation scene.
/// </summary>
[BurstCompile]
public class EditorColdStartup : MonoBehaviour
{
#if UNITY_EDITOR
	[SerializeField] private GameSceneSO thisSceneSO;
	[SerializeField] private GameSceneSO persistentManagersSO;
	[SerializeField] private AssetReference notifyColdStartupChannel;
	[SerializeField] private VoidEventChannelSO onSceneReadyChannel;
	[SerializeField] private PathStorageSO pathStorage;
	[SerializeField] private SaveSystem saveSystem;

	private bool _isColdStart;

	private void Awake()
	{
		if (!SceneManager.GetSceneByName(persistentManagersSO.sceneReference.editorAsset.name).isLoaded)
		{
			_isColdStart = true;

			//Reset the path taken, so the character will spawn in this location's default spawn point
			pathStorage.lastPathTaken = null;
		}

		CreateSaveFileIfNotPresent();
	}

	private void Start()
	{
		if (_isColdStart)
			persistentManagersSO.sceneReference.LoadSceneAsync(LoadSceneMode.Additive).Completed +=
				LoadEventChannel;
		CreateSaveFileIfNotPresent();
	}

	private void CreateSaveFileIfNotPresent()
	{
		if (saveSystem != null && !saveSystem.LoadSaveDataFromDisk()) saveSystem.SetNewGameData();
	}

	private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
	{
		notifyColdStartupChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += OnNotifyChannelLoaded;
	}

	private void OnNotifyChannelLoaded(AsyncOperationHandle<LoadEventChannelSO> obj)
	{
		if (thisSceneSO == null)
		{
			obj.Result.RaiseEvent(thisSceneSO);
			Debug.Log("Scene Ready Event Not Raised");
		}
		else
			//Raise a fake scene ready event, so the player is spawned
		{
			onSceneReadyChannel.RaiseEvent();
			Debug.Log("Scene Ready Event Raised");
		}
		//When this happens, the player won't be able to move between scenes because the SceneLoader has no conception of which scene we are in
	}


#endif
}