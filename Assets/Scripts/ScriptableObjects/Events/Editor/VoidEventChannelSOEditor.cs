using UnityEditor;
using UnityEngine;

namespace ScriptableObjects.Events.Editor
{
	[CustomEditor(typeof(VoidEventChannelSO))]
	public class VoidEventChannelSOEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			if (!Application.isPlaying)
				return;
			
			ScriptableObjectHelper.GenerateButtonsForEvents<VoidEventChannelSO>(target);
			Debug.Log("Generated Buttons for Events in UI");
		}
	}
}