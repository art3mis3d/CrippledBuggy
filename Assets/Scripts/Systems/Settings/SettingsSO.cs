using UnityEngine;
using UnityEngine.Localization;

namespace Systems.Settings
{
	[CreateAssetMenu(fileName = "Settings", menuName = "Settings/Create new settings SO")]

	public class SettingsSO : ScriptableObject
	{
		public float MasterVolume { get; private set; }
		public float MusicVolume { get; private set; }
		public float SfxVolume { get; private set; }
		public int ResolutionsIndex { get; private set; }
		public int AntiAliasingIndex { get; private set; }
		public float ShadowDistance { get; private set; }
		public bool IsFullscreen { get; private set; }
		public Locale CurrentLocale { get; private set; }
	
		public void SaveAudioSettings(float newMusicVolume, float newSfxVolume, float newMasterVolume)
		{
			MasterVolume = newMasterVolume;
			MusicVolume = newMusicVolume;
			SfxVolume = newSfxVolume;
		}
		public void SaveGraphicsSettings(int newResolutionsIndex, int newAntiAliasingIndex, float newShadowDistance, bool fullscreenState)
		{
			ResolutionsIndex = newResolutionsIndex;
			AntiAliasingIndex = newAntiAliasingIndex;
			ShadowDistance = newShadowDistance;
			IsFullscreen = fullscreenState;
		}
		public void SaveLanguageSettings(Locale local)
		{
			CurrentLocale = local;
		}
		public void LoadSavedSettings(Save savedFile)
		{
			MasterVolume = savedFile._masterVolume;
			MusicVolume = savedFile._musicVolume;
			SfxVolume = savedFile._sfxVolume;
			ResolutionsIndex = savedFile._resolutionsIndex;
			AntiAliasingIndex = savedFile._antiAliasingIndex;
			ShadowDistance = savedFile._shadowDistance;
			IsFullscreen = savedFile._isFullscreen;
			CurrentLocale = savedFile._currentLocale;
		}
	}
}