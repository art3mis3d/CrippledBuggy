using Character.Alpha;
using UnityEngine;
using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HorizontalMoveAction", menuName = "State Machines/Actions/Alpha/Horizontal Move Action")]
public class AlphaHorizontalMoveActionSO : StateActionSO<HorizontalMoveAction>
{
	[Tooltip("Horizontal XZ plane speed multiplier")]
	public float speed = 8f;
}

public class HorizontalMoveAction : StateAction
{
	// Component references
	private Protagonist _protagonistScript;
	protected AlphaHorizontalMoveActionSO _originSO => (AlphaHorizontalMoveActionSO)OriginSO; // The SO this stateAction is spawned from

	public override void Awake(StateMachine stateMachine)
	{
		_protagonistScript = stateMachine.GetComponent<Protagonist>();
	}

	public override void OnUpdate()
	{
		//delta.Time is used when the movement is applied (ApplyMovementVectorAction)
		_protagonistScript.movementVector.x = _protagonistScript.movementInput.x * _originSO.speed;
		_protagonistScript.movementVector.z = _protagonistScript.movementInput.z * _originSO.speed;
	}
}
