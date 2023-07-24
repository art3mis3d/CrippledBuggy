using System;
using RuntimeAnchors;
using UnityEngine;

namespace Character.Beta
{
    /// <summary>
    /// <para>This component consumes input from the InputReader and stores its values. The input is then read, and manipulated, by the StateMachine's Actions.</para>
    /// </summary>
    public class Protagonist : MonoBehaviour
    {
        [SerializeField]
        private InputReader inputReader;

        [SerializeField]
        private TransformAnchor gameplayCameraTransform;
        
        private Vector2 _inputVector;
        private float _previousSpeed;

        //These fields are read and manipulated by StateMachine Actions;
        [NonSerialized]
        public bool JumpInput;
        [NonSerialized]
        public Vector3 MovementInput; // Initial input coming from this Protagonist script
        [NonSerialized]
        public Vector3 MovementVector; // Final movement vector, manipulated by StateMachine actions

        [NonSerialized]
        public ControllerColliderHit LastHit;

        public const float GravityMultiplier = 5f;
        public const float MaxFallSpeed = -50f;
        public const float MaxRiseSpeed = 100f;
        public const float GravityComebackMultiplier = .03f;
        public const float GravityDivider = .6f;
        public const float AirResistance = 5f;

        //Adds listeners for events being triggered in the InputReader script
        private void OnEnable()
        {
            inputReader.MoveEvent += OnMove;
            inputReader.JumpEvent += OnJumpInitiated;
            inputReader.JumpCanceledEvent += OnJumpCancelled;
            //...
        }

        //Removes all listeners to the events coming from the InputReader script
        private void OnDisable()
        {
            inputReader.MoveEvent -= OnMove;
            inputReader.JumpEvent -= OnJumpInitiated;
            inputReader.JumpCanceledEvent -= OnJumpCancelled;
            //...
        }

        private void Update()
        {
            RecalculateMovement();
        }

        private void RecalculateMovement()
        {
            float targetSpeed;
            Vector3 adjustedMovement;

            if (gameplayCameraTransform.isSet)
            {
                // get the two axes from the camera and flatten it on them on the XZ plane
                Vector3 cameraForward = gameplayCameraTransform.Value.forward;
                cameraForward.y = 0;
                Vector3 cameraRight = gameplayCameraTransform.Value.right;
                cameraRight.y = 0;
                
                // Use the two axes, modulated by the corresponding inputs, and construct the final vector
                adjustedMovement = cameraRight.normalized * _inputVector.x + cameraForward * _inputVector.y;
            }
            else
            {
                // no camera manager exists in the scene, so the input is just used in absolute world-space
                Debug.LogWarning("No gameplay camera exists in scene, so the input is just used in absolute world space.");
                adjustedMovement = new Vector3(_inputVector.x, 0f, _inputVector.y);
            }
            
            // BUG: to avoid getting Vector3.Zero vector, which results in player turning to x:0, z:0
            if (_inputVector.sqrMagnitude == 0)
                adjustedMovement = transform.forward * (adjustedMovement.sqrMagnitude * .0001f);
            
            // Accelerate/Decelerate
            targetSpeed = Mathf.Clamp01(_inputVector.magnitude);
            targetSpeed = Mathf.Lerp(_previousSpeed, targetSpeed, Time.deltaTime * 4f);

            MovementInput = adjustedMovement.normalized * targetSpeed;

            _previousSpeed = targetSpeed;
        }
        
        //---- Event Listeners ---- 

        private void OnMove(Vector2 movement)
        {
            _inputVector = movement;
        }
        
        private void OnJumpInitiated()
        {
            JumpInput = true;
        }

        private void OnJumpCancelled()
        {
            JumpInput = false;
        }
    }
}
