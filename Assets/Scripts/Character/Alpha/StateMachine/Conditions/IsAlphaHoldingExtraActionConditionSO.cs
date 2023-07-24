using Character.Alpha;
using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Conditions/Alpha/Is Holding ExtraAction")]
public class IsAlphaHoldingExtraActionConditionSO : StateConditionSO<IsAlphaHoldingExtraActionCondition> { }

public class IsAlphaHoldingExtraActionCondition : Condition
{
	//Component references
	private Protagonist _protagonistScript;

	public override void Awake(StateMachine stateMachine)
	{
		_protagonistScript = stateMachine.GetComponent<Protagonist>();
	}

	protected override bool Statement()
	{
		if (_protagonistScript.extraActionInput)
		{
			// Consume the input
			_protagonistScript.extraActionInput = false;

			return true;
		}
		else
		{
			return false;
		}
	}
}

