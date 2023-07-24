using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "IsActuallyMoving", menuName = "State Machines/Conditions/Alpha/Is Actually Moving")]
public class IsAlphaActuallyMovingConditionSO : StateConditionSO
{
	[SerializeField] private float _treshold = 0.02f;

	protected override Condition CreateCondition() => new IsAlphaActuallyMovingCondition(_treshold);
}

public class IsAlphaActuallyMovingCondition : Condition
{
	private float _treshold;
	private CharacterController _characterController;

	public override void Awake(StateMachine stateMachine)
	{
		_characterController = stateMachine.GetComponent<CharacterController>();
	}

	public IsAlphaActuallyMovingCondition(float treshold)
	{
		_treshold = treshold;
	}

	protected override bool Statement()
	{
		return _characterController.velocity.sqrMagnitude > _treshold * _treshold;
	}
}
