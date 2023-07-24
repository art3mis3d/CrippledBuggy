using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;
using UnityEngine;

namespace Character.Beta.State_machine.Action
{
	[CreateAssetMenu(fileName = "AnimatorParameterAction", menuName = "State Machines/Actions/Beta/Animator Parameter Action")]
	public class AnimatorParameterActionSO : StateActionSO
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
		private AnimatorParameterActionSO _originSO => (AnimatorParameterActionSO)OriginSO; // The SO this stateAction is spawned from
 
		private readonly int _parameterHash;
	
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

		public override void OnUpdate()
		{
			if(_originSO.whenToRun == SpecificMoment.OnUpdate)
				SetParameter();
		}

		private void SetParameter()
		{
			switch (_originSO.parameterType)
			{
				case AnimatorParameterActionSO.ParameterType.Bool:
					_animator.SetBool(_parameterHash, _originSO.boolValue);
					break;
				case AnimatorParameterActionSO.ParameterType.Int:
					_animator.SetInteger(_parameterHash, _originSO.intValue);
					break;
				case AnimatorParameterActionSO.ParameterType.Float:
					_animator.SetFloat(_parameterHash, _originSO.floatValue);
					break;
				case AnimatorParameterActionSO.ParameterType.Trigger:
					_animator.SetTrigger(_parameterHash);
					break;
			}
		}
	}
}