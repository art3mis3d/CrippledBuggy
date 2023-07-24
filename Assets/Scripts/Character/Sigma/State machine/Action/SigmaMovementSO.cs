using UnityEngine;
using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SigmaMovement", menuName = "State Machines/Actions/Sigma/Sigma Movement")]
public class BetaMovementSO : StateActionSO<BetaMovement>
{
}

public class BetaMovement : StateAction
{
	protected BetaMovementSO _originSO => (BetaMovementSO)OriginSO;


	//private 

	public override void Awake(StateMachine stateMachine)
	{

	}

	public override void OnUpdate()
	{

	}

	public override void OnStateEnter()
	{

	}

	public override void OnStateExit()
	{
		
	}
}
