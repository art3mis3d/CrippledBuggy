using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Conditions/Alpha/Has Received Event")]
public class AlphaHasReceivedEventSO : StateConditionSO<AlphaHasReceivedEventCondition>
{
	public VoidEventChannelSO voidEvent;
}

public class AlphaHasReceivedEventCondition : Condition
{
	private AlphaHasReceivedEventSO _originSO => (AlphaHasReceivedEventSO)base.OriginSO; // The SO this Condition spawned from

	private bool _eventTriggered;

	public override void Awake(StateMachine stateMachine)
	{
		_eventTriggered = false;
		_originSO.voidEvent.OnEventRaised += EventReceived;
	}

	protected override bool Statement()
	{
		return _eventTriggered;
	}

	private void EventReceived()
	{
		_eventTriggered = true;
	}

	public override void OnStateExit()
	{
		_eventTriggered = false;
	}
}
