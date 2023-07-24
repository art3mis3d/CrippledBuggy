using System.Collections;
using Cinemachine;
using RuntimeAnchors;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
	public InputReader inputReader;
	public Camera mainCamera;
	public CinemachineFreeLook freeLookVCam;
	public CinemachineImpulseSource impulseSource;

	// [SerializeField]
	// [Range(.5f, 3f)]
	// private float speedMultiplier = 1f; //TODO: make this modifiable in the game settings
	[SerializeField]
	private TransformAnchor cameraTransformAnchor;
	[SerializeField]
	private TransformAnchor protagonistCameraTransformAnchor;

	[Header("Listening on channels")]
	[Tooltip("The CameraManager listens to this event, fired by protagonist GettingHit state, to shake camera")]
	[SerializeField] private VoidEventChannelSO camShakeEvent;

	private bool _cameraMovementLock;

	private void OnEnable()
	{
		inputReader.CameraEvent += OnCameraMove;

		protagonistCameraTransformAnchor.OnAnchorProvided += SetupProtagonistVirtualCamera;
		camShakeEvent.OnEventRaised += impulseSource.GenerateImpulse;
		
		Debug.Log($"Provided Camera Transform: {mainCamera.transform}");

		cameraTransformAnchor.Provide(mainCamera.GetComponent<Transform>());
	}

	private void OnDisable()
	{
		inputReader.CameraEvent -= OnCameraMove;

		protagonistCameraTransformAnchor.OnAnchorProvided -= SetupProtagonistVirtualCamera;
		camShakeEvent.OnEventRaised -= impulseSource.GenerateImpulse;

		cameraTransformAnchor.Unset();
	}

	private void Start()
	{
		//Setup the camera target if the protagonist is already available
		if(protagonistCameraTransformAnchor.isSet)
			SetupProtagonistVirtualCamera();
	}

	private void OnCameraMove(Vector2 cameraMovement)
	{
		if (_cameraMovementLock)
			return;
		
		// if (_cameraMovementLock)
		// 	return;

		// freeLookVCam.m_XAxis.m_InputAxisValue = cameraMovement.x * _speedMultiplier;
		// freeLookVCam.m_YAxis.m_InputAxisValue = cameraMovement.y * _speedMultiplier;
	}

	IEnumerator DisableCameraControlForFrame()
	{
		_cameraMovementLock = true;
		yield return new WaitForEndOfFrame();
		_cameraMovementLock = false;
	}

	/// <summary>
	/// Provides Cinemachine with its target, taken from the TransformAnchor SO containing a reference to the player's Transform component.
	/// This method is called every time the player is reinstantiated.
	/// </summary>
	private void SetupProtagonistVirtualCamera()
	{
		Debug.Log($"Protagonist Transform Anchor Value: {protagonistCameraTransformAnchor?.Value}");
		Transform target = protagonistCameraTransformAnchor?.Value;
		freeLookVCam.m_Follow = target;
		freeLookVCam.OnTargetObjectWarped(target, target.position - freeLookVCam.transform.position - Vector3.forward);
		freeLookVCam.m_LookAt = target;
	}
}
