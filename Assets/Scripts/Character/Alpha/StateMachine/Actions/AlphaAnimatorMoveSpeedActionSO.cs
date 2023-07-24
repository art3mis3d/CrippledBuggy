using Character.Alpha;
using UnityEngine;
using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AlphaAnimatorMoveSpeedAction", menuName = "State Machines/Actions/Alpha/Animator Move Speed Action")]
public class AlphaAnimatorMoveSpeedActionSO : StateActionSO
{
	public string parameterName;

	protected override StateAction CreateAction() => new AlphaAnimatorMoveSpeedAction(Animator.StringToHash(parameterName));
}

public class AlphaAnimatorMoveSpeedAction : StateAction
{
	// Component references
	private Animator _animator;
	private Protagonist _protagonist;
	
	protected AlphaAnimatorMoveSpeedActionSO _originSO => (AlphaAnimatorMoveSpeedActionSO)OriginSO; // The SO this stateAction is spawned from
	private int _parameterHash;
	
	public AlphaAnimatorMoveSpeedAction(int parameterHash)
	{
		_parameterHash = parameterHash;
	}

	public override void Awake(StateMachine stateMachine)
	{
		_animator = stateMachine.GetComponent<Animator>();
		_protagonist = stateMachine.GetComponent<Protagonist>();
	}

	public override void OnUpdate()
	{
		// TODO: do I like that we are using the magnitude here, per frame? can this be done in a smarter way?
		float normalizedSpeed = _protagonist.movementInput.magnitude;
		_animator.SetFloat(_parameterHash, normalizedSpeed);
	}
}
