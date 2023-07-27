using Character.Alpha;
using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "Ascend", menuName = "State Machines/Actions/Alpha/Ascend")]
public class AlphaAscendActionSO : StateActionSO<AlphaAscendAction>
{
	[Tooltip("The initial upwards push when pressing jump. This is injected into verticalMovement, and gradually cancelled by gravity")]
	public float initialJumpForce = 6f;
}

public class AlphaAscendAction : StateAction
{
	//Component references
	private Protagonist _protagonistScript;

	private float _verticalMovement;
	private float _gravityContributionMultiplier;
	private AlphaAscendActionSO _originSO => (AlphaAscendActionSO)base.OriginSO; // The SO this StateAction spawned from

	public override void Awake(StateMachine stateMachine)
	{
		_protagonistScript = stateMachine.GetComponent<Protagonist>();
	}

	public override void OnStateEnter()
	{
		_verticalMovement = _originSO.initialJumpForce;
	}

	public override void OnUpdate()
	{
		_gravityContributionMultiplier += Protagonist.GRAVITY_COMEBACK_MULTIPLIER;
		_gravityContributionMultiplier *= Protagonist.GRAVITY_DIVIDER; //Reduce the gravity effect

		//Note that deltaTime is used even though it's going to be used in ApplyMovementVectorAction, this is because it represents an acceleration, not a speed
		_verticalMovement += Physics.gravity.y * Protagonist.GRAVITY_MULTIPLIER * _gravityContributionMultiplier * Time.deltaTime;
		//Note that even if it's added, the above value is negative due to Physics.gravity.y

		_protagonistScript.movementVector.y = _verticalMovement;
	}
}
