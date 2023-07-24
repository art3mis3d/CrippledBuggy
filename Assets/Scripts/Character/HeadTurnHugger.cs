using System;
using RuntimeAnchors;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HeadTurnHugger : MonoBehaviour
{
	[SerializeField] private TransformAnchor cameraTransformAnchor;

	private void OnEnable()
	{
		transform.SetParent(cameraTransformAnchor.Value.transform, true);
	}
}