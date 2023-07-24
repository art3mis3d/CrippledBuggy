using ProjectX.StateMachine.ScriptableObjects;

namespace ProjectX.StateMachine
{
	public class State : IStateComponent
	{
		internal StateSO _originSO;
		internal StateMachine _stateMachine;
		internal StateTransition[] _transitions;
		internal StateAction[] _actions;

		internal State() { }

		public State(StateSO originSO, StateMachine stateMachine, StateTransition[] transitions, StateAction[] actions)
		{
			_originSO = originSO;
			_stateMachine = stateMachine;
			_transitions = transitions;
			_actions = actions;
		}

		public void OnStateEnter()
		{
			void OnStateEnter(IStateComponent[] comps)
			{
				for (int i = 0; i < comps.Length; i++)
				{
					comps[i].OnStateEnter();
				}
			}
			OnStateEnter(_transitions);
			OnStateEnter(_actions);
		}

		public void OnUpdate()
		{
			foreach (var action in _actions)
			{
				action.OnUpdate();
			}
		}

		public void OnStateExit()
		{
			void OnStateExit(IStateComponent[] comps)
			{
				for (int i = 0; i < comps.Length; i++)
					comps[i].OnStateExit();
			}
			OnStateExit(_transitions);
			OnStateExit(_actions);
		}

		public bool TryGetTransition(out State state)
		{
			state = null;

			foreach (var transition in _transitions)
			{
				if (transition.TryGetTransition(out state))
					break;
			}

			foreach (var transition in _transitions)
			{
				transition.ClearConditionsCache();
			}

			return state != null;
		}
	}
}
