using System;
using Character.Alpha;
using RuntimeAnchors;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
	[Header("Asset References")] [SerializeField]
	private InputReader inputReader;

	[SerializeField] private Protagonist playerPrefab;
	[SerializeField] private TransformAnchor playerTransformAnchor;
	[SerializeField] private TransformEventChannelSO playerInstantiatedChannel;
	[SerializeField] private PathStorageSO pathTaken;

	[Header("Scene Ready Event")] [SerializeField]
	private VoidEventChannelSO onSceneReady;

	private LocationEntrance[] _spawnLocations;
	private Transform _defaultSpawnPoint;

	private void Awake()
	{
		_spawnLocations = FindObjectsOfType<LocationEntrance>();
		_defaultSpawnPoint = transform.GetChild(0);
	}

	private void OnEnable()
	{
		onSceneReady.OnEventRaised += SpawnPlayer;
	}

	private void OnDisable()
	{
		onSceneReady.OnEventRaised -= SpawnPlayer;
		playerTransformAnchor.Unset();
	}

	private Transform GetSpawnLocation()
	{
		if (pathTaken == null)
			return _defaultSpawnPoint;

		// Look for the element in the available LocationEntries that matches the last PathSO Taken.
		int entranceIndex = Array.FindIndex(_spawnLocations, element => element.EntrancePath == pathTaken.lastPathTaken);
		
		if(entranceIndex == -1)
		{
			Debug.LogWarning("The player tried to spawn in an LocationEntry that doesn't exist, returning the default one.");
			return _defaultSpawnPoint;
		}
		else
		{
			return _spawnLocations[entranceIndex].transform;
		}
	}

	private void SpawnPlayer()
	{
		Debug.Log("Player Spawned");
		Transform spawnLocation = GetSpawnLocation();
		Protagonist playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
		playerInstantiatedChannel.RaiseEvent(playerInstance.transform);
		Debug.Log($"Provided Player Transform {playerInstance.transform}");
		playerTransformAnchor.Provide(playerInstance.transform); // the camera system will pick this up to frame the player

		// TODO: Probably move this to GameManager once it's up and running
		inputReader.EnableGameplayInput();
	}
}