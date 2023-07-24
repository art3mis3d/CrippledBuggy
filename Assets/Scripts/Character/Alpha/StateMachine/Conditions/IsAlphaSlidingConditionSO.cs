using Character.Alpha;
using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "IsSliding", menuName = "State Machines/Conditions/Alpha/Is Sliding")]
public class IsAlphaSlidingConditionSO : StateConditionSO<IsAlphaSlidingCondition> { }

public class IsAlphaSlidingCondition : Condition
{
	private CharacterController _characterController;
	private Protagonist _protagonistScript;

	public override void Awake(StateMachine stateMachine)
	{
		_characterController = stateMachine.GetComponent<CharacterController>();
		_protagonistScript = stateMachine.GetComponent<Protagonist>();
	}

	protected override bool Statement()
	{
		//First frame fail check
		if (_protagonistScript.lastHit == null)
			return false;

		float stepHeight = _protagonistScript.lastHit.point.y - _protagonistScript.transform.position.y;
		bool isWalkableStep = stepHeight <= _characterController.stepOffset;

		float currentSlope = Vector3.Angle(Vector3.up, _protagonistScript.lastHit.normal);
		bool isSlopeTooSteep = currentSlope >= _characterController.slopeLimit;

		if (!isSlopeTooSteep)
		{
			//Pendence is within slope limits
			return false;
		}
		else
		{
			//If the slope is too steep, we prevent sliding if it's within the step limit
			return !isWalkableStep;
		}
	}
}
