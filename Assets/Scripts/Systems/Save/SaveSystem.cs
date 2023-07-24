using System.Collections;
using Systems.Settings;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveSystem", menuName = "Game/Save System Settings")]
public class SaveSystem : ScriptableObject
{
	[SerializeField] private VoidEventChannelSO saveSettingsEvent;
	[SerializeField] private LoadEventChannelSO loadLocation;
	[SerializeField] private SettingsSO currentSettings;

	public string saveFilename = "save.ProjX";
	public string backupSaveFilename = "save.ProjX.bak";
	public Save saveData = new Save();

	void OnEnable()
	{
		saveSettingsEvent.OnEventRaised += SaveSettings;
		loadLocation.OnLoadingRequested += CacheLoadLocations;
	}

	void OnDisable()
	{
		saveSettingsEvent.OnEventRaised -= SaveSettings;
		loadLocation.OnLoadingRequested -= CacheLoadLocations;
	}

	private void CacheLoadLocations(GameSceneSO locationToLoad, bool showLoadingScreen, bool fadeScreen)
	{
		LocationSO locationSO = locationToLoad as LocationSO;
		if (locationSO)
		{
			saveData._locationId = locationSO.Guid;
		}

		SaveDataToDisk();
	}

	public bool LoadSaveDataFromDisk()
	{
		if (FileManager.LoadFromFile(saveFilename, out var json))
		{
			saveData.LoadFromJson(json);
			return true;
		}

		return false;
	}

	public IEnumerator LoadSavedInventory()
	{
		return null;
	}
	public void LoadSavedQuestlineStatus()
	{
	}

	public void SaveDataToDisk()
	{
		saveData._itemStacks.Clear();

		// TODO: Add REAL QuestManager Save System
		if (!FileManager.MoveFile(saveFilename, backupSaveFilename)) return;
		if (FileManager.WriteToFile(saveFilename, saveData.ToJson()))
		{
			Debug.Log("Save successful " + saveFilename);
		}
	}

	public void WriteEmptySaveFile()
	{
		FileManager.WriteToFile(saveFilename, "");

	}
	public void SetNewGameData()
	{
		FileManager.WriteToFile(saveFilename, "");
		// TODO: Add Inventory Initialize method
		// TODO: Add REAL Quest Manager Save Syatem
		// _questManagerSO.ResetQuestlines();

		SaveDataToDisk();

	}
	void SaveSettings()
	{
		saveData.SaveSettings(currentSettings);
	}
}
