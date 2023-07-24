using Character.Alpha;
using UnityEngine;
using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AlphaGroundGravityAction", menuName = "State Machines/Actions/Alpha/Ground Gravity Action")]
public class AlphaGroundGravityActionSO : StateActionSO<AlphaGroundGravityAction>
{
	[Tooltip("Vertical movement pulling down the player to keep it anchored to the groun")]
	public float verticalPull = -5f;
}

public class AlphaGroundGravityAction : StateAction
{
	// Component references
	private Protagonist _protagonistScript;
	protected AlphaGroundGravityActionSO _originSO => (AlphaGroundGravityActionSO)OriginSO; // The SO this stateAction is spawned from

	public override void Awake(StateMachine stateMachine)
	{
		_protagonistScript = stateMachine.GetComponent<Protagonist>();
	}

	public override void OnUpdate()
	{
		_protagonistScript.movementVector.y = _originSO.verticalPull;
	}
}
