﻿using Character.Alpha;
using ProjectX.StateMachine;
using ProjectX.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "StopAlphaMovement", menuName = "State Machines/Actions/Alpha/Stop Movement")]
public class StopAlphaMovementActionSO : StateActionSO<StopAlphaMovementAction>
{
    [SerializeField]
    private StateAction.SpecificMoment _moment;
    public StateAction.SpecificMoment Moment => _moment;

    protected override StateAction CreateAction() => new StopAlphaMovementAction();
}

public class StopAlphaMovementAction : StateAction
{
    private Protagonist _protagonist;
    private StopAlphaMovementActionSO _originSO => (StopAlphaMovementActionSO)OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _protagonist = stateMachine.GetComponent<Protagonist>();
    }

    public override void OnUpdate()
    {
        if (_originSO.Moment == SpecificMoment.OnUpdate)
            _protagonist.movementInput = Vector3.zero;
    }

    public override void OnStateEnter()
    {
        if (_originSO.Moment == SpecificMoment.OnStateEnter)
            _protagonist.movementInput = Vector3.zero;
    }

    public override void OnStateExit()
    {
        if(_originSO.Moment == SpecificMoment.OnStateExit)
            _protagonist.movementInput = Vector3.zero;
    }
}
