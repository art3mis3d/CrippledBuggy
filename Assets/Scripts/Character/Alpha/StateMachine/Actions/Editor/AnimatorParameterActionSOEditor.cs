using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AlphaAnimatorParameterActionSO)), CanEditMultipleObjects]
public class AlphaAnimatorParameterActionSOEditor : CustomBaseEditor
{
	public override void OnInspectorGUI()
	{
		base.DrawNonEditableScriptReference<AlphaAnimatorParameterActionSO>();

		serializedObject.Update();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("description"));
		EditorGUILayout.Space();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("whenToRun"));
		EditorGUILayout.Space();

		EditorGUILayout.LabelField("Animator Parameter", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("parameterName"), new GUIContent("Name"));

		// Draws the appropriate value depending on the type of parameter this SO is going to change on the Animator
		SerializedProperty animParamValue = serializedObject.FindProperty("parameterType");

		EditorGUILayout.PropertyField(animParamValue, new GUIContent("Type"));

		switch (animParamValue.intValue)
		{
			case (int)AlphaAnimatorParameterActionSO.ParameterType.Bool:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("boolValue"), new GUIContent("Desired value"));
				break;
			case (int)AlphaAnimatorParameterActionSO.ParameterType.Int:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("intValue"), new GUIContent("Desired value"));
				break;
			case (int)AlphaAnimatorParameterActionSO.ParameterType.Float:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("floatValue"), new GUIContent("Desired value"));
				break;

		}

		serializedObject.ApplyModifiedProperties();
	}
}
