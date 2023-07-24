using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Conditions/Alpha/Is Character Controller Grounded")]
public class IsAlphaCharacterControllerGroundedConditionSO : StateConditionSO<IsAlphaCharacterControllerGroundedCondition> { }

public class IsAlphaCharacterControllerGroundedCondition : Condition
{
	private CharacterController _characterController;

	public override void Awake(StateMachine stateMachine)
	{
		_characterController = stateMachine.GetComponent<CharacterController>();
	}

	protected override bool Statement() => _characterController.isGrounded;
}
