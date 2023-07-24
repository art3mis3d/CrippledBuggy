using System;
using UnityEngine;

public class EditorTeleporter : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
	[SerializeField] private GameObject cheatMenu;
	[SerializeField] private PathStorageSO path;

	[Header("Broadcast on")]
	[SerializeField] private LoadEventChannelSO _loadLocationRequest;

	private LocationSO _lastLocationTeleportedTo = default;

	private void OnEnable() => inputReader.CheatMenuEvent += ToggleCheatMenu;

	private void OnDisable() => inputReader.CheatMenuEvent -= ToggleCheatMenu;

	private void Start()
	{
		cheatMenu.SetActive(false);
	}

	private void ToggleCheatMenu()
	{
		cheatMenu.SetActive(!cheatMenu.activeInHierarchy);
	}

	public void Teleport(LocationSO where, PathSO whichEntrance)
	{
		//Avoid reloading the same Location, which would result in an error
		if(where == _lastLocationTeleportedTo)
			return;

		path.lastPathTaken = whichEntrance;
		_lastLocationTeleportedTo = where;
		_loadLocationRequest.RaiseEvent(where);
	}
}
