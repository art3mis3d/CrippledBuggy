using Character.Alpha;
using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Conditions/Alpha/Is Holding Jump")]
public class IsAlphaHoldingJumpConditionSO : StateConditionSO<IsAlphaHoldingJumpCondition> { }

public class IsAlphaHoldingJumpCondition : Condition
{
	//Component references
	private Protagonist _protagonistScript;

	public override void Awake(StateMachine stateMachine)
	{
		_protagonistScript = stateMachine.GetComponent<Protagonist>();
	}

	protected override bool Statement() => _protagonistScript.jumpInput;
}
