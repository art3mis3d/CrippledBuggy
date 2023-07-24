using System.Collections;
using Cinemachine;
using UnityEngine;

public class LocationEntrance : MonoBehaviour
{
	[SerializeField] private PathSO _entrancePath;
	[SerializeField] private PathStorageSO _pathStorage; // This is the where the the last path taken has been stored.
	[SerializeField] private CinemachineVirtualCamera entranceShot;

	[Header("Listening on")]
	[SerializeField]
	private VoidEventChannelSO _onSceneReady;

	public PathSO EntrancePath => _entrancePath;

	private void Awake()
	{
		if (_pathStorage.lastPathTaken != _entrancePath) return;
		entranceShot.Priority = 100;
		_onSceneReady.OnEventRaised += PlanTransition;
	}

	private void PlanTransition()
	{
		StartCoroutine(TransitionToGameCamera());
	}

	private IEnumerator TransitionToGameCamera()
	{
		yield return new WaitForSeconds(.1f);

		entranceShot.Priority = -1;
		_onSceneReady.OnEventRaised -= PlanTransition;
	}
}