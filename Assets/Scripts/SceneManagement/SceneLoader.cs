using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	[SerializeField] private GameSceneSO gameplayScene;
	[SerializeField] private InputReader inputReader;

	[Header("Listening to")] [SerializeField]
	private LoadEventChannelSO _loadLocation;

	[SerializeField] private LoadEventChannelSO _loadMenu;
	[SerializeField] private LoadEventChannelSO _coldStartupLocation;

	[Header("BroadCasting on")] [SerializeField]
	private BoolEventChannelSO _toggledLoadingScene;

	[SerializeField] private VoidEventChannelSO _onSceneReady;
	[SerializeField] private FadeChannelSO _fadeRequestChannel;

	private AsyncOperationHandle<SceneInstance> _loadingOperationHandle;
	private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOperationHandle;

	// Parameters coming from scene loading requests
	private GameSceneSO _sceneToLoad;
	private GameSceneSO _currentlyLoadedScene;
	private bool _showLoadingScreen;

	private SceneInstance _gameplayManagerSceneInstance = new();
	private const float _fadeDuration = .5f;
	private bool _isLoading; // To prevent a new loading request while already loading a new scene.

	private void OnEnable()
	{
		_loadLocation.OnLoadingRequested += LoadLocation;
		_loadMenu.OnLoadingRequested += LoadMenu;
#if UNITY_EDITOR
		_coldStartupLocation.OnLoadingRequested += LocationColdStartup;
#endif
	}

	private void OnDisable()
	{
		_loadLocation.OnLoadingRequested -= LoadLocation;
		_loadMenu.OnLoadingRequested -= LoadMenu;
#if true
		_coldStartupLocation.OnLoadingRequested -= LocationColdStartup;
#endif
	}

#if UNITY_EDITOR
	/// <summary>
	/// This special loading function is only used in the editor, when the developer presses Play in a Location scene, without passing Initialization.
	/// </summary>
	private void LocationColdStartup(GameSceneSO currentlyOpenedLocation, bool showLoadingScreen, bool fadeScreeb)
	{
		// Gameplay managers are loaded synchronously
		_gameplayManagerLoadingOperationHandle =
			gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
		_gameplayManagerLoadingOperationHandle.WaitForCompletion();
		_gameplayManagerSceneInstance = _gameplayManagerLoadingOperationHandle.Result;
	}
#endif

	/// <summary>
	/// This function loads the location scenes passed as array parameter
	/// </summary>
	/// <param name="locationToLoad">The location scene to load..</param>
	/// <param name="showLoadingScreen"> should the loading scene should be shown</param>
	/// <param name="fadeScreen">Should the screen be faded as a transition.</param>
	private void LoadLocation(GameSceneSO locationToLoad, bool showLoadingScreen = false, bool fadeScreen = false)
	{
		// Prevent a double loading, for the situation where the player falls in two Exit colliders in the frame
		if (_isLoading)
			return;

		_sceneToLoad = locationToLoad;
		_showLoadingScreen = showLoadingScreen;
		_isLoading = true;

		// In case we are coming from the main menu, we need to load the Gameplay manager scene first
		if (_gameplayManagerSceneInstance.Scene == null || !_gameplayManagerSceneInstance.Scene.isLoaded)
		{
			_gameplayManagerLoadingOperationHandle =
				gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
			_gameplayManagerLoadingOperationHandle.Completed += OnGameplayManagersLoaded;
		}
		else
		{
			StartCoroutine(UnloadPreviousScene());
		}
	}

	private void OnGameplayManagersLoaded(AsyncOperationHandle<SceneInstance> obj)
	{
		_gameplayManagerSceneInstance = _gameplayManagerLoadingOperationHandle.Result;

		StartCoroutine(UnloadPreviousScene());
	}

	/// <summary>
	/// Prepares to load the main menu scene, first removing the gameplay scene in case the game is coming back from the menus.
	/// </summary>
	/// <param name="menuToLoad"></param>
	/// <param name="showLoadingScreen"></param>
	/// <param name="fadeScreen"></param>
	private void LoadMenu(GameSceneSO menuToLoad, bool showLoadingScreen, bool fadeScreen)
	{
		// Prevent a double loading, for the situation where the player falls in two Exit colliders in the frame
		if (_isLoading)
			return;

		_sceneToLoad = menuToLoad;
		_showLoadingScreen = showLoadingScreen;
		_isLoading = true;

		// In case we are coming from a Location back to the main menu, we need to get rid of the persistent Gameplay manager scene
		if (_gameplayManagerSceneInstance.Scene is { isLoaded: true })
			Addressables.UnloadSceneAsync(_gameplayManagerLoadingOperationHandle, true);

		StartCoroutine(UnloadPreviousScene());
	}

	/// <summary>
	/// In both the location and menu loading, this function takes care of removing previously loaded scenes.
	/// </summary>
	/// <returns></returns>
	private IEnumerator UnloadPreviousScene()
	{
		inputReader.DisableAllInput();
		_fadeRequestChannel.FadeOut(_fadeDuration);

		yield return new WaitForSeconds(_fadeDuration);

		if (_currentlyLoadedScene != null)
		{
			if (_currentlyLoadedScene.sceneReference.OperationHandle.IsValid())
				// Unload the scene through its AssetReference. i. e. through the Addressable system
				_currentlyLoadedScene.sceneReference.UnLoadScene();
			else
				// Only used when, after a "cold start", the player moves to a new scene
				// Since the AsyncOperationHandle has not been used (the scene was already open in the editor),
				// the scene needs to be unloaded using regular SceneManager instead of as an Addressable
				SceneManager.UnloadSceneAsync(_currentlyLoadedScene.sceneReference.editorAsset.name);
		}

		LoadNewScene();
	}

	/// <summary>
	/// Kicks off the asynchronus loading of a scene ,either menu or location.
	/// </summary>
	private void LoadNewScene()
	{
		if (_showLoadingScreen) _toggledLoadingScene.RaiseEvent(true);

		_loadingOperationHandle = _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true, 0);
		_loadingOperationHandle.Completed += OnNewSceneLoaded;
	}

	private void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
	{
		// save loaded scenes (to be unloaded at next load request)
		_currentlyLoadedScene = _sceneToLoad;

		var s = obj.Result.Scene;
		SceneManager.SetActiveScene(s);
		LightProbes.Tetrahedralize();

		_isLoading = false;

		if (_showLoadingScreen)
			_toggledLoadingScene.RaiseEvent(false);

		_fadeRequestChannel.FadeIn(_fadeDuration);

		StartGameplay();
	}

	private void StartGameplay()
	{
		_onSceneReady.RaiseEvent(); // Spawn system will spawn the player in a gameplay scene
	}

	private void ExitGame()
	{
		Application.Quit();
		Debug.Log("Exit!");
	}
}