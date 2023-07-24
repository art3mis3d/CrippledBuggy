using Character.Sigma;
using UnityEngine;
using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsSigmaMoving", menuName = "State Machines/Conditions/Sigma/Is Sigma Moving")]
public class IsSigmaMovingSO : StateConditionSO<IsSigmaMoving>
{
	public float threshold;
}

public class IsSigmaMoving : Condition
{
	private Protagonist _protagonistScript;
	protected IsSigmaMovingSO _originSO => (IsSigmaMovingSO)OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
		_protagonistScript = stateMachine.GetComponent<Protagonist>();
	}

	protected override bool Statement()
	{
		Vector3 movement = _protagonistScript.movementInput;
		movement.y = 0f;
		return movement.magnitude > _originSO.threshold;

	}
}
