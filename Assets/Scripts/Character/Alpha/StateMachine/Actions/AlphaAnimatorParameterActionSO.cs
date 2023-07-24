using UnityEngine;
using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AlphaAnimatorParameterAction", menuName = "State Machines/Actions/Alpha/Animator Parameter Action")]
public class AlphaAnimatorParameterActionSO : StateActionSO
{
	public ParameterType parameterType;
	public string parameterName;

	public bool boolValue;
	public int intValue;
	public float floatValue;

	public StateAction.SpecificMoment whenToRun;

	protected override StateAction CreateAction() => new AlphaAnimatorParameterAction(Animator.StringToHash(parameterName));

	public enum ParameterType
	{
		Bool, Int, Float, Trigger
	}
}

public class AlphaAnimatorParameterAction : StateAction
{
	// Component references
	private Animator _animator;
	private AlphaAnimatorParameterActionSO _originSO => (AlphaAnimatorParameterActionSO)OriginSO; // The SO this stateAction is spawned from

	private int _parameterHash;
	
	public AlphaAnimatorParameterAction(int parameterHash)
	{
		_parameterHash = parameterHash;
	}
	
	public override void Awake(StateMachine stateMachine)
	{
		_animator = stateMachine.GetComponent<Animator>();
	}
	
	public override void OnStateEnter()
	{
		if (_originSO.whenToRun == SpecificMoment.OnStateEnter)
			SetParameter();
	}

	public override void OnStateExit()
	{
		if (_originSO.whenToRun == SpecificMoment.OnStateExit)
			SetParameter();
	}

	private void SetParameter()
	{
		switch (_originSO.parameterType)
		{
			case AlphaAnimatorParameterActionSO.ParameterType.Bool:
				_animator.SetBool(_parameterHash, _originSO.boolValue);
				break;
			case AlphaAnimatorParameterActionSO.ParameterType.Int:
				_animator.SetInteger(_parameterHash, _originSO.intValue);
				break;
			case AlphaAnimatorParameterActionSO.ParameterType.Float:
				_animator.SetFloat(_parameterHash, _originSO.floatValue);
				break;
			case AlphaAnimatorParameterActionSO.ParameterType.Trigger:
				_animator.SetTrigger(_parameterHash);
				break;
		}
	}
	
	public override void OnUpdate() { }
}
