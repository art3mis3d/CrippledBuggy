using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "RaiseVoidEventAction", menuName = "State Machines/Actions/Raise Void Event Action")]
public class RaiseVoidEventActionSO : StateActionSO
{
	public VoidEventChannelSO voidEvent;

	protected override StateAction CreateAction() => new AlphaRaiseVoidEventAction();
}

public class AlphaRaiseVoidEventAction : StateAction
{
	private VoidEventChannelSO _voidEvent;
	public override void Awake(StateMachine stateMachine)
	{
		_voidEvent = ((RaiseVoidEventActionSO)OriginSO).voidEvent;
	}

	public override void OnUpdate()
	{

	}

	public override void OnStateEnter()
	{
		_voidEvent.RaiseEvent();
	}

	public override void OnStateExit()
	{

	}
}
