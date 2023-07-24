using System;
using RuntimeAnchors;
using UnityEngine;

namespace Gameplay
{
	public class CameraHugger : MonoBehaviour
	{
		[SerializeField] private TransformAnchor playerCameraTransformAnchor;
		
		private void OnEnable()
		{
			playerCameraTransformAnchor.Provide(transform);
		}

		private void OnDestroy()
		{
			playerCameraTransformAnchor.Unset();
		}
	}
}