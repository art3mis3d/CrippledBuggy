using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Load Event Channel", order = 0)]
public class LoadEventChannelSO : DescriptionBaseSO
{
	public UnityAction<GameSceneSO, bool, bool> OnLoadingRequested;

	public void RaiseEvent(GameSceneSO locationToLoad, bool showLoadingScene = false, bool fadeScreen = false)
	{
		if (OnLoadingRequested != null)
		{
			OnLoadingRequested.Invoke(locationToLoad, showLoadingScene, fadeScreen);
		}
		else
		{
			Debug.LogWarning("A scene loading was requested, but nobody picked it up. " +
			                 "Check why There is no SceneLoader already present, " +
			                 "and make sure it's listening on this Load Event channel.");
		}
	}
}
