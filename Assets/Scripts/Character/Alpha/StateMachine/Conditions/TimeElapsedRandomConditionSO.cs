using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Conditions/Time elapsed random")]
public class TimeElapsedRandomConditionSO : StateConditionSO<TimeElapsedRandomCondition>
{
	public float minTimerLength = .5f;
	public float maxTimerLength = .5f;
}

public class TimeElapsedRandomCondition : Condition
{
	private float _startTime;
	private float timerLength = .5f;
	private TimeElapsedRandomConditionSO _originSO => (TimeElapsedRandomConditionSO)base.OriginSO; // The SO this Condition spawned from

	public override void OnStateEnter()
	{
		_startTime = Time.time;
		timerLength = Random.Range(_originSO.minTimerLength, _originSO.maxTimerLength);
	}

	protected override bool Statement() => Time.time >= _startTime + timerLength;
}
